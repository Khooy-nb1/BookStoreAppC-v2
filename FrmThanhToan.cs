using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BookStoreApp
{
    public partial class FrmThanhToan : Form
    {
        private readonly List<FrmGioHang.CartItem> _items;
        private readonly string _username;

        public FrmThanhToan(List<FrmGioHang.CartItem> items, string username = null)
        {
            InitializeComponent();
            _items = items ?? new List<FrmGioHang.CartItem>();
            _username = username;

            Load += FrmThanhToan_Load;

            btnThanhToan.Click += (s, e) => AskPaymentAndPay();
            btnXuatFile.Click += (s, e) => ExportHoaDonTxt();
            btnHuy.Click += (s, e) => this.Close();
        }

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            txtMaHD.Text = GenerateMaHD();
            dtNgayLap.Value = DateTime.Now;

            BindItems();
            UpdateTongTien();
        }

        private void BindItems()
        {
            var dt = new DataTable();
            dt.Columns.Add("MaSach");
            dt.Columns.Add("TenSach");
            dt.Columns.Add("DonGia", typeof(decimal));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("ThanhTien", typeof(decimal));

            foreach (var it in _items)
                dt.Rows.Add(it.MaSach, it.TenSach, it.DonGia, it.SoLuong, it.ThanhTien);

            dgvItems.DataSource = dt;
        }

        private void UpdateTongTien()
        {
            decimal total = _items.Sum(x => x.ThanhTien);
            lblTongTien.Text = "Tổng tiền: " + total.ToString("N0") + " đ";
        }

        private string GenerateMaHD()
        {
           
            string ma = "HD" + DateTime.Now.ToString("yyMMddHHmm");

            var exist = SqlHelper.Scalar("SELECT COUNT(*) FROM HoaDon WHERE MaHD=@id",
                new SqlParameter("@id", ma));

            if (Convert.ToInt32(exist) == 0) return ma;

            
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string ma2 = ma.Substring(0, 11) + c; 
                exist = SqlHelper.Scalar("SELECT COUNT(*) FROM HoaDon WHERE MaHD=@id",
                    new SqlParameter("@id", ma2));
                if (Convert.ToInt32(exist) == 0) return ma2;
            }

           
            return "HD" + DateTime.Now.ToString("yyMMddHH") + "99"; 
        }


        // chọn phương thức
        private void AskPaymentAndPay()
        {
            if (_items.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm để thanh toán!");
                return;
            }

            var choose = MessageBox.Show(
                "Chọn phương thức thanh toán:\n\n" +
                "YES  = Tiền mặt\n" +
                "NO   = QR ngân hàng\n" +
                "CANCEL = Hủy",
                "Thanh toán",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (choose == DialogResult.Cancel) return;

            if (choose == DialogResult.Yes)
            {
                var ok = MessageBox.Show(
                    "Bạn chọn thanh toán bằng TIỀN MẶT.\n\nNhấn OK để thanh toán hoặc Cancel để hủy.",
                    "Xác nhận",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (ok == DialogResult.OK)
                    DoThanhToanCore();
                else
                    MessageBox.Show("Đã hủy thanh toán.");
            }
            else 
            {
                using (var f = new FrmThanhToanQR(txtMaHD.Text.Trim(), lblTongTien.Text))
                {
                    var r = f.ShowDialog();
                    if (r == DialogResult.OK)
                    {
                        DoThanhToanCore();
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa xác nhận thanh toán QR.", "Thông báo");
                    }
                }
            }
        }

       
        private void DoThanhToanCore()
        {
           
            string maKH = txtMaKH.Text.Trim();
            if (!string.IsNullOrEmpty(maKH))
            {
                var khexist = SqlHelper.Scalar("SELECT COUNT(*) FROM KhachHang WHERE MaKH=@k",
                    new SqlParameter("@k", maKH));
                if (Convert.ToInt32(khexist) == 0)
                {
                    MessageBox.Show("Mã khách hàng không tồn tại! (Hoặc để trống nếu bán lẻ)");
                    return;
                }
            }

           
            foreach (var it in _items)
            {
                var tonObj = SqlHelper.Scalar("SELECT SoLuongTon FROM Sach WHERE MaSach=@m",
                    new SqlParameter("@m", it.MaSach));
                int ton = Convert.ToInt32(tonObj);
                if (it.SoLuong > ton)
                {
                    MessageBox.Show($"Sách {it.MaSach} không đủ tồn. Tồn={ton}, mua={it.SoLuong}");
                    return;
                }
            }

            string maHD = txtMaHD.Text.Trim();
            decimal tong = _items.Sum(x => x.ThanhTien);

            
            var existHD = SqlHelper.Scalar("SELECT COUNT(*) FROM HoaDon WHERE MaHD=@id",
                new SqlParameter("@id", maHD));
            if (Convert.ToInt32(existHD) > 0)
            {
                MessageBox.Show("Hóa đơn này đã được thanh toán trước đó!", "Thông báo");
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

           
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        
                        using (SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO HoaDon(MaHD, NgayLap, TongTien, MaKH)
                            VALUES(@MaHD, @NgayLap, @TongTien, @MaKH)
                        ", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaHD", maHD);
                            cmd.Parameters.AddWithValue("@NgayLap", dtNgayLap.Value.Date);
                            cmd.Parameters.AddWithValue("@TongTien", tong);
                            cmd.Parameters.AddWithValue("@MaKH",
                                string.IsNullOrEmpty(maKH) ? (object)DBNull.Value : maKH);

                            int n1 = cmd.ExecuteNonQuery();
                            if (n1 <= 0) throw new Exception("Không tạo được hóa đơn!");
                        }

                       
                        foreach (var it in _items)
                        {
                            using (SqlCommand cmdCT = new SqlCommand(@"
                                INSERT INTO CTHoaDon(MaHD, MaSach, SoLuong, DonGia)
                                VALUES(@MaHD, @MaSach, @SoLuong, @DonGia)
                            ", conn, tran))
                            {
                                cmdCT.Parameters.AddWithValue("@MaHD", maHD);
                                cmdCT.Parameters.AddWithValue("@MaSach", it.MaSach);
                                cmdCT.Parameters.AddWithValue("@SoLuong", it.SoLuong);
                                cmdCT.Parameters.AddWithValue("@DonGia", it.DonGia);
                                cmdCT.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdTon = new SqlCommand(@"
                                UPDATE Sach
                                SET SoLuongTon = SoLuongTon - @sl
                                WHERE MaSach = @m
                            ", conn, tran))
                            {
                                cmdTon.Parameters.AddWithValue("@sl", it.SoLuong);
                                cmdTon.Parameters.AddWithValue("@m", it.MaSach);
                                cmdTon.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();

                        MessageBox.Show("Thanh toán thành công!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Thanh toán thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportHoaDonTxt()
        {
            string maHD = txtMaHD.Text.Trim();
            if (string.IsNullOrWhiteSpace(maHD))
            {
                MessageBox.Show("Chưa có mã hóa đơn!");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text file (*.txt)|*.txt";
                sfd.FileName = maHD + ".txt";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();
                sb.AppendLine("===== HÓA ĐƠN =====");
                sb.AppendLine("Mã HD: " + maHD);
                sb.AppendLine("Ngày lập: " + dtNgayLap.Value.ToString("dd/MM/yyyy"));
                sb.AppendLine("Mã KH: " + (string.IsNullOrEmpty(txtMaKH.Text.Trim()) ? "(bán lẻ)" : txtMaKH.Text.Trim()));
                sb.AppendLine("-------------------");
                sb.AppendLine("MaSach\tTenSach\tDonGia\tSoLuong\tThanhTien");

                foreach (var it in _items)
                {
                    sb.AppendLine($"{it.MaSach}\t{it.TenSach}\t{it.DonGia:N0}\t{it.SoLuong}\t{it.ThanhTien:N0}");
                }

                sb.AppendLine("-------------------");
                sb.AppendLine(lblTongTien.Text);

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất file thành công!");
            }
        }
    }
}
