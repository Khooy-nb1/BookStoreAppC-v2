namespace BookStoreApp
{
    partial class FrmTacGia
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
            this.dgvTacGia = new System.Windows.Forms.DataGridView();
            this.btnHienThi = new System.Windows.Forms.Button();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnXoaTruong = new System.Windows.Forms.Button();
            this.btnXuatText = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.txtMaTG = new System.Windows.Forms.TextBox();
            this.txtTenTG = new System.Windows.Forms.TextBox();
            this.txtLienLac = new System.Windows.Forms.TextBox();
            this.lstFilter = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTacGia)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTacGia
            // 
            this.dgvTacGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTacGia.Location = new System.Drawing.Point(21, 106);
            this.dgvTacGia.Name = "dgvTacGia";
            this.dgvTacGia.Size = new System.Drawing.Size(523, 303);
            this.dgvTacGia.TabIndex = 0;
            // 
            // btnHienThi
            // 
            this.btnHienThi.BackColor = System.Drawing.Color.Cyan;
            this.btnHienThi.Location = new System.Drawing.Point(21, 77);
            this.btnHienThi.Name = "btnHienThi";
            this.btnHienThi.Size = new System.Drawing.Size(75, 23);
            this.btnHienThi.TabIndex = 1;
            this.btnHienThi.Text = "Hiển thị";
            this.btnHienThi.UseVisualStyleBackColor = false;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(713, 77);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 23);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.Lime;
            this.btnThem.Location = new System.Drawing.Point(561, 204);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.Yellow;
            this.btnSua.Location = new System.Drawing.Point(642, 204);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 4;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.Red;
            this.btnXoa.Location = new System.Drawing.Point(723, 204);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnXoaTruong
            // 
            this.btnXoaTruong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnXoaTruong.Location = new System.Drawing.Point(561, 243);
            this.btnXoaTruong.Name = "btnXoaTruong";
            this.btnXoaTruong.Size = new System.Drawing.Size(237, 23);
            this.btnXoaTruong.TabIndex = 6;
            this.btnXoaTruong.Text = "Xóa trường";
            this.btnXoaTruong.UseVisualStyleBackColor = false;
            // 
            // btnXuatText
            // 
            this.btnXuatText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnXuatText.Location = new System.Drawing.Point(21, 415);
            this.btnXuatText.Name = "btnXuatText";
            this.btnXuatText.Size = new System.Drawing.Size(75, 23);
            this.btnXuatText.TabIndex = 7;
            this.btnXuatText.Text = "Xuất file text";
            this.btnXuatText.UseVisualStyleBackColor = false;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.BackColor = System.Drawing.Color.Fuchsia;
            this.btnTrangChu.Location = new System.Drawing.Point(713, 415);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(75, 23);
            this.btnTrangChu.TabIndex = 8;
            this.btnTrangChu.Text = "Trang chủ";
            this.btnTrangChu.UseVisualStyleBackColor = false;
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(443, 77);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(264, 20);
            this.txtTim.TabIndex = 9;
            // 
            // txtMaTG
            // 
            this.txtMaTG.Location = new System.Drawing.Point(688, 106);
            this.txtMaTG.Name = "txtMaTG";
            this.txtMaTG.Size = new System.Drawing.Size(100, 20);
            this.txtMaTG.TabIndex = 10;
            // 
            // txtTenTG
            // 
            this.txtTenTG.Location = new System.Drawing.Point(688, 132);
            this.txtTenTG.Name = "txtTenTG";
            this.txtTenTG.Size = new System.Drawing.Size(100, 20);
            this.txtTenTG.TabIndex = 11;
            // 
            // txtLienLac
            // 
            this.txtLienLac.Location = new System.Drawing.Point(688, 158);
            this.txtLienLac.Name = "txtLienLac";
            this.txtLienLac.Size = new System.Drawing.Size(100, 20);
            this.txtLienLac.TabIndex = 12;
            // 
            // lstFilter
            // 
            this.lstFilter.FormattingEnabled = true;
            this.lstFilter.Location = new System.Drawing.Point(561, 314);
            this.lstFilter.Name = "lstFilter";
            this.lstFilter.Size = new System.Drawing.Size(237, 95);
            this.lstFilter.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(582, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Mã tác giả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(582, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tên tác giả";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(582, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Liên lạc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(293, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 31);
            this.label4.TabIndex = 17;
            this.label4.Text = "Quản Lý Tác Giả";
            // 
            // FrmTacGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstFilter);
            this.Controls.Add(this.txtLienLac);
            this.Controls.Add(this.txtTenTG);
            this.Controls.Add(this.txtMaTG);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.btnTrangChu);
            this.Controls.Add(this.btnXuatText);
            this.Controls.Add(this.btnXoaTruong);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.btnHienThi);
            this.Controls.Add(this.dgvTacGia);
            this.Name = "FrmTacGia";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTacGia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTacGia;
        private System.Windows.Forms.Button btnHienThi;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnXoaTruong;
        private System.Windows.Forms.Button btnXuatText;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.TextBox txtMaTG;
        private System.Windows.Forms.TextBox txtTenTG;
        private System.Windows.Forms.TextBox txtLienLac;
        private System.Windows.Forms.ListBox lstFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}