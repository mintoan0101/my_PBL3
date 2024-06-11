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
    public partial class NhaPhanPhoi_Them : Form
    {
        public bool isEdit { get; set; }
        public NhaPhanPhoi npp { get; set; }
        NhaPhanPhoiBUS bus = new NhaPhanPhoiBUS();
        public NhaPhanPhoi_Them()
        {
            InitializeComponent();
           
        }
        private void ThemNhaPhanPhoi_Load(object sender, EventArgs e)
        {

            if(isEdit)
            {
                Load_Thong_Tin();
                lbl_infor.Text = "Cập nhật thông tin nhà phân phối";
            }
            else
            {
                Load_ID_TuDong();
            }
        }
        private void btn_ok_Click_1(object sender, EventArgs e)
        {
            string warning = "";
            if (string.IsNullOrEmpty(txt_ten.Text) || string.IsNullOrEmpty(txt_diachi.Text)
                || string.IsNullOrEmpty(txt_sdt.Text))
            {
                warning = "Vui lòng nhập đầy đủ thông tin. ";
            }
            else if (!CheckSDT())
            {
                warning += "Số điện thoại không hợp lệ. ";
            }
            else if (!CountSDT())
            {
                warning += "Số điện thoại nhập vào phải có đúng 10 chữ số";
            }
            if (string.IsNullOrEmpty(warning))
            {
                if (isEdit)
                {
                    if (Cap_Nhat_Thong_tin())
                    {
                        MessageBox.Show("Đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    if (Them_Nha_Phan_Phoi())
                    {
                        MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            else MessageBox.Show(warning, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        //CÁC HÀM BỔ TRỢ
        public NhaPhanPhoi GetNPP()
        {
            NhaPhanPhoi p = new NhaPhanPhoi(); 
            p.IDNhaPhanPhoi = txt_id.Text;
            p.TenNhaPhanPhoi = txt_ten.Text;
            p.SoDienThoai = txt_sdt.Text.Trim();
            p.DiaChi = txt_diachi.Text;
            return p;
        }
        public void Load_ID_TuDong()
        {
            string last_id = bus.GetLastID();
            int num = int.Parse(last_id.Substring(last_id.Length - 2))+1;
            if(num < 10)
            {
                txt_id.Text = "NPP0" + num;
            }
            else
            {
                txt_id.Text = "NPP" + num;
            }
        }
        public bool Them_Nha_Phan_Phoi()
        {
            if (bus.Insert(GetNPP()) > 0)
            {
                return true;
            }
            return false;
        }
        public void Load_Thong_Tin()
        {
            txt_id.Text = npp.IDNhaPhanPhoi;
            txt_ten.Text = npp.TenNhaPhanPhoi;
            txt_sdt.Text = npp.SoDienThoai;
            txt_diachi.Text = npp.DiaChi;
        }
        public bool Cap_Nhat_Thong_tin()
        {
            if(bus.Update(GetNPP())>0)
            {
                return true;
            }
            return false;
        }

        public bool CountSDT()
        {
            if (txt_sdt.Text.Length != 10)
            {
                return false;
            }
            return true;
        }
        public bool CheckSDT()
        {
            if (txt_sdt.Text.Trim().All(char.IsDigit))
            {
                return true;
            }
            return false;
        }
        private void btn_ok_MouseEnter(object sender, EventArgs e)
        {
            btn_ok.BackColor = Color.PowderBlue;
        }

        private void btn_ok_MouseLeave(object sender, EventArgs e)
        {
            btn_ok.BackColor = Color.FromArgb(226, 240, 243);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.PowderBlue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(226, 240, 243);
        }

      
    }
}
