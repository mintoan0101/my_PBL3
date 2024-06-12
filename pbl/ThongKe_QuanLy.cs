using BusinessLogicLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using ValueObject;

namespace pbl
{
    public partial class ThongKe_QuanLy : Form
    {
        private  Form currentFormChild;
        public ThongKe_QuanLy()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.RowTemplate.Height = 30;
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Load_ComboBox_NhanVien();
            Load_ComboBox_BoLoc();
            Load_Doanh_Thu();
            Load_Tong_Loi_Nhuan();
            Load_So_San_Pham_Da_Ban();
            Load_Tong_Hoa_Don();
            checkBox1.Checked = false;
            dateTimePicker1.Value = new DateTime(2023, 1, 1);
            dateTimePicker2.Value = DateTime.Now;
            btn_timkiem.PerformClick();
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
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
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenChildForm(new ThongKe_BieuDo());
        }
        private void Set_Col()
        {
            dataGridView1.Columns[0].HeaderText = "Ngày Tạo Hóa Đơn";
            dataGridView1.Columns[1].HeaderText = "Giảm Giá";
            dataGridView1.Columns[2].HeaderText = "Doanh Thu";
            dataGridView1.Columns[3].HeaderText = "Lợi Nhuận";
            dataGridView1.Columns[4].HeaderText = "ID Hóa Đơn";
            dataGridView1.Columns[5].HeaderText = "Nhân Viên";

        }
        #region
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Hien_Thi_Ket_Qua();
            Set_Col();
            Load_Doanh_Thu();
            Load_Tong_Hoa_Don();
            Load_So_San_Pham_Da_Ban();
            Load_Tong_Loi_Nhuan();
        }

        #endregion
        //CÁC HÀM BỔ TRỢ

        public void Load_Doanh_Thu()
        {
            decimal total = 0;
            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                total += decimal.Parse(r.Cells[2].Value.ToString());
            }
            txt_doanhthu.Text = (total*1000).ToString("#,##0");
        }
        public void Load_Tong_Hoa_Don()
        {
            txt_tonghoadon.Text = dataGridView1.Rows.Count.ToString();
        }
        public void Load_Tong_Loi_Nhuan()
        {
            decimal total = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                total += decimal.Parse(r.Cells[3].Value.ToString());
            }
            txt_loinhuan.Text = (total*1000).ToString("#,##0");
        }
        public void Load_So_San_Pham_Da_Ban()
        {
            int sl = 0;
            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                sl += HoaDonBUS.Instance.CountSoLuongSanPham(r.Cells[4].Value.ToString());
            }
            txt_daban.Text = sl.ToString();
        }
        public void Load_ComboBox_NhanVien()
        {
            NhanVienBUS bus = new NhanVienBUS();
            cb_nhanvien.Items.Add("Tất Cả");
            foreach(string s in bus.GetNameNhanVien())
            {
                cb_nhanvien.Items.Add(s);
            }
            cb_nhanvien.SelectedItem = "Tất Cả";
        }
        public void Load_ComboBox_BoLoc()
        {
            cb_boloc.Items.Add("Doanh Thu");
            cb_boloc.Items.Add("Lợi Nhuận");
            cb_boloc.Items.Add("Ngày");
            cb_boloc.Items.Add("Giảm Giá");
            cb_boloc.SelectedItem = "Ngày";
        }
        public void Hien_Thi_Ket_Qua()
        {
            string nv = cb_nhanvien.SelectedItem.ToString();
            string sapxep = cb_boloc.SelectedItem.ToString();
            bool Tangdan = checkBox1.Checked; 
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            if (nv == "Tất Cả")
            {
                nv = "";
            }
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = db.HoaDons.Select(h => new
                {
                    Ngay = h.NgayTaoHoaDon,
                    GiamGia = h.ChietKhau,
                    DoanhThu = h.TongTien,
                    LoiNhuan = h.LoiNhuan,
                    ID = h.IDHoaDon,
                    TenNhanVien = h.NhanVien.TenNhanVien,
                }).Where(h => h.Ngay >= start && h.Ngay < end && h.TenNhanVien.Contains(nv));

                if (sapxep == "Ngày")
                {
                    if(Tangdan)
                    {
                        li = li.OrderBy(h => h.Ngay);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.Ngay);
                    }
                }
                else if(sapxep == "Doanh Thu")
                {
                    if (Tangdan)
                    {
                        li = li.OrderBy(h => h.DoanhThu);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.DoanhThu);
                    }
                }
                else if(sapxep =="Lợi Nhuận")
                {
                    if (Tangdan)
                    {
                        li = li.OrderBy(h => h.LoiNhuan);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.LoiNhuan);
                    }
                }
                else if(sapxep == "Giảm Giá")
                {
                    if (Tangdan)
                    {
                        li = li.OrderBy(h => h.GiamGia);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.GiamGia);
                    }
                }
                dataGridView1.DataSource = li.ToList();
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
