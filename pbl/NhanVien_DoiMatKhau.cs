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

namespace pbl
{
    public partial class NhanVien_DoiMatKhau : Form
    {
        TaiKhoanBUS bus = new TaiKhoanBUS();
        public string idnv {  get; set; }
        public NhanVien_DoiMatKhau()
        {
            InitializeComponent();
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            txt_matkhaucu.UseSystemPasswordChar = true;
            txt_matkhaumoi.UseSystemPasswordChar = true;
            txt_xacnhan .UseSystemPasswordChar = true;
        }

        private void MatKhau_Load(object sender, EventArgs e)
        {

        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!Check_Day_Du_Thong_Tin()) message = "Vui lòng điền đầy đủ thông tin.";
            else if (!Check_Do_Dai_Mat_Khau()) message = "Độ dài mật khẩu phải tối thiểu 8 chữ số. ";
            else if (!Check_Mat_Khau_Khong_Qua_Dai()) message = "Độ dài mật khẩu tối đa không được quá 20 kí tự. ";
            else if (!Check_Mat_Khau_Cu_Hop_le()) message = "Mật khẩu cũ hiện tại không chính xác.";
            else if (!Check_Xac_Nhan_mat_Khau_Moi()) message = "Mật khẩu xác nhận không trùng khớp với mật khẩu mới.";
            if (String.IsNullOrEmpty(message))
            {
                if (bus.UpdatePasswordByIDNV(idnv, txt_matkhaumoi.Text) > 0)
                {
                    MessageBox.Show("Cập nhật mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra trong quá trình thay đổi mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool Check_Day_Du_Thong_Tin()
        {
            if(txt_matkhaucu.Text != ""&&
                txt_matkhaumoi.Text != ""&&
                txt_xacnhan.Text != "")
            {
                return true;
            }
            return false;
        }
        private bool Check_Mat_Khau_Cu_Hop_le()
        {
            if(bus.CheckPassByIDNV(idnv,txt_matkhaucu.Text))
            {
                return true;
            }
            return false;
        }
        public bool Check_Xac_Nhan_mat_Khau_Moi()
        {
            if (txt_xacnhan.Text.Trim() == txt_matkhaumoi.Text.Trim())
            {
                return true;
            }
            return false;
        }
        private bool Check_Do_Dai_Mat_Khau()
        {
            if(txt_matkhaumoi.Text.Trim().Length < 8)
            {
                return false;
            }
            return true;
        }
        private bool Check_Mat_Khau_Khong_Qua_Dai()
        {
            if(txt_matkhaumoi.Text.Trim().Length >20)
            {
                return false;
            }
            return true;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox4.Visible = true;
            txt_matkhaucu.UseSystemPasswordChar = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox5.Visible = true;
            txt_matkhaumoi.UseSystemPasswordChar = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox6.Visible = true;
            txt_xacnhan.UseSystemPasswordChar = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox1.Visible = true;
            txt_matkhaucu.UseSystemPasswordChar = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = false;
            pictureBox2.Visible = true;
            txt_matkhaumoi.UseSystemPasswordChar = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox6.Visible = false;
            pictureBox3.Visible = true;
            txt_xacnhan.UseSystemPasswordChar = true;
        }

        private void txt_matkhaucu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_matkhaumoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_xacnhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
