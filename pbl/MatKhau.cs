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
    public partial class MatKhau : Form
    {
        TaiKhoanBUS bus = new TaiKhoanBUS();
        public string idnv {  get; set; }
        public MatKhau()
        {
            InitializeComponent();
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

      
    }
}
