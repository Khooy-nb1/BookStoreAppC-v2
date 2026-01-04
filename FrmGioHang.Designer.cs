namespace BookStoreApp
{
    partial class FrmGioHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvSach = new System.Windows.Forms.DataGridView();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.txtTimSach = new System.Windows.Forms.TextBox();
            this.btnTimSach = new System.Windows.Forms.Button();
            this.btnTaiSach = new System.Windows.Forms.Button();

            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.txtTon = new System.Windows.Forms.TextBox();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();

            this.btnThemVaoGio = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnXoaGio = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();

            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblSoDong = new System.Windows.Forms.Label();

            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.SuspendLayout();

            // dgvSach
            this.dgvSach.Location = new System.Drawing.Point(12, 52);
            this.dgvSach.Name = "dgvSach";
            this.dgvSach.Size = new System.Drawing.Size(560, 220);
            this.dgvSach.TabIndex = 0;

            // txtTimSach
            this.txtTimSach.Location = new System.Drawing.Point(12, 14);
            this.txtTimSach.Name = "txtTimSach";
            this.txtTimSach.Size = new System.Drawing.Size(270, 20);
            this.txtTimSach.TabIndex = 1;

            // btnTimSach
            this.btnTimSach.Location = new System.Drawing.Point(288, 12);
            this.btnTimSach.Name = "btnTimSach";
            this.btnTimSach.Size = new System.Drawing.Size(75, 23);
            this.btnTimSach.TabIndex = 2;
            this.btnTimSach.Text = "Tìm";

            // btnTaiSach
            this.btnTaiSach.Location = new System.Drawing.Point(369, 12);
            this.btnTaiSach.Name = "btnTaiSach";
            this.btnTaiSach.Size = new System.Drawing.Size(75, 23);
            this.btnTaiSach.TabIndex = 3;
            this.btnTaiSach.Text = "Tải";

            // labels + inputs
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(590, 55);
            this.label1.Text = "Mã sách";

            this.txtMaSach.Location = new System.Drawing.Point(660, 52);
            this.txtMaSach.Size = new System.Drawing.Size(150, 20);
            this.txtMaSach.ReadOnly = true;

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(590, 85);
            this.label2.Text = "Tên sách";

            this.txtTenSach.Location = new System.Drawing.Point(660, 82);
            this.txtTenSach.Size = new System.Drawing.Size(150, 20);
            this.txtTenSach.ReadOnly = true;

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(590, 115);
            this.label3.Text = "Tồn";

            this.txtTon.Location = new System.Drawing.Point(660, 112);
            this.txtTon.Size = new System.Drawing.Size(150, 20);
            this.txtTon.ReadOnly = true;

            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(590, 145);
            this.label4.Text = "Đơn giá";

            this.txtDonGia.Location = new System.Drawing.Point(660, 142);
            this.txtDonGia.Size = new System.Drawing.Size(150, 20);
            this.txtDonGia.ReadOnly = true;

            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(590, 175);
            this.label5.Text = "Số lượng";

            this.nudSoLuong.Location = new System.Drawing.Point(660, 172);
            this.nudSoLuong.Size = new System.Drawing.Size(150, 20);

            // buttons
            this.btnThemVaoGio.Location = new System.Drawing.Point(660, 205);
            this.btnThemVaoGio.Size = new System.Drawing.Size(150, 28);
            this.btnThemVaoGio.Text = "Thêm vào giỏ";

            // dgvGioHang
            this.dgvGioHang.Location = new System.Drawing.Point(12, 290);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.Size = new System.Drawing.Size(560, 220);
            this.dgvGioHang.TabIndex = 10;

            this.btnXoaDong.Location = new System.Drawing.Point(593, 290);
            this.btnXoaDong.Size = new System.Drawing.Size(217, 28);
            this.btnXoaDong.Text = "Xóa dòng";

            this.btnXoaGio.Location = new System.Drawing.Point(593, 324);
            this.btnXoaGio.Size = new System.Drawing.Size(217, 28);
            this.btnXoaGio.Text = "Xóa giỏ";

            this.btnThanhToan.Location = new System.Drawing.Point(593, 358);
            this.btnThanhToan.Size = new System.Drawing.Size(217, 36);
            this.btnThanhToan.Text = "Thanh toán";

            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(12, 520);
            this.lblTongTien.Text = "Tổng: 0 đ";

            this.lblSoDong.AutoSize = true;
            this.lblSoDong.Location = new System.Drawing.Point(200, 520);
            this.lblSoDong.Text = "Số dòng: 0";

            // Form
            this.ClientSize = new System.Drawing.Size(830, 555);
            this.Controls.Add(this.dgvSach);
            this.Controls.Add(this.txtTimSach);
            this.Controls.Add(this.btnTimSach);
            this.Controls.Add(this.btnTaiSach);

            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaSach);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTenSach);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudSoLuong);
            this.Controls.Add(this.btnThemVaoGio);

            this.Controls.Add(this.dgvGioHang);
            this.Controls.Add(this.btnXoaDong);
            this.Controls.Add(this.btnXoaGio);
            this.Controls.Add(this.btnThanhToan);

            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblSoDong);

            this.Name = "FrmGioHang";
            this.Text = "Giỏ hàng";

            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSach;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.TextBox txtTimSach;
        private System.Windows.Forms.Button btnTimSach;
        private System.Windows.Forms.Button btnTaiSach;

        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.TextBox txtTon;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.NumericUpDown nudSoLuong;

        private System.Windows.Forms.Button btnThemVaoGio;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnXoaGio;
        private System.Windows.Forms.Button btnThanhToan;

        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblSoDong;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
