using BookStoreApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace BookStoreApp
{
    public partial class FrmTacGia : Form
    {
        private DataTable _dt;

        public FrmTacGia()
        {
            InitializeComponent();

            Load += FrmTacGia_Load;

            btnHienThi.Click += (s, e) => LoadTacGia();
            btnTim.Click += (s, e) => Search();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnXoaTruong.Click += (s, e) => ClearInputs();

            dgvTacGia.CellClick += dgvTacGia_CellClick;

            lstFilter.SelectedIndexChanged += (s, e) => FilterByLienLac();

            btnXuatText.Click += (s, e) => ExportTxt();
            btnTrangChu.Click += (s, e) => Close(); // hoặc mở FrmMenu
        }

        private void FrmTacGia_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadTacGia();
            LoadFilter();
        }

        private void SetupGrid()
        {
            dgvTacGia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTacGia.MultiSelect = false;
            dgvTacGia.AllowUserToAddRows = false;
            dgvTacGia.ReadOnly = true;
            dgvTacGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadTacGia()
        {
            _dt = SqlHelper.Query(@"
            SELECT MaTG, TenTG, LienLac
            FROM TacGia
            ORDER BY MaTG");
            dgvTacGia.DataSource = _dt;
        }

        private void LoadFilter()
        {
            // Lọc theo LiênLac (giống ảnh: Việt Nam, Đức, Nhật Bản...)
            var dt = SqlHelper.Query(@"
            SELECT DISTINCT ISNULL(NULLIF(LTRIM(RTRIM(LienLac)),''), N'Khác') AS LienLac
            FROM TacGia
            ORDER BY LienLac");

            lstFilter.Items.Clear();
            foreach (DataRow r in dt.Rows) lstFilter.Items.Add(r["LienLac"].ToString());
        }

        private void dgvTacGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvTacGia.Rows[e.RowIndex];

            txtMaTG.Text = row.Cells["MaTG"].Value?.ToString();
            txtTenTG.Text = row.Cells["TenTG"].Value?.ToString();
            txtLienLac.Text = row.Cells["LienLac"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM TacGia WHERE MaTG=@id",
                new SqlParameter("@id", txtMaTG.Text.Trim()));
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Mã tác giả đã tồn tại!");
                return;
            }

            int n = SqlHelper.Execute(@"
            INSERT INTO TacGia(MaTG, TenTG, LienLac)
            VALUES(@MaTG,@TenTG,@LienLac)",
                new SqlParameter("@MaTG", txtMaTG.Text.Trim()),
                new SqlParameter("@TenTG", txtTenTG.Text.Trim()),
                new SqlParameter("@LienLac", (object)txtLienLac.Text.Trim() ?? DBNull.Value)
            );

            if (n > 0)
            {
                LoadTacGia();
                LoadFilter();
                ClearInputs();
                MessageBox.Show("Thêm thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTG.Text))
            {
                MessageBox.Show("Chọn 1 dòng để sửa!");
                return;
            }
            if (!ValidateInputs()) return;

            int n = SqlHelper.Execute(@"
            UPDATE TacGia
            SET TenTG=@TenTG, LienLac=@LienLac
            WHERE MaTG=@MaTG",
                new SqlParameter("@MaTG", txtMaTG.Text.Trim()),
                new SqlParameter("@TenTG", txtTenTG.Text.Trim()),
                new SqlParameter("@LienLac", (object)txtLienLac.Text.Trim() ?? DBNull.Value)
            );

            if (n > 0)
            {
                LoadTacGia();
                LoadFilter();
                MessageBox.Show("Sửa thành công!");
            }
            else MessageBox.Show("Không tìm thấy mã tác giả để sửa!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTG.Text))
            {
                MessageBox.Show("Chọn 1 dòng để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa tác giả này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                int n = SqlHelper.Execute("DELETE FROM TacGia WHERE MaTG=@id",
                    new SqlParameter("@id", txtMaTG.Text.Trim()));

                if (n > 0)
                {
                    LoadTacGia();
                    LoadFilter();
                    ClearInputs();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa vì tác giả đang được dùng trong bảng Sách.\n" + ex.Message);
            }
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();

            _dt = SqlHelper.Query(@"
            SELECT MaTG, TenTG, LienLac
            FROM TacGia
            WHERE MaTG LIKE @k OR TenTG LIKE @k OR LienLac LIKE @k
            ORDER BY MaTG",
                new SqlParameter("@k", "%" + key + "%"));

            dgvTacGia.DataSource = _dt;
        }

        private void FilterByLienLac()
        {
            if (lstFilter.SelectedItem == null) return;
            string ll = lstFilter.SelectedItem.ToString();

            if (ll == "Khác")
            {
                _dt = SqlHelper.Query(@"
                SELECT MaTG, TenTG, LienLac
                FROM TacGia
                WHERE LienLac IS NULL OR LTRIM(RTRIM(LienLac))=''
                ORDER BY MaTG");
            }
            else
            {
                _dt = SqlHelper.Query(@"
                SELECT MaTG, TenTG, LienLac
                FROM TacGia
                WHERE LTRIM(RTRIM(LienLac)) = @ll
                ORDER BY MaTG",
                    new SqlParameter("@ll", ll));
            }

            dgvTacGia.DataSource = _dt;
        }

        private void ExportTxt()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text file (*.txt)|*.txt";
                sfd.FileName = "TacGia.txt";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                StringBuilder sb = new StringBuilder();

                // Header
                for (int i = 0; i < dgvTacGia.Columns.Count; i++)
                {
                    sb.Append(dgvTacGia.Columns[i].HeaderText);
                    sb.Append(i == dgvTacGia.Columns.Count - 1 ? "\n" : "\t");
                }

                // Rows
                foreach (DataGridViewRow r in dgvTacGia.Rows)
                {
                    if (r.IsNewRow) continue;
                    for (int i = 0; i < dgvTacGia.Columns.Count; i++)
                    {
                        sb.Append(r.Cells[i].Value != null ? r.Cells[i].Value.ToString() : "");
                        sb.Append(i == dgvTacGia.Columns.Count - 1 ? "\n" : "\t");
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất file thành công!");
            }
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaTG.Text) ||
                string.IsNullOrWhiteSpace(txtTenTG.Text))
            {
                MessageBox.Show("Mã tác giả và Tên tác giả không được để trống!");
                return false;
            }

            if (txtMaTG.Text.Trim().Length > 10)
            {
                MessageBox.Show("Mã tác giả tối đa 10 ký tự!");
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtMaTG.Clear();
            txtTenTG.Clear();
            txtLienLac.Clear();
            txtMaTG.Focus();
        }
    }
}