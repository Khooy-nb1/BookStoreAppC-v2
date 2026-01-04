using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmThongKeTopBan : Form
    {
        private DataTable _dt;

        public FrmThongKeTopBan()
        {
            InitializeComponent();

            btnHienThi.Click += (s, e) => LoadTop();
            btnXuatText.Click += (s, e) => ExportTxt();
            btnTrangChu.Click += (s, e) => Close();

            Load += (s, e) => { SetupGrid(); LoadTop(); };
        }

        private void SetupGrid()
        {
            dgvTop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTop.MultiSelect = false;
            dgvTop.AllowUserToAddRows = false;
            dgvTop.ReadOnly = true;
            dgvTop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadTop()
        {
            int topN = 10;
            int.TryParse(txtTop.Text.Trim(), out topN);
            if (topN <= 0) topN = 10;

            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date;

            _dt = SqlHelper.Query(@"
                SELECT TOP (@top)
                       s.MaSach,
                       s.TenSach,
                       SUM(ct.SoLuong) AS TongSoLuongBan,
                       SUM(ct.ThanhTien) AS TongDoanhThu
                FROM CTHoaDon ct
                JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                JOIN Sach s ON ct.MaSach = s.MaSach
                WHERE hd.NgayLap >= @from AND hd.NgayLap <= @to
                GROUP BY s.MaSach, s.TenSach
                ORDER BY SUM(ct.SoLuong) DESC, SUM(ct.ThanhTien) DESC",
                new SqlParameter("@top", topN),
                new SqlParameter("@from", from),
                new SqlParameter("@to", to));

            dgvTop.DataSource = _dt;
        }

        private void ExportTxt()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text file (*.txt)|*.txt";
                sfd.FileName = "ThongKeTopBan.txt";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("THỐNG KÊ TOP BÁN CHẠY");
                sb.AppendLine("Từ: " + dtFrom.Value.ToString("yyyy-MM-dd") + "  Đến: " + dtTo.Value.ToString("yyyy-MM-dd"));
                sb.AppendLine();

                for (int i = 0; i < dgvTop.Columns.Count; i++)
                {
                    sb.Append(dgvTop.Columns[i].HeaderText);
                    sb.Append(i == dgvTop.Columns.Count - 1 ? "\n" : "\t");
                }

                foreach (DataGridViewRow r in dgvTop.Rows)
                {
                    if (r.IsNewRow) continue;
                    for (int i = 0; i < dgvTop.Columns.Count; i++)
                    {
                        sb.Append(r.Cells[i].Value?.ToString());
                        sb.Append(i == dgvTop.Columns.Count - 1 ? "\n" : "\t");
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất file thành công!");
            }
        }
    }
}
