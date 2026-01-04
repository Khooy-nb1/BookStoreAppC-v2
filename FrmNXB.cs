using BookStoreApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace BookStoreApp
{
    public partial class FrmNXB : Form
    {
        private DataTable _dt;

        public FrmNXB()
        {
            InitializeComponent();

            Load += FrmNXB_Load;

            dgvNXB.CellClick += dgvNXB_CellClick;

            // Nút hiển thị / load
            if (btnHienThi != null) btnHienThi.Click += (s, e) => LoadNXB();
            if (btnLoad != null) btnLoad.Click += (s, e) => LoadNXB();

            btnTim.Click += (s, e) => Search();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;

            btnXuatText.Click += (s, e) => ExportTxt();
            btnTrangChu.Click += (s, e) => Close(); // hoặc mở FrmMenu
        }

        private void FrmNXB_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadNXB();
        }

        private void SetupGrid()
        {
            dgvNXB.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNXB.MultiSelect = false;
            dgvNXB.AllowUserToAddRows = false;
            dgvNXB.ReadOnly = true;
            dgvNXB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadNXB()
        {
            _dt = SqlHelper.Query(@"
            SELECT MaNXB, TenNXB, DiaChi, SDT
            FROM NhaXuatBan
            ORDER BY MaNXB");
            dgvNXB.DataSource = _dt;
        }

        private void dgvNXB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvNXB.Rows[e.RowIndex];

            txtMaNXB.Text = row.Cells["MaNXB"].Value?.ToString();
            txtTenNXB.Text = row.Cells["TenNXB"].Value?.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
            txtSDT.Text = row.Cells["SDT"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM NhaXuatBan WHERE MaNXB=@id",
                new SqlParameter("@id", txtMaNXB.Text.Trim()));
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Mã NXB đã tồn tại!");
                return;
            }

            int n = SqlHelper.Execute(@"
            INSERT INTO NhaXuatBan(MaNXB, TenNXB, DiaChi, SDT)
            VALUES(@MaNXB,@TenNXB,@DiaChi,@SDT)",
                new SqlParameter("@MaNXB", txtMaNXB.Text.Trim()),
                new SqlParameter("@TenNXB", txtTenNXB.Text.Trim()),
                new SqlParameter("@DiaChi", (object)txtDiaChi.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@SDT", (object)txtSDT.Text.Trim() ?? DBNull.Value)
            );

            if (n > 0)
            {
                LoadNXB();
                ClearInputs();
                MessageBox.Show("Thêm thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNXB.Text))
            {
                MessageBox.Show("Chọn 1 dòng để sửa!");
                return;
            }
            if (!ValidateInputs()) return;

            int n = SqlHelper.Execute(@"
            UPDATE NhaXuatBan
            SET TenNXB=@TenNXB, DiaChi=@DiaChi, SDT=@SDT
            WHERE MaNXB=@MaNXB",
                new SqlParameter("@MaNXB", txtMaNXB.Text.Trim()),
                new SqlParameter("@TenNXB", txtTenNXB.Text.Trim()),
                new SqlParameter("@DiaChi", (object)txtDiaChi.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@SDT", (object)txtSDT.Text.Trim() ?? DBNull.Value)
            );

            if (n > 0)
            {
                LoadNXB();
                MessageBox.Show("Sửa thành công!");
            }
            else MessageBox.Show("Không tìm thấy mã NXB để sửa!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNXB.Text))
            {
                MessageBox.Show("Chọn 1 dòng để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa nhà xuất bản này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                int n = SqlHelper.Execute("DELETE FROM NhaXuatBan WHERE MaNXB=@id",
                    new SqlParameter("@id", txtMaNXB.Text.Trim()));

                if (n > 0)
                {
                    LoadNXB();
                    ClearInputs();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa vì NXB đang được dùng trong bảng Sách/Phiếu nhập.\n" + ex.Message);
            }
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();

            _dt = SqlHelper.Query(@"
            SELECT MaNXB, TenNXB, DiaChi, SDT
            FROM NhaXuatBan
            WHERE MaNXB LIKE @k OR TenNXB LIKE @k OR DiaChi LIKE @k OR SDT LIKE @k
            ORDER BY MaNXB",
                new SqlParameter("@k", "%" + key + "%"));

            dgvNXB.DataSource = _dt;
        }

        private void ExportTxt()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text file (*.txt)|*.txt";
                sfd.FileName = "NhaXuatBan.txt";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                StringBuilder sb = new StringBuilder();

                // Header
                for (int i = 0; i < dgvNXB.Columns.Count; i++)
                {
                    sb.Append(dgvNXB.Columns[i].HeaderText);
                    sb.Append(i == dgvNXB.Columns.Count - 1 ? "\n" : "\t");
                }

                // Rows
                foreach (DataGridViewRow r in dgvNXB.Rows)
                {
                    if (r.IsNewRow) continue;
                    for (int i = 0; i < dgvNXB.Columns.Count; i++)
                    {
                        sb.Append(r.Cells[i].Value != null ? r.Cells[i].Value.ToString() : "");
                        sb.Append(i == dgvNXB.Columns.Count - 1 ? "\n" : "\t");
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất file thành công!");
            }
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaNXB.Text) ||
                string.IsNullOrWhiteSpace(txtTenNXB.Text))
            {
                MessageBox.Show("Mã NXB và Tên NXB không được để trống!");
                return false;
            }

            if (txtMaNXB.Text.Trim().Length > 10)
            {
                MessageBox.Show("Mã NXB tối đa 10 ký tự!");
                return false;
            }

            // SDT chỉ cho số (nếu có nhập)
            var sdt = txtSDT.Text.Trim();
            if (!string.IsNullOrEmpty(sdt))
            {
                foreach (char c in sdt)
                {
                    if (!char.IsDigit(c) && c != '+' && c != ' ' && c != '-')
                    {
                        MessageBox.Show("SĐT chỉ nên gồm số (có thể có +, -, khoảng trắng)!");
                        return false;
                    }
                }
            }

            return true;
        }

        private void ClearInputs()
        {
            txtMaNXB.Clear();
            txtTenNXB.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtMaNXB.Focus();
        }
    }
}