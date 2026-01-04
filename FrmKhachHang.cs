using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmKhachHang : Form
    {
        private DataTable _dt;

        public FrmKhachHang()
        {
            InitializeComponent();

            Load += FrmKhachHang_Load;

            dgvKH.CellClick += dgvKH_CellClick;

            btnHienThi.Click += (s, e) => LoadKH();
            btnTim.Click += (s, e) => Search();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnXoaTruong.Click += (s, e) => ClearInputs();

            btnXuatText.Click += (s, e) => ExportTxt();
            btnTrangChu.Click += (s, e) => Close();
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadKH();
        }

        private void SetupGrid()
        {
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKH.MultiSelect = false;
            dgvKH.AllowUserToAddRows = false;
            dgvKH.ReadOnly = true;
            dgvKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadKH()
        {
            _dt = SqlHelper.Query(@"
                SELECT MaKH, TenKH, SDT, DiaChi, Email, DiemTichLuy
                FROM KhachHang
                ORDER BY MaKH");
            dgvKH.DataSource = _dt;
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvKH.Rows[e.RowIndex];

            txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
            txtTenKH.Text = row.Cells["TenKH"].Value?.ToString();
            txtSDT.Text = row.Cells["SDT"].Value?.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtDiem.Text = row.Cells["DiemTichLuy"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM KhachHang WHERE MaKH=@id",
                new SqlParameter("@id", txtMaKH.Text.Trim()));
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Mã KH đã tồn tại!");
                return;
            }

            int n = SqlHelper.Execute(@"
                INSERT INTO KhachHang(MaKH, TenKH, SDT, DiaChi, Email, DiemTichLuy)
                VALUES(@MaKH,@TenKH,@SDT,@DiaChi,@Email,@Diem)",
                new SqlParameter("@MaKH", txtMaKH.Text.Trim()),
                new SqlParameter("@TenKH", txtTenKH.Text.Trim()),
                new SqlParameter("@SDT", (object)txtSDT.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)txtDiaChi.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@Email", (object)txtEmail.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@Diem", int.Parse(txtDiem.Text.Trim()))
            );

            if (n > 0)
            {
                LoadKH();
                ClearInputs();
                MessageBox.Show("Thêm thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Chọn 1 dòng để sửa!");
                return;
            }
            if (!ValidateInputs()) return;

            int n = SqlHelper.Execute(@"
                UPDATE KhachHang
                SET TenKH=@TenKH, SDT=@SDT, DiaChi=@DiaChi, Email=@Email, DiemTichLuy=@Diem
                WHERE MaKH=@MaKH",
                new SqlParameter("@MaKH", txtMaKH.Text.Trim()),
                new SqlParameter("@TenKH", txtTenKH.Text.Trim()),
                new SqlParameter("@SDT", (object)txtSDT.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)txtDiaChi.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@Email", (object)txtEmail.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@Diem", int.Parse(txtDiem.Text.Trim()))
            );

            if (n > 0) { LoadKH(); MessageBox.Show("Sửa thành công!"); }
            else MessageBox.Show("Không tìm thấy mã KH để sửa!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Chọn 1 dòng để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                int n = SqlHelper.Execute("DELETE FROM KhachHang WHERE MaKH=@id",
                    new SqlParameter("@id", txtMaKH.Text.Trim()));
                if (n > 0)
                {
                    LoadKH();
                    ClearInputs();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa vì KH có liên quan hóa đơn.\n" + ex.Message);
            }
        }

        private void Search()
        {
            string key = txtTim.Text.Trim();
            _dt = SqlHelper.Query(@"
                SELECT MaKH, TenKH, SDT, DiaChi, Email, DiemTichLuy
                FROM KhachHang
                WHERE MaKH LIKE @k OR TenKH LIKE @k OR SDT LIKE @k OR Email LIKE @k
                ORDER BY MaKH",
                new SqlParameter("@k", "%" + key + "%"));
            dgvKH.DataSource = _dt;
        }

        private void ExportTxt()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text file (*.txt)|*.txt";
                sfd.FileName = "KhachHang.txt";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < dgvKH.Columns.Count; i++)
                {
                    sb.Append(dgvKH.Columns[i].HeaderText);
                    sb.Append(i == dgvKH.Columns.Count - 1 ? "\n" : "\t");
                }

                foreach (DataGridViewRow r in dgvKH.Rows)
                {
                    if (r.IsNewRow) continue;
                    for (int i = 0; i < dgvKH.Columns.Count; i++)
                    {
                        sb.Append(r.Cells[i].Value?.ToString());
                        sb.Append(i == dgvKH.Columns.Count - 1 ? "\n" : "\t");
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất file thành công!");
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Mã KH và Tên KH không được để trống!");
                return false;
            }

            int diem;
            if (!int.TryParse(txtDiem.Text.Trim(), out diem) || diem < 0)
            {
                MessageBox.Show("Điểm tích lũy phải là số >= 0!");
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtDiem.Text = "0";
            txtMaKH.Focus();
        }
    }
}
