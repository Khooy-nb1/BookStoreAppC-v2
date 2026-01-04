namespace BookStoreApp
{
    partial class FrmHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvHD = new System.Windows.Forms.DataGridView();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.dtNgayLap = new System.Windows.Forms.DateTimePicker();
            this.btnHienThi = new System.Windows.Forms.Button();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnTaoHD = new System.Windows.Forms.Button();
            this.btnXoaHD = new System.Windows.Forms.Button();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHD
            // 
            this.dgvHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHD.Location = new System.Drawing.Point(12, 95);
            this.dgvHD.Name = "dgvHD";
            this.dgvHD.Size = new System.Drawing.Size(412, 311);
            this.dgvHD.TabIndex = 0;
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(660, 95);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(100, 20);
            this.txtMaHD.TabIndex = 1;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(660, 153);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(100, 20);
            this.txtTongTien.TabIndex = 2;
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(464, 69);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(243, 20);
            this.txtTim.TabIndex = 3;
            // 
            // dtNgayLap
            // 
            this.dtNgayLap.Location = new System.Drawing.Point(560, 127);
            this.dtNgayLap.Name = "dtNgayLap";
            this.dtNgayLap.Size = new System.Drawing.Size(200, 20);
            this.dtNgayLap.TabIndex = 4;
            // 
            // btnHienThi
            // 
            this.btnHienThi.BackColor = System.Drawing.Color.Cyan;
            this.btnHienThi.Location = new System.Drawing.Point(12, 66);
            this.btnHienThi.Name = "btnHienThi";
            this.btnHienThi.Size = new System.Drawing.Size(75, 23);
            this.btnHienThi.TabIndex = 5;
            this.btnHienThi.Text = "Hiển thị";
            this.btnHienThi.UseVisualStyleBackColor = false;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(713, 66);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 23);
            this.btnTim.TabIndex = 6;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            // 
            // btnTaoHD
            // 
            this.btnTaoHD.BackColor = System.Drawing.Color.Lime;
            this.btnTaoHD.Location = new System.Drawing.Point(509, 230);
            this.btnTaoHD.Name = "btnTaoHD";
            this.btnTaoHD.Size = new System.Drawing.Size(75, 23);
            this.btnTaoHD.TabIndex = 7;
            this.btnTaoHD.Text = "Tạo";
            this.btnTaoHD.UseVisualStyleBackColor = false;
            // 
            // btnXoaHD
            // 
            this.btnXoaHD.BackColor = System.Drawing.Color.Red;
            this.btnXoaHD.Location = new System.Drawing.Point(590, 230);
            this.btnXoaHD.Name = "btnXoaHD";
            this.btnXoaHD.Size = new System.Drawing.Size(75, 23);
            this.btnXoaHD.TabIndex = 8;
            this.btnXoaHD.Text = "Xóa";
            this.btnXoaHD.UseVisualStyleBackColor = false;
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.BackColor = System.Drawing.Color.Yellow;
            this.btnChiTiet.Location = new System.Drawing.Point(671, 230);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(75, 23);
            this.btnChiTiet.TabIndex = 9;
            this.btnChiTiet.Text = "Chi tiết";
            this.btnChiTiet.UseVisualStyleBackColor = false;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.BackColor = System.Drawing.Color.Fuchsia;
            this.btnTrangChu.Location = new System.Drawing.Point(713, 415);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(75, 23);
            this.btnTrangChu.TabIndex = 10;
            this.btnTrangChu.Text = "Trang chủ";
            this.btnTrangChu.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(461, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Mã hóa đơn";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(461, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tổng tiền";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(327, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hóa Đơn";
            // 
            // FrmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTrangChu);
            this.Controls.Add(this.btnChiTiet);
            this.Controls.Add(this.btnXoaHD);
            this.Controls.Add(this.btnTaoHD);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.btnHienThi);
            this.Controls.Add(this.dtNgayLap);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.txtMaHD);
            this.Controls.Add(this.dgvHD);
            this.Name = "FrmHoaDon";
            this.Text = "FrmHoaDon";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHD;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.DateTimePicker dtNgayLap;
        private System.Windows.Forms.Button btnHienThi;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnTaoHD;
        private System.Windows.Forms.Button btnXoaHD;
        private System.Windows.Forms.Button btnChiTiet;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}