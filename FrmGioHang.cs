using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmGioHang : Form
    {
        // Item trong giỏ
        public class CartItem
        {
            public string MaSach { get; set; }
            public string TenSach { get; set; }
            public decimal DonGia { get; set; }
            public int SoLuong { get; set; }
            public decimal ThanhTien => DonGia * SoLuong;
        }

        private readonly List<CartItem> _cart = new List<CartItem>();
        private DataTable _dtSach;
        private readonly string _username;

        public FrmGioHang(string username = null)
        {
            InitializeComponent();
            _username = username;

            Load += FrmGioHang_Load;

            btnTaiSach.Click += (s, e) => LoadSach();
            btnTimSach.Click += (s, e) => SearchSach();

            dgvSach.CellClick += dgvSach_CellClick;

            btnThemVaoGio.Click += (s, e) => AddToCart();
            btnXoaDong.Click += (s, e) => RemoveSelectedCartItem();
            btnXoaGio.Click += (s, e) => ClearCart();
            btnThanhToan.Click += (s, e) => OpenThanhToan();
        }

        private void FrmGioHang_Load(object sender, EventArgs e)
        {
            SetupGrids();
            LoadSach();
            BindCart();
        }

        private void SetupGrids()
        {
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.MultiSelect = false;
            dgvSach.ReadOnly = true;
            dgvSach.AllowUserToAddRows = false;
            dgvSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGioHang.MultiSelect = false;
            dgvGioHang.ReadOnly = true;
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            nudSoLuong.Minimum = 1;
            nudSoLuong.Maximum = 9999;
            nudSoLuong.Value = 1;
        }

        private void LoadSach()
        {
            _dtSach = SqlHelper.Query(@"
                SELECT MaSach, TenSach, SoLuongTon, GiaBan
                FROM Sach
                ORDER BY MaSach
            ");
            dgvSach.DataSource = _dtSach;
        }

        private void SearchSach()
        {
            string key = txtTimSach.Text.Trim();
            _dtSach = SqlHelper.Query(@"
                SELECT MaSach, TenSach, SoLuongTon, GiaBan
                FROM Sach
                WHERE MaSach LIKE @k OR TenSach LIKE @k
                ORDER BY MaSach
            ", new SqlParameter("@k", "%" + key + "%"));

            dgvSach.DataSource = _dtSach;
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvSach.Rows[e.RowIndex];

            txtMaSach.Text = row.Cells["MaSach"].Value?.ToString();
            txtTenSach.Text = row.Cells["TenSach"].Value?.ToString();
            txtTon.Text = row.Cells["SoLuongTon"].Value?.ToString();
            txtDonGia.Text = row.Cells["GiaBan"].Value?.ToString();
        }

        private void AddToCart()
        {
            string ma = txtMaSach.Text.Trim();
            if (string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Chọn sách trước!");
                return;
            }

            int ton;
            if (!int.TryParse(txtTon.Text.Trim(), out ton)) ton = 0;

            int sl = (int)nudSoLuong.Value;
            if (sl <= 0)
            {
                MessageBox.Show("Số lượng phải > 0");
                return;
            }

            if (sl > ton)
            {
                MessageBox.Show("Số lượng mua vượt quá tồn kho!");
                return;
            }

            decimal donGia;
            if (!decimal.TryParse(txtDonGia.Text.Trim(), out donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ (cần cột GiaBan trong bảng Sach).");
                return;
            }

            var exist = _cart.FirstOrDefault(x => x.MaSach == ma);
            if (exist != null)
            {
                if (exist.SoLuong + sl > ton)
                {
                    MessageBox.Show("Tổng số lượng trong giỏ vượt quá tồn kho!");
                    return;
                }
                exist.SoLuong += sl;
            }
            else
            {
                _cart.Add(new CartItem
                {
                    MaSach = ma,
                    TenSach = txtTenSach.Text.Trim(),
                    DonGia = donGia,
                    SoLuong = sl
                });
            }

            BindCart();
        }

        private void BindCart()
        {
            var dt = new DataTable();
            dt.Columns.Add("MaSach");
            dt.Columns.Add("TenSach");
            dt.Columns.Add("DonGia", typeof(decimal));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("ThanhTien", typeof(decimal));

            foreach (var it in _cart)
            {
                dt.Rows.Add(it.MaSach, it.TenSach, it.DonGia, it.SoLuong, it.ThanhTien);
            }

            dgvGioHang.DataSource = dt;

            lblTongTien.Text = "Tổng: " + _cart.Sum(x => x.ThanhTien).ToString("N0") + " đ";
            lblSoDong.Text = "Số dòng: " + _cart.Count;
        }

        private void RemoveSelectedCartItem()
        {
            if (dgvGioHang.SelectedRows.Count == 0) return;
            string ma = dgvGioHang.SelectedRows[0].Cells["MaSach"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(ma)) return;

            _cart.RemoveAll(x => x.MaSach == ma);
            BindCart();
        }

        private void ClearCart()
        {
            _cart.Clear();
            BindCart();
        }

        private void OpenThanhToan()
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!");
                return;
            }

            // clone list để tránh sửa lẫn
            var items = _cart.Select(x => new CartItem
            {
                MaSach = x.MaSach,
                TenSach = x.TenSach,
                DonGia = x.DonGia,
                SoLuong = x.SoLuong
            }).ToList();

            using (var f = new FrmThanhToan(items, _username))
            {
                var ok = f.ShowDialog();
                if (ok == DialogResult.OK)
                {
                    // thanh toán xong -> clear giỏ + reload tồn
                    ClearCart();
                    LoadSach();
                }
            }
        }
    }
}
