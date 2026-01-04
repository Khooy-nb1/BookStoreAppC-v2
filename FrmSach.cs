using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BookStoreApp;

namespace BookStoreApp
{
    public partial class FrmSach : Form
    {
        private DataTable _dt;

        public FrmSach()
        {
            InitializeComponent();
            Load += FrmSach_Load;

            dgvSach.CellClick += dgvSach_CellClick;
            btnHienThi.Click += (s, e) => LoadSach();
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnXoaTruong.Click += (s, e) => ClearInputs();
            btnTim.Click += (s, e) => Search();

            lstLoc.SelectedIndexChanged += (s, e) => FilterByTheLoai();
        }

        private void FrmSach_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadLocTheLoai();
            LoadSach();
        }

        private void SetupGrid()
        {
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.MultiSelect = false;
            dgvSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSach.AllowUserToAddRows = false;
            dgvSach.ReadOnly = true;
        }

        private void LoadSach()
        {
            _dt = SqlHelper.Query(@"
                SELECT MaSach, TenSach, SoLuongTon, MaTL AS [Mã thể loại], MaNXB AS [Mã NXB], MaTG AS [Mã tác giả]
                FROM Sach
                ORDER BY MaSach");
            dgvSach.DataSource = _dt;
        }

        private void LoadLocTheLoai()
        {
            var dtTL = SqlHelper.Query("SELECT TenTL FROM TheLoai ORDER BY TenTL");
            lstLoc.Items.Clear();
            foreach (DataRow r in dtTL.Rows) lstLoc.Items.Add(r["TenTL"].ToString());
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvSach.Rows[e.RowIndex];
            txtMaSach.Text = row.Cells["MaSach"].Value?.ToString();
            txtTenSach.Text = row.Cells["TenSach"].Value?.ToString();
            txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value?.ToString();
            txtMaTL.Text = row.Cells["Mã thể loại"].Value?.ToString();
            txtMaNXB.Text = row.Cells["Mã NXB"].Value?.ToString();
            txtMaTG.Text = row.Cells["Mã tác giả"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // check trùng mã
            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM Sach WHERE MaSach=@id",
                new SqlParameter("@id", txtMaSach.Text.Trim()));
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Mã sách đã tồn tại!");
                return;
            }

            int n = SqlHelper.Execute(@"
                INSERT INTO Sach(MaSach, TenSach, SoLuongTon, MaTL, MaNXB, MaTG)
                VALUES(@MaSach,@TenSach,@SoLuong,@MaTL,@MaNXB,@MaTG)",
                new SqlParameter("@MaSach", txtMaSach.Text.Trim()),
                new SqlParameter("@TenSach", txtTenSach.Text.Trim()),
                new SqlParameter("@SoLuong", int.Parse(txtSoLuongTon.Text.Trim())),
                new SqlParameter("@MaTL", txtMaTL.Text.Trim()),
                new SqlParameter("@MaNXB", txtMaNXB.Text.Trim()),
                new SqlParameter("@MaTG", txtMaTG.Text.Trim())
            );

            if (n > 0) { LoadSach(); ClearInputs(); MessageBox.Show("Thêm thành công!"); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Chọn 1 dòng để sửa!");
                return;
            }
            if (!ValidateInputs()) return;

            int n = SqlHelper.Execute(@"
                UPDATE Sach
                SET TenSach=@TenSach, SoLuongTon=@SoLuong, MaTL=@MaTL, MaNXB=@MaNXB, MaTG=@MaTG
                WHERE MaSach=@MaSach",
                new SqlParameter("@MaSach", txtMaSach.Text.Trim()),
                new SqlParameter("@TenSach", txtTenSach.Text.Trim()),
                new SqlParameter("@SoLuong", int.Parse(txtSoLuongTon.Text.Trim())),
                new SqlParameter("@MaTL", txtMaTL.Text.Trim()),
                new SqlParameter("@MaNXB", txtMaNXB.Text.Trim()),
                new SqlParameter("@MaTG", txtMaTG.Text.Trim())
            );

            if (n > 0) { LoadSach(); MessageBox.Show("Sửa thành công!"); }
            else MessageBox.Show("Không tìm thấy mã sách để sửa!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Chọn 1 dòng để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            // Nếu sách đã có trong hóa đơn/phiếu nhập => FK sẽ chặn. Bạn có thể báo lỗi rõ.
            try
            {
                int n = SqlHelper.Execute("DELETE FROM Sach WHERE MaSach=@id",
                    new SqlParameter("@id", txtMaSach.Text.Trim()));
                if (n > 0) { LoadSach(); ClearInputs(); MessageBox.Show("Xóa thành công!"); }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa vì sách đang được dùng ở Hóa đơn/Phiếu nhập.\n" + ex.Message);
            }
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();
            _dt = SqlHelper.Query(@"
                SELECT MaSach, TenSach, SoLuongTon, MaTL AS [Mã thể loại], MaNXB AS [Mã NXB], MaTG AS [Mã tác giả]
                FROM Sach
                WHERE MaSach LIKE @k OR TenSach LIKE @k
                ORDER BY MaSach",
                new SqlParameter("@k", "%" + key + "%"));
            dgvSach.DataSource = _dt;
        }

        private void FilterByTheLoai()
        {
            if (lstLoc.SelectedItem == null) return;
            string tenTL = lstLoc.SelectedItem.ToString();

            _dt = SqlHelper.Query(@"
                SELECT s.MaSach, s.TenSach, s.SoLuongTon,
                       s.MaTL AS [Mã thể loại], s.MaNXB AS [Mã NXB], s.MaTG AS [Mã tác giả]
                FROM Sach s
                JOIN TheLoai tl ON s.MaTL = tl.MaTL
                WHERE tl.TenTL = @ten
                ORDER BY s.MaSach",
                new SqlParameter("@ten", tenTL));
            dgvSach.DataSource = _dt;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text) ||
                string.IsNullOrWhiteSpace(txtTenSach.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuongTon.Text) ||
                string.IsNullOrWhiteSpace(txtMaTL.Text) ||
                string.IsNullOrWhiteSpace(txtMaNXB.Text) ||
                string.IsNullOrWhiteSpace(txtMaTG.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (!int.TryParse(txtSoLuongTon.Text.Trim(), out int sl) || sl < 0)
            {
                MessageBox.Show("Số lượng tồn phải là số >= 0!");
                return false;
            }

            // Check FK tồn tại
            if (Convert.ToInt32(SqlHelper.Scalar("SELECT COUNT(*) FROM TheLoai WHERE MaTL=@x",
                new SqlParameter("@x", txtMaTL.Text.Trim()))) == 0)
            {
                MessageBox.Show("Mã thể loại không tồn tại!");
                return false;
            }

            if (Convert.ToInt32(SqlHelper.Scalar("SELECT COUNT(*) FROM NhaXuatBan WHERE MaNXB=@x",
                new SqlParameter("@x", txtMaNXB.Text.Trim()))) == 0)
            {
                MessageBox.Show("Mã NXB không tồn tại!");
                return false;
            }

            if (Convert.ToInt32(SqlHelper.Scalar("SELECT COUNT(*) FROM TacGia WHERE MaTG=@x",
                new SqlParameter("@x", txtMaTG.Text.Trim()))) == 0)
            {
                MessageBox.Show("Mã tác giả không tồn tại!");
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtSoLuongTon.Clear();
            txtMaTL.Clear();
            txtMaNXB.Clear();
            txtMaTG.Clear();
            txtMaSach.Focus();
        }
    }
}
