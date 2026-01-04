using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmQuenMatKhau : Form
    {
        public FrmQuenMatKhau()
        {
            InitializeComponent();
            btnDoiMK.Click += btnDoiMK_Click;
            btnThoat.Click += (s, e) => Close();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mk1 = txtNewPass.Text;
            string mk2 = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(user) ||
                string.IsNullOrWhiteSpace(mk1) ||
                string.IsNullOrWhiteSpace(mk2))
            {
                MessageBox.Show("Vui lòng nhập đủ Username và mật khẩu mới!");
                return;
            }

            if (mk1 != mk2)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp!");
                return;
            }

            // Nếu DB có cột Email thì kiểm tra, không có thì bỏ qua
            bool hasEmail = false;
            try
            {
                var c = SqlHelper.Scalar(@"
                    SELECT COUNT(*)
                    FROM sys.columns 
                    WHERE object_id = OBJECT_ID('dbo.TaiKhoan') AND name = 'Email'");
                hasEmail = Convert.ToInt32(c) > 0;
            }
            catch { hasEmail = false; }

            object exist;
            if (hasEmail && !string.IsNullOrWhiteSpace(email))
            {
                exist = SqlHelper.Scalar("SELECT COUNT(*) FROM TaiKhoan WHERE TenTK=@u AND Email=@e",
                    new SqlParameter("@u", user),
                    new SqlParameter("@e", email));
                if (Convert.ToInt32(exist) == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản hoặc Email không đúng!");
                    return;
                }
            }
            else
            {
                exist = SqlHelper.Scalar("SELECT COUNT(*) FROM TaiKhoan WHERE TenTK=@u",
                    new SqlParameter("@u", user));
                if (Convert.ToInt32(exist) == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản!");
                    return;
                }
            }

            int n = SqlHelper.Execute("UPDATE TaiKhoan SET MatKhau=@p WHERE TenTK=@u",
                new SqlParameter("@p", mk1),
                new SqlParameter("@u", user));

            if (n > 0)
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
                Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!");
            }
        }
    }
}
