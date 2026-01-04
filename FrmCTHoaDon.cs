using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmCTHoaDon : Form
    {
        private readonly string _maHD;
        private DataTable _dt;

        public FrmCTHoaDon(string maHD)
        {
            InitializeComponent();
            _maHD = maHD;

            Load += FrmCTHoaDon_Load;

            btnHienThi.Click += (s, e) => LoadCT(_maHD);
            btnTim.Click += (s, e) => Search();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;

            dgvCT.CellClick += dgvCT_CellClick;

            txtMaHD.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
        }

        private void FrmCTHoaDon_Load(object sender, EventArgs e)
        {
            SetupGrid();
            txtMaHD.Text = _maHD;
            LoadCT(_maHD);
        }

        private void SetupGrid()
        {
            dgvCT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCT.MultiSelect = false;
            dgvCT.AllowUserToAddRows = false;
            dgvCT.ReadOnly = true;
            dgvCT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadCT(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
            {
                MessageBox.Show("Thiếu mã hóa đơn!");
                return;
            }

            _dt = SqlHelper.Query(@"
                SELECT MaHD, MaSach, SoLuong, DonGia, ThanhTien
                FROM CTHoaDon
                WHERE MaHD = @MaHD
                ORDER BY MaSach",
                new SqlParameter("@MaHD", maHD));

            dgvCT.DataSource = _dt;

            // Đổi HeaderText cho đẹp (NHƯNG vẫn giữ Name cột gốc để code không lỗi)
            if (dgvCT.Columns["MaHD"] != null) dgvCT.Columns["MaHD"].HeaderText = "Mã HĐ";
            if (dgvCT.Columns["MaSach"] != null) dgvCT.Columns["MaSach"].HeaderText = "Mã sách";
            if (dgvCT.Columns["SoLuong"] != null) dgvCT.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgvCT.Columns["DonGia"] != null) dgvCT.Columns["DonGia"].HeaderText = "Đơn giá";
            if (dgvCT.Columns["ThanhTien"] != null) dgvCT.Columns["ThanhTien"].HeaderText = "Thành tiền";

            UpdateTongTien(maHD);
        }

        private void Search()
        {
            // style bạn đang làm: tìm theo textbox txtTim
            string key = txtTim.Text.Trim();

            if (string.IsNullOrWhiteSpace(key))
            {
                LoadCT(_maHD);
                return;
            }

            // Nếu nhập đúng mã HĐ thì load theo mã đó, còn không thì filter theo mã sách
            if (key.StartsWith("HD", StringComparison.OrdinalIgnoreCase) || key.Length >= 3)
            {
                // lọc trong hóa đơn hiện tại theo mã sách
                _dt = SqlHelper.Query(@"
                    SELECT MaHD, MaSach, SoLuong, DonGia, ThanhTien
                    FROM CTHoaDon
                    WHERE MaHD = @MaHD AND (MaSach LIKE @k)
                    ORDER BY MaSach",
                    new SqlParameter("@MaHD", _maHD),
                    new SqlParameter("@k", "%" + key + "%"));

                dgvCT.DataSource = _dt;

                if (dgvCT.Columns["MaHD"] != null) dgvCT.Columns["MaHD"].HeaderText = "Mã HĐ";
                if (dgvCT.Columns["MaSach"] != null) dgvCT.Columns["MaSach"].HeaderText = "Mã sách";
                if (dgvCT.Columns["SoLuong"] != null) dgvCT.Columns["SoLuong"].HeaderText = "Số lượng";
                if (dgvCT.Columns["DonGia"] != null) dgvCT.Columns["DonGia"].HeaderText = "Đơn giá";
                if (dgvCT.Columns["ThanhTien"] != null) dgvCT.Columns["ThanhTien"].HeaderText = "Thành tiền";

                UpdateTongTien(_maHD);
            }
        }

        private void dgvCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvCT.Rows[e.RowIndex];

            txtMaHD.Text = row.Cells["MaHD"].Value?.ToString();
            txtMaSach.Text = row.Cells["MaSach"].Value?.ToString();
            txtSoLuong.Text = row.Cells["SoLuong"].Value?.ToString();
            txtDonGia.Text = row.Cells["DonGia"].Value?.ToString();
            txtThanhTien.Text = row.Cells["ThanhTien"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateLine(out int sl, out decimal donGia)) return;

            // check hóa đơn tồn tại
            var hdExist = SqlHelper.Scalar("SELECT COUNT(*) FROM HoaDon WHERE MaHD=@id",
                new SqlParameter("@id", _maHD));
            if (Convert.ToInt32(hdExist) == 0)
            {
                MessageBox.Show("Hóa đơn chưa tồn tại! Hãy tạo HĐ trước ở form Hóa đơn.");
                return;
            }

            // check sách tồn tại
            var bookExist = SqlHelper.Scalar("SELECT COUNT(*) FROM Sach WHERE MaSach=@ms",
                new SqlParameter("@ms", txtMaSach.Text.Trim()));
            if (Convert.ToInt32(bookExist) == 0)
            {
                MessageBox.Show("Mã sách không tồn tại!");
                return;
            }

            // check trùng dòng
            var existLine = SqlHelper.Scalar("SELECT COUNT(*) FROM CTHoaDon WHERE MaHD=@hd AND MaSach=@ms",
                new SqlParameter("@hd", _maHD),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));
            if (Convert.ToInt32(existLine) > 0)
            {
                MessageBox.Show("Sách này đã có trong hóa đơn! Hãy dùng Sửa.");
                return;
            }

            // check tồn kho
            int tonKho = Convert.ToInt32(SqlHelper.Scalar("SELECT SoLuongTon FROM Sach WHERE MaSach=@ms",
                new SqlParameter("@ms", txtMaSach.Text.Trim())));
            if (sl > tonKho)
            {
                MessageBox.Show($"Không đủ tồn kho! Tồn hiện tại: {tonKho}");
                return;
            }

            // Insert
            SqlHelper.Execute(@"
                INSERT INTO CTHoaDon(MaHD, MaSach, SoLuong, DonGia)
                VALUES(@MaHD,@MaSach,@SoLuong,@DonGia)",
                new SqlParameter("@MaHD", _maHD),
                new SqlParameter("@MaSach", txtMaSach.Text.Trim()),
                new SqlParameter("@SoLuong", sl),
                new SqlParameter("@DonGia", donGia)
            );

            // trừ tồn kho
            SqlHelper.Execute(@"
                UPDATE Sach SET SoLuongTon = SoLuongTon - @sl
                WHERE MaSach=@ms",
                new SqlParameter("@sl", sl),
                new SqlParameter("@ms", txtMaSach.Text.Trim())
            );

            LoadCT(_maHD);
            ClearLine();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Chọn 1 dòng để sửa!");
                return;
            }
            if (!ValidateLine(out int newSL, out decimal newDG)) return;

            // lấy số lượng cũ
            var oldObj = SqlHelper.Scalar(@"
                SELECT SoLuong
                FROM CTHoaDon
                WHERE MaHD=@hd AND MaSach=@ms",
                new SqlParameter("@hd", _maHD),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            if (oldObj == null)
            {
                MessageBox.Show("Không tìm thấy dòng chi tiết để sửa!");
                return;
            }

            int oldSL = Convert.ToInt32(oldObj);

            // hoàn tồn kho cũ
            SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon + @sl WHERE MaSach=@ms",
                new SqlParameter("@sl", oldSL),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            // check tồn kho đủ cho số mới
            int tonKho = Convert.ToInt32(SqlHelper.Scalar("SELECT SoLuongTon FROM Sach WHERE MaSach=@ms",
                new SqlParameter("@ms", txtMaSach.Text.Trim())));
            if (newSL > tonKho)
            {
                // rollback hoàn tồn kho
                SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon - @sl WHERE MaSach=@ms",
                    new SqlParameter("@sl", oldSL),
                    new SqlParameter("@ms", txtMaSach.Text.Trim()));

                MessageBox.Show($"Không đủ tồn kho để sửa! Tồn hiện tại: {tonKho}");
                return;
            }

            // update chi tiết
            SqlHelper.Execute(@"
                UPDATE CTHoaDon
                SET SoLuong=@sl, DonGia=@dg
                WHERE MaHD=@hd AND MaSach=@ms",
                new SqlParameter("@sl", newSL),
                new SqlParameter("@dg", newDG),
                new SqlParameter("@hd", _maHD),
                new SqlParameter("@ms", txtMaSach.Text.Trim())
            );

            // trừ tồn kho mới
            SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon - @sl WHERE MaSach=@ms",
                new SqlParameter("@sl", newSL),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            LoadCT(_maHD);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Chọn 1 dòng để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa dòng chi tiết này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            object slObj = SqlHelper.Scalar(@"
                SELECT SoLuong
                FROM CTHoaDon
                WHERE MaHD=@hd AND MaSach=@ms",
                new SqlParameter("@hd", _maHD),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            if (slObj == null)
            {
                MessageBox.Show("Không tìm thấy dòng để xóa!");
                return;
            }

            int sl = Convert.ToInt32(slObj);

            SqlHelper.Execute("DELETE FROM CTHoaDon WHERE MaHD=@hd AND MaSach=@ms",
                new SqlParameter("@hd", _maHD),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            // hoàn tồn kho
            SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon + @sl WHERE MaSach=@ms",
                new SqlParameter("@sl", sl),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            LoadCT(_maHD);
            ClearLine();
        }

        private void UpdateTongTien(string maHD)
        {
            object sumObj = SqlHelper.Scalar(
                "SELECT ISNULL(SUM(ThanhTien),0) FROM CTHoaDon WHERE MaHD=@hd",
                new SqlParameter("@hd", maHD));

            decimal tong = Convert.ToDecimal(sumObj);

            SqlHelper.Execute("UPDATE HoaDon SET TongTien=@t WHERE MaHD=@hd",
                new SqlParameter("@t", tong),
                new SqlParameter("@hd", maHD));

            txtTongTien.Text = tong.ToString("0");
        }

        private bool ValidateLine(out int sl, out decimal donGia)
        {
            sl = 0;
            donGia = 0;

            if (string.IsNullOrWhiteSpace(txtMaSach.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Nhập đủ Mã sách / Số lượng / Đơn giá!");
                return false;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số > 0!");
                return false;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số >= 0!");
                return false;
            }

            // tính thành tiền để hiển thị
            txtThanhTien.Text = (sl * donGia).ToString("0");
            return true;
        }

        private void ClearLine()
        {
            txtMaSach.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();
            txtThanhTien.Clear();
            txtMaSach.Focus();
        }
    }
}
