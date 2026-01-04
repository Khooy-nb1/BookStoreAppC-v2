
using BookStoreApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace BookStoreApp
{
    public partial class FrmPhieuNhap : Form
    {
        private DataTable _dt;

        public FrmPhieuNhap()
        {
            InitializeComponent();

            Load += FrmPhieuNhap_Load;

            btnHienThi.Click += (s, e) => LoadPhieuNhap();
            btnTim.Click += (s, e) => Search();

            btnTaoPN.Click += btnTaoPN_Click;
            btnXoaPN.Click += btnXoaPN_Click;
            btnChiTiet.Click += btnChiTiet_Click;

            dgvPN.CellClick += dgvPN_CellClick;

            btnTrangChu.Click += (s, e) => Close();
        }

        private void FrmPhieuNhap_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadPhieuNhap();
            txtTongTien.ReadOnly = true;
            dtNgayNhap.Value = DateTime.Now;
        }

        private void SetupGrid()
        {
            dgvPN.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPN.MultiSelect = false;
            dgvPN.AllowUserToAddRows = false;
            dgvPN.ReadOnly = true;
            dgvPN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadPhieuNhap()
        {
            _dt = SqlHelper.Query(@"
            SELECT MaPN AS [Mã PN], NgayNhap AS [Ngày nhập], MaNXB AS [Mã NXB], TongTien AS [Tổng tiền]
            FROM PhieuNhap
            ORDER BY NgayNhap DESC, MaPN DESC");
            dgvPN.DataSource = _dt;
        }

        private void dgvPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvPN.Rows[e.RowIndex];

            txtMaPN.Text = row.Cells["Mã PN"].Value?.ToString();
            dtNgayNhap.Value = Convert.ToDateTime(row.Cells["Ngày nhập"].Value);
            txtMaNXB.Text = row.Cells["Mã NXB"].Value?.ToString();
            txtTongTien.Text = row.Cells["Tổng tiền"].Value?.ToString();
        }

        private void btnTaoPN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPN.Text) || string.IsNullOrWhiteSpace(txtMaNXB.Text))
            {
                MessageBox.Show("Nhập Mã PN và Mã NXB!");
                return;
            }

            // check PN tồn tại
            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM PhieuNhap WHERE MaPN=@id",
                new SqlParameter("@id", txtMaPN.Text.Trim()));
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Mã PN đã tồn tại!");
                return;
            }

            // check NXB tồn tại
            var nxbExist = SqlHelper.Scalar("SELECT COUNT(*) FROM NhaXuatBan WHERE MaNXB=@x",
                new SqlParameter("@x", txtMaNXB.Text.Trim()));
            if (Convert.ToInt32(nxbExist) == 0)
            {
                MessageBox.Show("Mã NXB không tồn tại!");
                return;
            }

            int n = SqlHelper.Execute(@"
            INSERT INTO PhieuNhap(MaPN, NgayNhap, MaNXB, TongTien)
            VALUES(@MaPN, @Ngay, @MaNXB, 0)",
                new SqlParameter("@MaPN", txtMaPN.Text.Trim()),
                new SqlParameter("@Ngay", dtNgayNhap.Value.Date),
                new SqlParameter("@MaNXB", txtMaNXB.Text.Trim())
            );

            if (n > 0)
            {
                LoadPhieuNhap();
                MessageBox.Show("Tạo phiếu nhập thành công! Vào Chi tiết để thêm sách nhập.");
            }
        }

        private void btnXoaPN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPN.Text))
            {
                MessageBox.Show("Chọn 1 phiếu nhập để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa phiếu nhập này? (Sẽ hoàn tồn kho theo chi tiết)", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                // Hoàn tồn kho trước khi xóa chi tiết
                var dt = SqlHelper.Query("SELECT MaSach, SoLuong FROM CTPhieuNhap WHERE MaPN=@pn",
                    new SqlParameter("@pn", txtMaPN.Text.Trim()));

                foreach (DataRow r in dt.Rows)
                {
                    string ms = r["MaSach"].ToString();
                    int sl = Convert.ToInt32(r["SoLuong"]);

                    // vì lúc nhập đã cộng kho => giờ xóa PN phải trừ lại
                    SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon - @sl WHERE MaSach=@ms",
                        new SqlParameter("@sl", sl),
                        new SqlParameter("@ms", ms));
                }

                SqlHelper.Execute("DELETE FROM CTPhieuNhap WHERE MaPN=@pn",
                    new SqlParameter("@pn", txtMaPN.Text.Trim()));

                SqlHelper.Execute("DELETE FROM PhieuNhap WHERE MaPN=@pn",
                    new SqlParameter("@pn", txtMaPN.Text.Trim()));

                LoadPhieuNhap();
                txtMaPN.Clear();
                txtMaNXB.Clear();
                txtTongTien.Clear();
                MessageBox.Show("Xóa phiếu nhập thành công!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa phiếu nhập.\n" + ex.Message);
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPN.Text))
            {
                MessageBox.Show("Chọn/Nhập 1 Mã PN trước!");
                return;
            }

            var f = new FrmCTPhieuNhap(txtMaPN.Text.Trim());
            f.ShowDialog();

            LoadPhieuNhap(); // refresh tổng tiền
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();
            _dt = SqlHelper.Query(@"
            SELECT MaPN AS [Mã PN], NgayNhap AS [Ngày nhập], MaNXB AS [Mã NXB], TongTien AS [Tổng tiền]
            FROM PhieuNhap
            WHERE MaPN LIKE @k
            ORDER BY NgayNhap DESC, MaPN DESC",
                new SqlParameter("@k", "%" + key + "%"));
            dgvPN.DataSource = _dt;
        }
    }
}