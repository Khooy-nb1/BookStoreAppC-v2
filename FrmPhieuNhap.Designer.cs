namespace BookStoreApp
{
    partial class FrmPhieuNhap
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
            this.dgvPN = new System.Windows.Forms.DataGridView();
            this.txtMaPN = new System.Windows.Forms.TextBox();
            this.txtMaNXB = new System.Windows.Forms.TextBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.dtNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.btnHienThi = new System.Windows.Forms.Button();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnTaoPN = new System.Windows.Forms.Button();
            this.btnXoaPN = new System.Windows.Forms.Button();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPN)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPN
            // 
            this.dgvPN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPN.Location = new System.Drawing.Point(12, 113);
            this.dgvPN.Name = "dgvPN";
            this.dgvPN.Size = new System.Drawing.Size(495, 325);
            this.dgvPN.TabIndex = 0;
            // 
            // txtMaPN
            // 
            this.txtMaPN.Location = new System.Drawing.Point(670, 113);
            this.txtMaPN.Name = "txtMaPN";
            this.txtMaPN.Size = new System.Drawing.Size(100, 20);
            this.txtMaPN.TabIndex = 1;
            // 
            // txtMaNXB
            // 
            this.txtMaNXB.Location = new System.Drawing.Point(670, 165);
            this.txtMaNXB.Name = "txtMaNXB";
            this.txtMaNXB.Size = new System.Drawing.Size(100, 20);
            this.txtMaNXB.TabIndex = 2;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(670, 191);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(100, 20);
            this.txtTongTien.TabIndex = 3;
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(173, 74);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(256, 20);
            this.txtTim.TabIndex = 4;
            // 
            // dtNgayNhap
            // 
            this.dtNgayNhap.Location = new System.Drawing.Point(570, 139);
            this.dtNgayNhap.Name = "dtNgayNhap";
            this.dtNgayNhap.Size = new System.Drawing.Size(200, 20);
            this.dtNgayNhap.TabIndex = 5;
            // 
            // btnHienThi
            // 
            this.btnHienThi.BackColor = System.Drawing.Color.Cyan;
            this.btnHienThi.Location = new System.Drawing.Point(12, 71);
            this.btnHienThi.Name = "btnHienThi";
            this.btnHienThi.Size = new System.Drawing.Size(75, 23);
            this.btnHienThi.TabIndex = 6;
            this.btnHienThi.Text = "Hiển thị";
            this.btnHienThi.UseVisualStyleBackColor = false;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(435, 71);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 23);
            this.btnTim.TabIndex = 7;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            // 
            // btnTaoPN
            // 
            this.btnTaoPN.BackColor = System.Drawing.Color.Lime;
            this.btnTaoPN.Location = new System.Drawing.Point(551, 253);
            this.btnTaoPN.Name = "btnTaoPN";
            this.btnTaoPN.Size = new System.Drawing.Size(75, 23);
            this.btnTaoPN.TabIndex = 8;
            this.btnTaoPN.Text = "Tạo";
            this.btnTaoPN.UseVisualStyleBackColor = false;
            // 
            // btnXoaPN
            // 
            this.btnXoaPN.BackColor = System.Drawing.Color.Red;
            this.btnXoaPN.Location = new System.Drawing.Point(632, 253);
            this.btnXoaPN.Name = "btnXoaPN";
            this.btnXoaPN.Size = new System.Drawing.Size(75, 23);
            this.btnXoaPN.TabIndex = 9;
            this.btnXoaPN.Text = "Xóa";
            this.btnXoaPN.UseVisualStyleBackColor = false;
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.BackColor = System.Drawing.Color.Cyan;
            this.btnChiTiet.Location = new System.Drawing.Point(713, 253);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(75, 23);
            this.btnChiTiet.TabIndex = 10;
            this.btnChiTiet.Text = "Chi tiết";
            this.btnChiTiet.UseVisualStyleBackColor = false;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.BackColor = System.Drawing.Color.Fuchsia;
            this.btnTrangChu.Location = new System.Drawing.Point(713, 415);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(75, 23);
            this.btnTrangChu.TabIndex = 11;
            this.btnTrangChu.Text = "Trang chủ";
            this.btnTrangChu.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(513, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Mã PN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(513, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Mã NXB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(513, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Tổng tiền";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(317, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 31);
            this.label4.TabIndex = 15;
            this.label4.Text = "Phiếu Nhập";
            // 
            // FrmPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTrangChu);
            this.Controls.Add(this.btnChiTiet);
            this.Controls.Add(this.btnXoaPN);
            this.Controls.Add(this.btnTaoPN);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.btnHienThi);
            this.Controls.Add(this.dtNgayNhap);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.txtMaNXB);
            this.Controls.Add(this.txtMaPN);
            this.Controls.Add(this.dgvPN);
            this.Name = "FrmPhieuNhap";
            this.Text = "FrmPhieuNhap";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPN;
        private System.Windows.Forms.TextBox txtMaPN;
        private System.Windows.Forms.TextBox txtMaNXB;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.DateTimePicker dtNgayNhap;
        private System.Windows.Forms.Button btnHienThi;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnTaoPN;
        private System.Windows.Forms.Button btnXoaPN;
        private System.Windows.Forms.Button btnChiTiet;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}