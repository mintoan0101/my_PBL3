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
    public partial class NhanVien_ThongTin : Form
    {
        private Form currentFormChild;
        public string username { get; set; }
        public string idnv {  get; set; }
        public NhanVien_ThongTin()
        {
            InitializeComponent();
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ThongTinCaNhan_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
            label1.Text = " Xin chào, " + username;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NhanVien_ChinhSuaThongTin c = new NhanVien_ChinhSuaThongTin();
            c.username = this.username;
            c.idnv = this.idnv;
            OpenChildForm(c);
            button1.BackColor = this.BackColor;
            button2.BackColor = panel1.BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NhanVien_DoanhThu d = new NhanVien_DoanhThu();
            d.idnv = this.idnv;
            OpenChildForm(d);
            button2.BackColor = this.BackColor;
            button1.BackColor = panel1.BackColor;
            button4.BackColor = panel1.BackColor;
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            NhanVien_DoiMatKhau m = new NhanVien_DoiMatKhau();
            m.idnv = this.idnv;
            OpenChildForm(m);
            button4.BackColor = this.BackColor;
            button1.BackColor = panel1.BackColor;
            button2.BackColor = panel1.BackColor;
        }
    }
}
