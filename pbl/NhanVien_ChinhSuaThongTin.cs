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
    public partial class NhanVien_ChinhSuaThongTin : Form
    {
        NhanVienBUS bus = new NhanVienBUS();
        public string username { get; set; }
        public string idnv { get; set; }
        public NhanVien_ChinhSuaThongTin()
        {
            InitializeComponent();
        }

        private void ChinhSuaThongTinCaNhan_Load(object sender, EventArgs e)
        {
            Load_Thong_Tin_Ca_Nhan();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string mess = "";
            if (!Check_Day_Du_Thong_Tin()) mess = " Vui lòng nhập đầy đủ thông tin. ";
            else if (!Check_SDT_Hop_Le()) mess = " Số điện thoại không hợp lệ. ";
            else if (!Check_Do_Dai_SDT()) mess = " Số điện thoại phải chứa đúng 10 chữ số. ";
            else if (!Check_CCCD_Hop_Le()) mess = " CCCD không hợp lệ. ";
            else if (!Check_Do_Dai_CCCD()) mess = " CCCD phải chứa đúng 12 chữ số. ";
            if(String.IsNullOrEmpty(mess))
            {
                NhanVien nv = new NhanVien();
                nv.IDNhanVien = txt_id.Text;
                nv.TenNhanVien = txt_hovaten.Text;
                nv.Email = txt_email.Text;
                nv.SoDienThoai = txt_sdt.Text;
                nv.NgaySinh = dateTimePicker1.Value;
                nv.DiaChi = txt_diachi.Text;
                nv.CCCD = txt_cccd.Text;
                nv.Nam = (rad_nam.Checked == true) ? true : false;
                if(bus.UpdateByNhanVien(nv)>0)
                {
                    MessageBox.Show("Đã cập nhật thông tin cá nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(mess, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ChinhSuaThongTinCaNhan_KeyDown(object sender, KeyEventArgs e)
        {
            btn_luu.PerformClick();
        }
        //CÁC HÀM BỔ TRỢ
        public void Load_Thong_Tin_Ca_Nhan()
        {
            NhanVien n = bus.GetNVByID(idnv);
            txt_id.Text = n.IDNhanVien;
            txt_hovaten.Text = n.TenNhanVien;
            txt_email.Text = n.Email;
            txt_sdt.Text = n.SoDienThoai;
            dateTimePicker1.Value = n.NgaySinh;
            if(n.Nam )
            {
                rad_nam.Checked = true;
            }
            else
            {
                rad_nu.Checked = true;
            }
            txt_cccd.Text = n.CCCD;
            txt_diachi.Text = n.DiaChi;

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
        public bool Check_Day_Du_Thong_Tin()
        {
            if(txt_email.Text != ""&&
                txt_id.Text != ""&&
                txt_hovaten.Text != ""&&
                txt_sdt.Text != ""&&
                txt_diachi.Text != ""&&
                txt_cccd.Text != "")
            {
                return true;
            }
            return false;
        }
        private bool Check_SDT_Hop_Le()
        {
            if(txt_sdt.Text.Trim().All(char.IsDigit)) 
                { return true;}
            return false;
        }
        private bool Check_Do_Dai_SDT()
        {
            if(txt_sdt.Text.Trim().Length != 10)
            {
                return false;
            }
            return true;
        }

        private bool Check_CCCD_Hop_Le()
        {
            if(txt_cccd.Text.Trim().All(char.IsDigit))
                {
                return true;
            }
            return false;
        }
        private bool Check_Do_Dai_CCCD()
        {
            if(txt_cccd.Text.Trim().Length != 12)
            {
                return false;
            }
            return true;
        }
        private void txt_cccd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_hovaten_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_sdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_sdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txt_cccd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
