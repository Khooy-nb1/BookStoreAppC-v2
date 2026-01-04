using BookStoreApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmCTPhieuNhap : Form
    {
        private readonly string _maPN;
        private DataTable _dt;

        public FrmCTPhieuNhap(string maPN)
        {
            InitializeComponent();
            _maPN = maPN;

            Load += FrmCTPhieuNhap_Load;

            btnHienThi.Click += (s, e) => LoadCT(_maPN);
            btnTim.Click += (s, e) => LoadCT(txtTim.Text.Trim());

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;

            dgvCTPN.CellClick += dgvCTPN_CellClick;

            if (txtThanhTien != null) txtThanhTien.ReadOnly = true;
            if (txtTongTien != null) txtTongTien.ReadOnly = true;
        }

        private void FrmCTPhieuNhap_Load(object sender, EventArgs e)
        {
            SetupGrid();

            
            if (Controls.ContainsKey("txtMaPN")) ((TextBox)Controls["txtMaPN"]).Text = _maPN;

            LoadCT(_maPN);
        }

        private void SetupGrid()
        {
            dgvCTPN.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCTPN.MultiSelect = false;
            dgvCTPN.AllowUserToAddRows = false;
            dgvCTPN.ReadOnly = true;
            dgvCTPN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadCT(string maPN)
        {
            if (string.IsNullOrWhiteSpace(maPN))
            {
                MessageBox.Show("Nhập mã PN để xem chi tiết!");
                return;
            }

            _dt = SqlHelper.Query(@"
            SELECT MaPN AS [Mã PN], MaSach AS [Mã sách], SoLuong AS [Số lượng],
                   GiaNhap AS [Giá nhập], ThanhTien AS [Thành tiền]
            FROM CTPhieuNhap
            WHERE MaPN = @MaPN
            ORDER BY MaSach",
                new SqlParameter("@MaPN", maPN));

            dgvCTPN.DataSource = _dt;

            UpdateTongTien(maPN);
        }

        private void dgvCTPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvCTPN.Rows[e.RowIndex];

            txtMaSach.Text = row.Cells["Mã sách"].Value?.ToString();
            txtSoLuong.Text = row.Cells["Số lượng"].Value?.ToString();
            txtGiaNhap.Text = row.Cells["Giá nhập"].Value?.ToString();

            if (txtThanhTien != null)
                txtThanhTien.Text = row.Cells["Thành tiền"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateLine(out int sl, out decimal giaNhap)) return;

            // check PN tồn tại
            var pnExist = SqlHelper.Scalar("SELECT COUNT(*) FROM PhieuNhap WHERE MaPN=@id",
                new SqlParameter("@id", _maPN));
            if (Convert.ToInt32(pnExist) == 0)
            {
                MessageBox.Show("Phiếu nhập chưa tồn tại! Hãy tạo PN trước.");
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

            // check trùng dòng (MaPN, MaSach)
            var existLine = SqlHelper.Scalar("SELECT COUNT(*) FROM CTPhieuNhap WHERE MaPN=@pn AND MaSach=@ms",
                new SqlParameter("@pn", _maPN),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));
            if (Convert.ToInt32(existLine) > 0)
            {
                MessageBox.Show("Sách này đã có trong phiếu nhập! Hãy dùng Sửa để đổi SL/Giá.");
                return;
            }

            // Insert chi tiết
            SqlHelper.Execute(@"
            INSERT INTO CTPhieuNhap(MaPN, MaSach, SoLuong, GiaNhap)
            VALUES(@MaPN,@MaSach,@SoLuong,@GiaNhap)",
                new SqlParameter("@MaPN", _maPN),
                new SqlParameter("@MaSach", txtMaSach.Text.Trim()),
                new SqlParameter("@SoLuong", sl),
                new SqlParameter("@GiaNhap", giaNhap)
            );

            // cộng tồn kho
            SqlHelper.Execute(@"
            UPDATE Sach SET SoLuongTon = SoLuongTon + @sl
            WHERE MaSach=@ms",
                new SqlParameter("@sl", sl),
                new SqlParameter("@ms", txtMaSach.Text.Trim())
            );

            LoadCT(_maPN);
            ClearLine();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateLine(out int newSL, out decimal newGia)) return;

            // lấy số lượng cũ để hoàn kho rồi cộng lại theo số mới
            var dtOld = SqlHelper.Query(@"
            SELECT SoLuong FROM CTPhieuNhap WHERE MaPN=@pn AND MaSach=@ms",
                new SqlParameter("@pn", _maPN),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            if (dtOld.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dòng chi tiết để sửa!");
                return;
            }

            int oldSL = Convert.ToInt32(dtOld.Rows[0]["SoLuong"]);

            // trừ kho theo số cũ (rollback về trạng thái trước khi nhập dòng này)
            SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon - @sl WHERE MaSach=@ms",
                new SqlParameter("@sl", oldSL),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            // cộng kho theo số mới
            SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon + @sl WHERE MaSach=@ms",
                new SqlParameter("@sl", newSL),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            // update chi tiết
            SqlHelper.Execute(@"
            UPDATE CTPhieuNhap
            SET SoLuong=@sl, GiaNhap=@gia
            WHERE MaPN=@pn AND MaSach=@ms",
                new SqlParameter("@sl", newSL),
                new SqlParameter("@gia", newGia),
                new SqlParameter("@pn", _maPN),
                new SqlParameter("@ms", txtMaSach.Text.Trim())
            );

            LoadCT(_maPN);
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

            // lấy số lượng để trừ kho (vì nhập đã cộng)
            var slObj = SqlHelper.Scalar("SELECT SoLuong FROM CTPhieuNhap WHERE MaPN=@pn AND MaSach=@ms",
                new SqlParameter("@pn", _maPN),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            if (slObj == null)
            {
                MessageBox.Show("Không tìm thấy dòng để xóa!");
                return;
            }

            int sl = Convert.ToInt32(slObj);

            SqlHelper.Execute("DELETE FROM CTPhieuNhap WHERE MaPN=@pn AND MaSach=@ms",
                new SqlParameter("@pn", _maPN),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            // trừ tồn kho
            SqlHelper.Execute("UPDATE Sach SET SoLuongTon = SoLuongTon - @sl WHERE MaSach=@ms",
                new SqlParameter("@sl", sl),
                new SqlParameter("@ms", txtMaSach.Text.Trim()));

            LoadCT(_maPN);
            ClearLine();
        }

        private void UpdateTongTien(string maPN)
        {
            var sum = SqlHelper.Scalar("SELECT ISNULL(SUM(ThanhTien),0) FROM CTPhieuNhap WHERE MaPN=@pn",
                new SqlParameter("@pn", maPN));

            decimal tong = Convert.ToDecimal(sum);

            SqlHelper.Execute("UPDATE PhieuNhap SET TongTien=@t WHERE MaPN=@pn",
                new SqlParameter("@t", tong),
                new SqlParameter("@pn", maPN));

            if (txtTongTien != null) txtTongTien.Text = tong.ToString("0");
        }

        private bool ValidateLine(out int sl, out decimal giaNhap)
        {
            sl = 0; giaNhap = 0;

            if (string.IsNullOrWhiteSpace(txtMaSach.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(txtGiaNhap.Text))
            {
                MessageBox.Show("Nhập đủ Mã sách / Số lượng / Giá nhập!");
                return false;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số > 0!");
                return false;
            }

            if (!decimal.TryParse(txtGiaNhap.Text.Trim(), out giaNhap) || giaNhap < 0)
            {
                MessageBox.Show("Giá nhập phải là số >= 0!");
                return false;
            }

            return true;
        }

        private void ClearLine()
        {
            txtMaSach.Clear();
            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            if (txtThanhTien != null) txtThanhTien.Clear();
            txtMaSach.Focus();
        }
    }
}