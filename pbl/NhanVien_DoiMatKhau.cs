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
            if(Check_Day_Du_Thong_Tin())
            {
                if(Check_Mat_Khau_Cu_Hop_le())
                {
                    if(bus.UpdatePasswordByIDNV(idnv,txt_matkhaumoi.Text)>0)
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
                    MessageBox.Show("Mật khẩu cũ không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public bool Check_Day_Du_Thong_Tin()
        {
            if(txt_matkhaucu.Text != ""&&
                txt_matkhaumoi.Text != ""&&
                txt_xacnhan.Text != "")
            {
                return true;
            }
            return false;
        }
        public bool Check_Mat_Khau_Cu_Hop_le()
        {
            if(bus.CheckPassByIDNV(idnv,txt_matkhaucu.Text))
            {
                return true;
            }
            return false;
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
    }
}
