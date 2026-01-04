namespace BookStoreApp
{
    partial class FrmLichSuHoaDon
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.btnHienThi = new System.Windows.Forms.Button();
            this.dgvHD = new System.Windows.Forms.DataGridView();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.btnXuatText = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Text = "Từ ngày";
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(66, 14);
            this.dtFrom.Size = new System.Drawing.Size(200, 20);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 18);
            this.label2.Text = "Đến ngày";
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(340, 14);
            this.dtTo.Size = new System.Drawing.Size(200, 20);
            // 
            // btnHienThi
            // 
            this.btnHienThi.BackColor = System.Drawing.Color.Cyan;
            this.btnHienThi.Location = new System.Drawing.Point(555, 13);
            this.btnHienThi.Size = new System.Drawing.Size(75, 23);
            this.btnHienThi.Text = "Hiển thị";
            this.btnHienThi.UseVisualStyleBackColor = false;
            // 
            // dgvHD
            // 
            this.dgvHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHD.Location = new System.Drawing.Point(12, 88);
            this.dgvHD.Size = new System.Drawing.Size(730, 310);
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(632, 52);
            this.txtTim.Size = new System.Drawing.Size(110, 20);
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(555, 50);
            this.btnTim.Size = new System.Drawing.Size(75, 23);
            this.btnTim.Text = "Tìm";
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.Location = new System.Drawing.Point(12, 407);
            this.btnChiTiet.Size = new System.Drawing.Size(110, 27);
            this.btnChiTiet.Text = "Chi tiết";
            // 
            // btnXuatText
            // 
            this.btnXuatText.Location = new System.Drawing.Point(130, 407);
            this.btnXuatText.Size = new System.Drawing.Size(110, 27);
            this.btnXuatText.Text = "Xuất TXT";
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Location = new System.Drawing.Point(248, 407);
            this.btnTrangChu.Size = new System.Drawing.Size(110, 27);
            this.btnTrangChu.Text = "Trang chủ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Text = "Mã HD chọn";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(86, 52);
            this.txtMaHD.Size = new System.Drawing.Size(150, 20);
            // 
            // FrmLichSuHoaDon
            // 
            this.ClientSize = new System.Drawing.Size(754, 447);
            this.Controls.Add(this.txtMaHD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTrangChu);
            this.Controls.Add(this.btnXuatText);
            this.Controls.Add(this.btnChiTiet);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.dgvHD);
            this.Controls.Add(this.btnHienThi);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.label1);
            this.Name = "FrmLichSuHoaDon";
            this.Text = "FrmLichSuHoaDon";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label label1, label2, label3;
        private System.Windows.Forms.DateTimePicker dtFrom, dtTo;
        private System.Windows.Forms.Button btnHienThi, btnTim, btnChiTiet, btnXuatText, btnTrangChu;
        private System.Windows.Forms.DataGridView dgvHD;
        private System.Windows.Forms.TextBox txtTim, txtMaHD;
    }
}
