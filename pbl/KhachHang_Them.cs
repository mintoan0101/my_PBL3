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
    public partial class KhachHang_Them : Form
    {
        public KhachHangBUS bus = new KhachHangBUS();
        public KhachHang kh { get; set; }
        public bool isEdit { get; set; }
        public KhachHang_Them(string sdt)
        {
            InitializeComponent();
            setGUI(sdt);
        }
        private void ThemKhachHang_Load(object sender, EventArgs e)
        {
            if(isEdit)
            {
                txt_diem.Enabled = true;
                lbl_infor.Text = "Cập nhật thông tin khách hàng";
            }
            else
            {
                Load_ID_TuDong();
                txt_diem.Enabled = false;
                txt_diem.Text = 0+"";
            }
        }
        private void setGUI(string sdt)
        {
            if (sdt != null)
            {
                KhachHang kh = KhachHangBUS.Instance.GetKhachHangBySDT(sdt);
                txt_sdt.Text = sdt;
                txt_ten.Text = kh.Ten;
                txt_diem.Text = kh.Diem.ToString();
                txt_id.Text = kh.IDKhachHang;
            }
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txt_sdt.Text) &&
              !string.IsNullOrEmpty(this.txt_ten.Text))
            {
                if(CheckSDT())
                {
                    if(isEdit)
                    {
                            if (bus.Update(GetKH()) > 0)
                            {
                                MessageBox.Show("Đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                    }
                    else
                    {
                        if (bus.Insert(GetKH()) > 0)
                        {
                            MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại đúng 10 chữ số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
           
            }
            else MessageBox.Show("Vui lòng điền đầy đủ thông tin","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public KhachHang GetKH()
        {
            KhachHang kh = new KhachHang();
            kh.IDKhachHang = txt_id.Text;
            kh.SDT = txt_sdt.Text.Trim();
            kh.Ten = txt_ten.Text;
            kh.Diem = int.Parse(txt_diem.Text.Trim());
            return kh;
        }
        public void Load_ID_TuDong()
        {
            string last_id = bus.GetLastID();
            int num = int.Parse(last_id.Substring(last_id.Length - 2))+1;
            if(num <10)
            {
                txt_id.Text = "KH00" + num;
            }
            else if(num < 100 && num >= 10)
            {
                txt_id.Text = "KH0" + num;
            }
            else if (num >= 100 )
            {
                txt_id.Text = "KH" + num;
            }

        }
        public bool CheckSDT()
        {
            string sdt = txt_sdt.Text.Trim();
            if (sdt.Length != 10)
            {
                return false;
            }
            foreach (char c in sdt)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void btn_ok_MouseEnter(object sender, EventArgs e)
        {
            btn_ok.BackColor = Color.PowderBlue;
        }

        private void btn_ok_MouseLeave(object sender, EventArgs e)
        {
            btn_ok.BackColor= Color.FromArgb(226, 240, 243);
        }

        private void btn_exit_MouseEnter(object sender, EventArgs e)
        {
            btn_exit.BackColor = Color.PowderBlue;
        }

        private void btn_exit_MouseLeave(object sender, EventArgs e)
        {
            btn_exit.BackColor = Color.FromArgb(226, 240, 243);
        }
    }
}
