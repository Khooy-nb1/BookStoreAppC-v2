using BookStoreApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookStoreApp
{ 
public partial class FrmLogin : Form
{
    public FrmLogin()
    {
        InitializeComponent();

        btnLogin.Click += btnLogin_Click;
        btnSignup.Click += btnSignup_Click;
        btnThoat.Click += (s, e) => Application.Exit();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUser.Text) ||
            string.IsNullOrWhiteSpace(txtPass.Text))
        {
            MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!");
            return;
        }

        var count = SqlHelper.Scalar(@"
            SELECT COUNT(*)
            FROM TaiKhoan
            WHERE TenTK = @u AND MatKhau = @p",
            new SqlParameter("@u", txtUser.Text.Trim()),
            new SqlParameter("@p", txtPass.Text.Trim())
        );

        if (Convert.ToInt32(count) == 1)
        {
            MessageBox.Show("Đăng nhập thành công!");

            FrmMenu menu = new FrmMenu(txtUser.Text.Trim());
            menu.Show();

            this.Hide();
            menu.FormClosed += (s, e2) => this.Show();
        }
        else
        {
            MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
        }
    }

    private void btnSignup_Click(object sender, EventArgs e)
    {
        new FrmSignup().ShowDialog();
    }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            new FrmQuenMatKhau().ShowDialog();
        }
    }
}