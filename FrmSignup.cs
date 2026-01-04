using BookStoreApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace BookStoreApp
{
    public partial class FrmSignup : Form
    {
        public FrmSignup()
        {
            InitializeComponent();

            btnDangKy.Click += btnDangKy_Click;
            btnHuy.Click += (s, e) => Close();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text) ||
                string.IsNullOrWhiteSpace(txtRePass.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (txtPass.Text != txtRePass.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
                return;
            }

            var exist = SqlHelper.Scalar(
                "SELECT COUNT(*) FROM TaiKhoan WHERE TenTK=@u",
                new SqlParameter("@u", txtUser.Text.Trim())
            );

            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show("Tên tài khoản đã tồn tại!");
                return;
            }

            SqlHelper.Execute(
                "INSERT INTO TaiKhoan(TenTK, MatKhau) VALUES(@u,@p)",
                new SqlParameter("@u", txtUser.Text.Trim()),
                new SqlParameter("@p", txtPass.Text.Trim())
            );

            MessageBox.Show("Đăng ký thành công! Quay lại đăng nhập.");
            Close();
        }
    }
}