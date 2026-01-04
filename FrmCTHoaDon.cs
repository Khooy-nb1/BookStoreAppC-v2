using BookStoreApp;
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
            btnTim.Click += (s, e) => LoadCT(txtTim.Text.Trim());

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;

            dgvCT.CellClick += dgvCT_CellClick;

            if (txtThanhTien != null) txtThanhTien.ReadOnly = true;
            if (txtTongTien != null) txtTongTien.ReadOnly = true;
        }

        private void FrmCTHoaDon_Load(object sender, EventArgs e)
        {
            SetupGrid();

            // hiển thị mã HĐ
           
            if (Controls.ContainsKey("txtMaHD")) ((TextBox)Controls["txtMaHD"]).Text = _maHD;

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
                MessageBox.Show("Nhập mã HĐ để xem chi tiết!");
                return;
            }

            _dt = SqlHelper.Query(@"
            SELECT MaHD AS [Mã HĐ], MaSach AS [Mã sách], SoLuong AS [Số lượng],
                   DonGia AS [Đơn giá], ThanhTien AS [Thành tiền]
            FROM CTHoaDon
            WHERE MaHD = @MaHD
            ORDER BY MaSach",
                new SqlParameter("@MaHD", maHD));

            dgvCT.DataSource = _dt;

            // cập nhật tổng tiền hiển thị
            UpdateTongTien(maHD);
        }

        private void dgvCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvCT.Rows[e.RowIndex];

            txtMaSach.Text = row.Cells["Mã sách"].Value?.ToString();
            txtSoLuong.Text = row.Cells["Số lượng"].Value?.ToString();
            txtDonGia.Text = row.Cells["Đơn giá"].Value?.ToString();

            if (txtThanhTien != null)
                txtThanhTien.Text = row.Cells["Thành tiền"].Value?.ToString();
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

            // check tồn kho
            var ton = SqlHelper.Scalar("SELECT SoLuongTon FROM Sach WHERE MaSach=@ms",
                new SqlParameter("@ms", txtMaSach.Text.Trim()));
            int tonKho = Convert.ToInt32(ton);
            if (sl > tonKho)
            {
                MessageBox.Show($"Không đủ tồn kho! Tồn hiện tại: {tonKho}");
                return;
            }

            // check trùng dòng (MaHD, MaSach)
            var existLine = SqlHelper.Scalar("SELECT COUNT(*) FROM CTHoaDon WHERE MaHD=@hd AND MaSach=@ms",
                new SqlParameter("@hd", _maHD),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));
            if (Convert.ToInt32(existLine) > 0)
            {
                MessageBox.Show("Sách này đã có trong hóa đơn! Hãy dùng Sửa để đổi số lượng/đơn giá.");
                return;
            }

            // Insert chi tiết
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
            if (!ValidateLine(out int newSL, out decimal newDG)) return;

            // lấy dòng cũ để hoàn tồn kho rồi trừ lại theo số mới
            var dtOld = SqlHelper.Query(@"
            SELECT SoLuong, DonGia FROM CTHoaDon WHERE MaHD=@hd AND MaSach=@ms",
                new SqlParameter("@hd", _maHD),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            if (dtOld.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dòng chi tiết để sửa!");
                return;
            }

            int oldSL = Convert.ToInt32(dtOld.Rows[0]["SoLuong"]);

            // hoàn tồn kho số cũ
            SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon + @sl WHERE MaSach=@ms",
                new SqlParameter("@sl", oldSL),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            // check tồn kho đủ để trừ số mới
            int tonKho = Convert.ToInt32(SqlHelper.Scalar("SELECT SoLuongTon FROM Sach WHERE MaSach=@ms",
                new SqlParameter("@ms", txtMaSach.Text.Trim())));
            if (newSL > tonKho)
            {
                // rollback hoàn (trừ lại old để giữ đúng)
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

            // trừ tồn kho theo số mới
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

            // lấy số lượng để hoàn tồn kho
            var slObj = SqlHelper.Scalar("SELECT SoLuong FROM CTHoaDon WHERE MaHD=@hd AND MaSach=@ms",
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
            var sum = SqlHelper.Scalar("SELECT ISNULL(SUM(ThanhTien),0) FROM CTHoaDon WHERE MaHD=@hd",
                new SqlParameter("@hd", maHD));

            decimal tong = Convert.ToDecimal(sum);

            // update vào bảng HoaDon
            SqlHelper.Execute("UPDATE HoaDon SET TongTien=@t WHERE MaHD=@hd",
                new SqlParameter("@t", tong),
                new SqlParameter("@hd", maHD));

            if (txtTongTien != null) txtTongTien.Text = tong.ToString("0");
        }

        private bool ValidateLine(out int sl, out decimal donGia)
        {
            sl = 0; donGia = 0;

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

            return true;
        }

        private void ClearLine()
        {
            txtMaSach.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();
            if (txtThanhTien != null) txtThanhTien.Clear();
            txtMaSach.Focus();
        }
    }
}