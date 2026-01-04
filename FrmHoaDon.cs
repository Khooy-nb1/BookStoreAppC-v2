using BookStoreApp;
using BookStoreApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmHoaDon : Form
    {
        private DataTable _dt;

        public FrmHoaDon()
        {
            InitializeComponent();
            Load += FrmHoaDon_Load;

            btnHienThi.Click += (s, e) => LoadHoaDon();
            btnTim.Click += (s, e) => Search();
            btnTaoHD.Click += btnTaoHD_Click;
            btnXoaHD.Click += btnXoaHD_Click;
            btnChiTiet.Click += btnChiTiet_Click;

            dgvHD.CellClick += dgvHD_CellClick;
            btnTrangChu.Click += (s, e) => Close();
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadHoaDon();
            txtTongTien.ReadOnly = true;
            dtNgayLap.Value = DateTime.Now;
        }

        private void SetupGrid()
        {
            dgvHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHD.MultiSelect = false;
            dgvHD.AllowUserToAddRows = false;
            dgvHD.ReadOnly = true;
            dgvHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadHoaDon()
        {
            _dt = SqlHelper.Query(@"
            SELECT MaHD AS [Mã HĐ], NgayLap AS [Ngày lập], TongTien AS [Tổng tiền]
            FROM HoaDon
            ORDER BY NgayLap DESC, MaHD DESC");
            dgvHD.DataSource = _dt;
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvHD.Rows[e.RowIndex];
            txtMaHD.Text = row.Cells["Mã HĐ"].Value?.ToString();
            dtNgayLap.Value = Convert.ToDateTime(row.Cells["Ngày lập"].Value);
            txtTongTien.Text = row.Cells["Tổng tiền"].Value?.ToString();
        }

        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Nhập Mã HĐ!");
                return;
            }

            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM HoaDon WHERE MaHD=@id",
                new SqlParameter("@id", txtMaHD.Text.Trim()));
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Mã HĐ đã tồn tại!");
                return;
            }

            int n = SqlHelper.Execute(@"
            INSERT INTO HoaDon(MaHD, NgayLap, TongTien)
            VALUES(@MaHD, @NgayLap, 0)",
                new SqlParameter("@MaHD", txtMaHD.Text.Trim()),
                new SqlParameter("@NgayLap", dtNgayLap.Value.Date)
            );

            if (n > 0)
            {
                LoadHoaDon();
                MessageBox.Show("Tạo hóa đơn thành công! Giờ vào Chi tiết để thêm sách.");
            }
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Chọn 1 hóa đơn để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa hóa đơn này? (Phải xóa chi tiết trước)", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                // Xóa chi tiết trước để tránh FK
                SqlHelper.Execute("DELETE FROM CTHoaDon WHERE MaHD=@id",
                    new SqlParameter("@id", txtMaHD.Text.Trim()));

                SqlHelper.Execute("DELETE FROM HoaDon WHERE MaHD=@id",
                    new SqlParameter("@id", txtMaHD.Text.Trim()));

                LoadHoaDon();
                txtMaHD.Clear();
                txtTongTien.Clear();
                MessageBox.Show("Xóa hóa đơn thành công!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa hóa đơn.\n" + ex.Message);
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Chọn/Nhập 1 Mã HĐ trước!");
                return;
            }

            // mở form chi tiết hóa đơn, truyền MaHD
            var f = new FrmCTHoaDon(txtMaHD.Text.Trim());
            f.ShowDialog();

            // refresh tổng tiền sau khi đóng
            LoadHoaDon();
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();
            _dt = SqlHelper.Query(@"
            SELECT MaHD AS [Mã HĐ], NgayLap AS [Ngày lập], TongTien AS [Tổng tiền]
            FROM HoaDon
            WHERE MaHD LIKE @k
            ORDER BY NgayLap DESC, MaHD DESC",
                new SqlParameter("@k", "%" + key + "%"));
            dgvHD.DataSource = _dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}