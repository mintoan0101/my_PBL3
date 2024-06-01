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
    public partial class HoaDon_NhanVien : Form
    {
        public string IDNhanvien;
        private Form currentFormChild;
        public HoaDon_NhanVien()
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
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HoaDon_DanhSach());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            HoaDon_Them f = new HoaDon_Them();
            f.IDNhanVien = IDNhanvien;
            OpenChildForm(f);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.LightGray;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.WhiteSmoke;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.LightGray;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.WhiteSmoke;
        }

        private void Hoadon_nhanvien_Load(object sender, EventArgs e)
        {

        }
    }
}
