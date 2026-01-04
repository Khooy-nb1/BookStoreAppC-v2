using System;
using System.Windows.Forms;

namespace BookStoreApp
{
    public partial class FrmThanhToanQR : Form
    {
        private readonly string _maHD;
        private readonly string _tongTienText;

        public FrmThanhToanQR(string maHD, string tongTienText)
        {
            InitializeComponent();
            _maHD = maHD;
            _tongTienText = tongTienText;

            Load += FrmThanhToanQR_Load;
            btnDaThanhToan.Click += btnDaThanhToan_Click;
            btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void FrmThanhToanQR_Load(object sender, EventArgs e)
        {
            lblInfo.Text = $"Quét QR để thanh toán\nMã HĐ: {_maHD}\n{_tongTienText}";
            
        }

        private void btnDaThanhToan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xác nhận thanh toán QR thành công!", "Thành công");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
