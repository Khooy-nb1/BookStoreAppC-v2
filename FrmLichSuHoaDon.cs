using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmLichSuHoaDon : Form
    {
        private DataTable _dt;

        public FrmLichSuHoaDon()
        {
            InitializeComponent();

            Load += (s, e) => { SetupGrid(); LoadHD(); };

            btnHienThi.Click += (s, e) => LoadHD();
            btnTim.Click += (s, e) => Search();

            btnChiTiet.Click += (s, e) => OpenChiTiet();
            btnXuatText.Click += (s, e) => ExportTxt();
            btnTrangChu.Click += (s, e) => Close();

            dgvHD.CellClick += dgvHD_CellClick;
        }

        private void SetupGrid()
        {
            dgvHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHD.MultiSelect = false;
            dgvHD.AllowUserToAddRows = false;
            dgvHD.ReadOnly = true;
            dgvHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadHD()
        {
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date;

            _dt = SqlHelper.Query(@"
                SELECT MaHD, NgayLap, TongTien
                FROM HoaDon
                WHERE NgayLap >= @from AND NgayLap <= @to
                ORDER BY NgayLap DESC, MaHD DESC",
                new SqlParameter("@from", from),
                new SqlParameter("@to", to));

            dgvHD.DataSource = _dt;
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date;

            _dt = SqlHelper.Query(@"
                SELECT MaHD, NgayLap, TongTien
                FROM HoaDon
                WHERE (MaHD LIKE @k) AND (NgayLap >= @from AND NgayLap <= @to)
                ORDER BY NgayLap DESC, MaHD DESC",
                new SqlParameter("@k", "%" + key + "%"),
                new SqlParameter("@from", from),
                new SqlParameter("@to", to));

            dgvHD.DataSource = _dt;
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvHD.Rows[e.RowIndex];
            txtMaHD.Text = row.Cells["MaHD"].Value?.ToString();
        }

        private void OpenChiTiet()
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Chọn 1 hóa đơn để xem chi tiết!");
                return;
            }

            // CHÚ Ý: FrmCTHoaDon của bạn đang có constructor FrmCTHoaDon(string maHD)
            // => gọi đúng như này để hết lỗi "There is no argument given..."
            FrmCTHoaDon f = new FrmCTHoaDon(txtMaHD.Text.Trim());
            f.ShowDialog();
        }

        private void ExportTxt()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text file (*.txt)|*.txt";
                sfd.FileName = "LichSuHoaDon.txt";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("LỊCH SỬ HÓA ĐƠN");
                sb.AppendLine("Từ: " + dtFrom.Value.ToString("yyyy-MM-dd") + "  Đến: " + dtTo.Value.ToString("yyyy-MM-dd"));
                sb.AppendLine();

                for (int i = 0; i < dgvHD.Columns.Count; i++)
                {
                    sb.Append(dgvHD.Columns[i].HeaderText);
                    sb.Append(i == dgvHD.Columns.Count - 1 ? "\n" : "\t");
                }

                foreach (DataGridViewRow r in dgvHD.Rows)
                {
                    if (r.IsNewRow) continue;
                    for (int i = 0; i < dgvHD.Columns.Count; i++)
                    {
                        sb.Append(r.Cells[i].Value?.ToString());
                        sb.Append(i == dgvHD.Columns.Count - 1 ? "\n" : "\t");
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất file thành công!");
            }
        }
    }
}
