using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace BookStoreApp
{
    public partial class FrmTheLoai : Form
    {
        private DataTable _dt;

        public FrmTheLoai()
        {
            InitializeComponent();

            Load += FrmTheLoai_Load;

            btnHienThi.Click += (s, e) => LoadTheLoai();
            btnTim.Click += (s, e) => Search();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnXoaTruong.Click += (s, e) => ClearInputs();

            dgvTheLoai.CellClick += dgvTheLoai_CellClick;

            btnTrangChu.Click += (s, e) => Close(); 
        }

        private void FrmTheLoai_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadTheLoai();
        }

        private void SetupGrid()
        {
            dgvTheLoai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTheLoai.MultiSelect = false;
            dgvTheLoai.AllowUserToAddRows = false;
            dgvTheLoai.ReadOnly = true;
            dgvTheLoai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadTheLoai()
        {
            _dt = SqlHelper.Query(@"
            SELECT MaTL AS [Mã thể loại], TenTL AS [Tên thể loại]
            FROM TheLoai
            ORDER BY MaTL");
            dgvTheLoai.DataSource = _dt;
        }

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvTheLoai.Rows[e.RowIndex];

            txtMaTL.Text = row.Cells["Mã thể loại"].Value?.ToString();
            txtTenTL.Text = row.Cells["Tên thể loại"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM TheLoai WHERE MaTL=@id",
                new SqlParameter("@id", txtMaTL.Text.Trim()));
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Mã thể loại đã tồn tại!");
                return;
            }

            int n = SqlHelper.Execute(@"
            INSERT INTO TheLoai(MaTL, TenTL)
            VALUES(@MaTL,@TenTL)",
                new SqlParameter("@MaTL", txtMaTL.Text.Trim()),
                new SqlParameter("@TenTL", txtTenTL.Text.Trim())
            );

            if (n > 0)
            {
                LoadTheLoai();
                ClearInputs();
                MessageBox.Show("Thêm thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTL.Text))
            {
                MessageBox.Show("Chọn 1 dòng để sửa!");
                return;
            }
            if (!ValidateInputs()) return;

            int n = SqlHelper.Execute(@"
            UPDATE TheLoai
            SET TenTL=@TenTL
            WHERE MaTL=@MaTL",
                new SqlParameter("@MaTL", txtMaTL.Text.Trim()),
                new SqlParameter("@TenTL", txtTenTL.Text.Trim())
            );

            if (n > 0)
            {
                LoadTheLoai();
                MessageBox.Show("Sửa thành công!");
            }
            else MessageBox.Show("Không tìm thấy mã thể loại để sửa!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTL.Text))
            {
                MessageBox.Show("Chọn 1 dòng để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa thể loại này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                int n = SqlHelper.Execute("DELETE FROM TheLoai WHERE MaTL=@id",
                    new SqlParameter("@id", txtMaTL.Text.Trim()));

                if (n > 0)
                {
                    LoadTheLoai();
                    ClearInputs();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa vì thể loại đang được dùng trong bảng Sách.\n" + ex.Message);
            }
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();

            _dt = SqlHelper.Query(@"
            SELECT MaTL AS [Mã thể loại], TenTL AS [Tên thể loại]
            FROM TheLoai
            WHERE MaTL LIKE @k OR TenTL LIKE @k
            ORDER BY MaTL",
                new SqlParameter("@k", "%" + key + "%"));

            dgvTheLoai.DataSource = _dt;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaTL.Text) ||
                string.IsNullOrWhiteSpace(txtTenTL.Text))
            {
                MessageBox.Show("Mã thể loại và Tên thể loại không được để trống!");
                return false;
            }

            if (txtMaTL.Text.Trim().Length > 10)
            {
                MessageBox.Show("Mã thể loại tối đa 10 ký tự!");
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtMaTL.Clear();
            txtTenTL.Clear();
            txtMaTL.Focus();
        }
    }
}