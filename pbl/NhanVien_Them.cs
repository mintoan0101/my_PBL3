using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValueObject;


namespace pbl
{
    public partial class NhanVien_Them : Form
    {
        NhanVienBUS nvbus = new NhanVienBUS();
        TaiKhoanBUS tkbus = new TaiKhoanBUS();

        public string idnv { get; set; }
        public bool isEdit { get; set; }
        public NhanVien_Them()
        {
            InitializeComponent();
        }

        private void ThemNhanvien_Load(object sender, EventArgs e)
        {
            txt_idtk.Enabled = false;
            txt_idnv.Enabled = false;
            if (isEdit)
            {
                Load_Thong_Tin_Nhan_Vien();
                lbl_infor.Text = "Cập nhật thông tin nhân viên";
            }
            else
            {

                Load_IDNV_Tu_Dong();
                Load_IDTK_Tu_Dong();
            }
        }
        //CÁC HÀM SỰ KIỆN
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                Luu_Nhan_Vien();
            }
            else
            {
                Them_Nhan_Vien();
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_hovaten_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_diachi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_cccd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_sdt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_tendangnhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_tendangnhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_matkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_luong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ok.PerformClick();
            }
        }
        //CÁC HÀM BỔ TRỢ
        //Thêm Nhân Viên
        public void Them_Nhan_Vien()
        {
            string message = "";
            if(!Kiem_Tra_Day_Du_Thong_Tin()) message += " Vui lòng nhập đầy đủ thông tin.";
            if(!Kiem_Tra_SDT()) message += " Vui lòng nhập số điện thoại đúng 10 chữ số.";
            if (!Kiem_Tra_Dinh_Dang_SDT()) message += " Số điện thoại chỉ được phép chứa kí tự số.";
            if (!Kiem_Tra_Dinh_Dang_CCCD()) message += " Số CCCD chỉ được phép chứa kí tự số.";
            if (message.Length == 0)
            {
                    TaiKhoan tk = new TaiKhoan();
                    //Thêm các thông tin tài khoảng trước
                    tk.IDTaiKhoan = txt_idtk.Text;
                    tk.TenTaiKhoan = txt_tendangnhap.Text;
                    tk.MatKhau = txt_matkhau.Text;
                    if (tkbus.Insert(tk) > 0)
                    {
                        NhanVien nv = new NhanVien();
                        //Rồi thêm các thông tin bên nhân viên
                        nv.TenNhanVien = txt_hovaten.Text;
                        nv.IDNhanVien = txt_idnv.Text;
                        nv.DiaChi = txt_diachi.Text;
                        nv.NgaySinh = dateTimePicker1.Value;
                        nv.Nam = (rad_nam.Checked) ? true : false;
                        nv.SoDienThoai = txt_sdt.Text;
                        nv.IDTaiKhoan = txt_idtk.Text;
                        nv.MucLuong = int.Parse(txt_luong.Text);
                        nv.CCCD = txt_cccd.Text;
                        nv.Email = txt_email.Text;
                        if (nvbus.Insert(nv) > 0)
                        {
                            MessageBox.Show("Đã thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
            else
            {
                MessageBox.Show(message, "Lỗi thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public bool Kiem_Tra_Day_Du_Thong_Tin()
        {

            if (txt_diachi.Text != "" &&
                txt_email.Text != "" &&
                txt_hovaten.Text != "" &&
                txt_matkhau.Text != "" &&
                txt_tendangnhap.Text != "" &&
                (rad_nam.Checked == true || rad_nu.Checked == true)
             )
            {
                return true;
            }

            return false;
        }
        public bool Kiem_Tra_SDT()
        {
            if(txt_sdt.Text.Length != 10)
            {
                return false;
            }
            return true;
        }
        public bool Kiem_Tra_Dinh_Dang_CCCD()
        {
            int s = 0;
            if(int.TryParse(txt_cccd.Text, out s))
            {
                return true;
            }
            return false;
        }
        public bool Kiem_Tra_Dinh_Dang_SDT()
        {
            int s = 0;
            if (int.TryParse(txt_sdt.Text, out s))
            { 
            
                return true;
            }
            return false;
        }
        public void Load_IDNV_Tu_Dong()
        {
            string lastID = nvbus.GetLastID();
            int num = int.Parse(lastID.Substring(2)) + 1;
            if (num < 10)
            {
                txt_idnv.Text = "NV0" + num;
            }
            else
            {
                txt_idnv.Text = "NV" + num;
            }
        }
        public void Load_IDTK_Tu_Dong()
        {
            string lastID = tkbus.GetLastID();
            int num = int.Parse(lastID.Substring(2)) + 1;
            if (num < 10)
            {
                txt_idtk.Text = "US0" + num;
            }
            else
            {
                txt_idtk.Text = "US" + num;
            }
        }
        public string Num_ID(int num)
        {
            if (num <= 9)
            {
                return "0" + num;
            }
            return num + "";
        }
        //Sửa Nhân Viên
        public void Load_Thong_Tin_Nhan_Vien()
        {
            NhanVien n = nvbus.GetNVByID(idnv);
            txt_idnv.Text = idnv;
            txt_idtk.Text = n.IDTaiKhoan.ToString();
            txt_hovaten.Text = n.TenNhanVien.ToString();
            dateTimePicker1.Value = SetStringToDate(n.NgaySinh.ToString());
            if (n.Nam.ToString() == "True")
            {
                rad_nam.Checked = true;
            }
            else
            {
                rad_nu.Checked = true;
            }
            txt_email.Text = n.Email.ToString();
            txt_cccd.Text = n.CCCD.ToString();
            txt_sdt.Text = n.SoDienThoai.ToString();
            txt_luong.Text = n.MucLuong.ToString();
            txt_diachi.Text = n.DiaChi.ToString();
            txt_tendangnhap.Text = n.TaiKhoan.TenTaiKhoan.ToString();
            txt_matkhau.Text = n.TaiKhoan.MatKhau.ToString();

        }
        public DateTime SetStringToDate(string date)
        {
            string date2 = date.Substring(0, 10);
            string[] parts = date2.Split('/');
            int y = int.Parse(parts[2]);
            int m = int.Parse(parts[1]);
            int d = int.Parse(parts[0]);
            return new DateTime(y, m, d);

        }
        public void Luu_Nhan_Vien()
        {
            if (Kiem_Tra_Day_Du_Thong_Tin())
            {
                TaiKhoan tk = new TaiKhoan();
                //Lưu thông tin tài khoản trước
                tk.IDTaiKhoan = txt_idtk.Text;
                tk.TenTaiKhoan = txt_tendangnhap.Text;
                tk.MatKhau = txt_matkhau.Text;
                if (tkbus.Update(tk) > 0)
                {
                    NhanVien nv = new NhanVien();
                    //Rồi lưu thông tin nhân viên
                    nv.IDNhanVien = txt_idnv.Text;
                    nv.IDTaiKhoan = txt_idtk.Text;
                    nv.TenNhanVien = txt_hovaten.Text;
                    nv.NgaySinh = dateTimePicker1.Value;
                    nv.Nam = (rad_nam.Checked == true) ? true : false;
                    nv.Email = txt_email.Text;
                    nv.CCCD = txt_cccd.Text;
                    nv.SoDienThoai = txt_sdt.Text;
                    nv.MucLuong = int.Parse(txt_luong.Text);
                    nv.DiaChi = txt_diachi.Text;
                    if (nvbus.Update(nv) > 0)
                    {
                        MessageBox.Show("Đã chỉnh sửa thông tin nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
