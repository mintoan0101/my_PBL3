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
using System.Windows.Forms;
using ValueObject;

namespace pbl
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Load_Hoa_Don();
            Load_ComboBox_NhanVien();
            Load_ComboBox_BoLoc();
            Load_Doanh_Thu();
            Load_Tong_Loi_Nhuan();
            Load_So_San_Pham_Da_Ban();
            Load_Tong_Hoa_Don();
            checkBox1.Checked = true;
            dateTimePicker1.Value = new DateTime(2023, 1, 1);
            dateTimePicker2.Value = DateTime.Now;
        }
        #region
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Hien_Thi_Ket_Qua();
            Load_Doanh_Thu();
            Load_Tong_Hoa_Don();
            Load_So_San_Pham_Da_Ban();
            Load_Tong_Loi_Nhuan();
        }

        #endregion
        //CÁC HÀM BỔ TRỢ
        public void Load_Hoa_Don()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from h in db.HoaDon
                         select new
                         {
                             Ngay = h.NgayTaoHoaDon,
                             GiamGia = h.ChietKhau,
                             DoanhThu = h.TongTien,
                             LoiNhuan = h.LoiNhuan,
                             ID = h.IDHoaDon,
                             TenNhanVien = h.NhanVien.TenNhanVien,
                         };
                dataGridView1.DataSource = li.ToList();
            }
        }
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
            cb_boloc.Items.Add("Doanh thu ");
            cb_boloc.Items.Add("Lợi nhuận");
            cb_boloc.Items.Add("Ngày");
            cb_boloc.Items.Add("Giảm giá");
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
                var li = db.HoaDon.Select(h => new
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
                else if(sapxep == "Doanh thu")
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
                else if(sapxep =="Lợi nhuận")
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
                else if(sapxep == "Giảm giá")
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BieuDo f = new BieuDo();
            f.ShowDialog();
        }
    }
}
