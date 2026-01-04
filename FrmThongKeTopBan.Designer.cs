namespace BookStoreApp
{
    partial class FrmThongKeTopBan
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtTop = new System.Windows.Forms.TextBox();
            this.btnHienThi = new System.Windows.Forms.Button();
            this.dgvTop = new System.Windows.Forms.DataGridView();
            this.btnXuatText = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTop)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Từ ngày";
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(152, 79);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(200, 20);
            this.dtFrom.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(366, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Đến ngày";
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(426, 79);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(200, 20);
            this.dtTo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(641, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Top";
            // 
            // txtTop
            // 
            this.txtTop.Location = new System.Drawing.Point(671, 80);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new System.Drawing.Size(60, 20);
            this.txtTop.TabIndex = 4;
            this.txtTop.Text = "10";
            // 
            // btnHienThi
            // 
            this.btnHienThi.BackColor = System.Drawing.Color.Cyan;
            this.btnHienThi.Location = new System.Drawing.Point(16, 80);
            this.btnHienThi.Name = "btnHienThi";
            this.btnHienThi.Size = new System.Drawing.Size(75, 23);
            this.btnHienThi.TabIndex = 3;
            this.btnHienThi.Text = "Hiển thị";
            this.btnHienThi.UseVisualStyleBackColor = false;
            // 
            // dgvTop
            // 
            this.dgvTop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTop.Location = new System.Drawing.Point(12, 109);
            this.dgvTop.Name = "dgvTop";
            this.dgvTop.Size = new System.Drawing.Size(723, 256);
            this.dgvTop.TabIndex = 2;
            // 
            // btnXuatText
            // 
            this.btnXuatText.Location = new System.Drawing.Point(12, 377);
            this.btnXuatText.Name = "btnXuatText";
            this.btnXuatText.Size = new System.Drawing.Size(110, 27);
            this.btnXuatText.TabIndex = 1;
            this.btnXuatText.Text = "Xuất TXT";
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Location = new System.Drawing.Point(130, 377);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(110, 27);
            this.btnTrangChu.TabIndex = 0;
            this.btnTrangChu.Text = "Trang chủ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(300, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "Thống Kê";
            // 
            // FrmThongKeTopBan
            // 
            this.ClientSize = new System.Drawing.Size(749, 420);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTrangChu);
            this.Controls.Add(this.btnXuatText);
            this.Controls.Add(this.dgvTop);
            this.Controls.Add(this.btnHienThi);
            this.Controls.Add(this.txtTop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.label1);
            this.Name = "FrmThongKeTopBan";
            this.Text = "FrmThongKeTopBan";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label label1, label2, label3;
        private System.Windows.Forms.DateTimePicker dtFrom, dtTo;
        private System.Windows.Forms.TextBox txtTop;
        private System.Windows.Forms.Button btnHienThi, btnXuatText, btnTrangChu;
        private System.Windows.Forms.DataGridView dgvTop;
        private System.Windows.Forms.Label label4;
    }
}
