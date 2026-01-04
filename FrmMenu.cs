using System;
using System.Windows.Forms;
using BookStoreApp;

namespace BookStoreApp
{
    public partial class FrmMenu : Form
    {
        private readonly string _username;

        public FrmMenu(string username = "")
        {
            InitializeComponent();
            _username = username;

            Load += FrmMenu_Load;

            btnTacGia.Click += (s, e) => new FrmTacGia().ShowDialog();
            btnNXB.Click += (s, e) => new FrmNXB().ShowDialog();
            btnTheLoai.Click += (s, e) => new FrmTheLoai().ShowDialog();
            btnSach.Click += (s, e) => new FrmSach().ShowDialog();

            btnHoaDon.Click += (s, e) => new FrmHoaDon().ShowDialog();
            btnPhieuNhap.Click += (s, e) => new FrmPhieuNhap().ShowDialog();

            btnDangXuat.Click += btnDangXuat_Click;
            btnThoat.Click += (s, e) => Application.Exit();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            if (Controls.ContainsKey("lblUser"))
            {
                var lbl = (Label)Controls["lblUser"];
                lbl.Text = string.IsNullOrWhiteSpace(_username) ? "Xin chào!" : $"Xin chào, {_username}";
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đăng xuất về màn hình đăng nhập?", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            // Mở login lại
            var f = new FrmLogin();
            f.Show();

            // Đóng menu
            this.Close();
        }

        private void btnBanSach_Click(object sender, EventArgs e)
        {
            new FrmGioHang(_username).ShowDialog();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            new FrmKhachHang().ShowDialog();
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            new FrmLichSuHoaDon().ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            new FrmThongKeTopBan().ShowDialog();
        }
    }
}