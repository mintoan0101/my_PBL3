using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValueObject;

namespace pbl
{
    public partial class HoaDonNhap_ChiTiet : Form
    {
        public string IDHoaDonNhap {  get; set; }

        public HoaDonNhap_ChiTiet()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.RowTemplate.Height = 30;
        }

        private void XemChiTietHoaDonNhap_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Load_San_Pham();
            Load_Sap_Xep();
            checkBox1.Checked = true;
            Dieu_Chinh_DataGridView();
        }
        private void Dieu_Chinh_DataGridView()
        {
            int total = dataGridView1.Width;
            int part = total / 20;
            int i = 1;
            int count = dataGridView1.Columns.Count;
            int rate = 1;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                if (i == 1) rate = 2;
                else if (i == 2) rate = 7;
                else if (i == 3) rate = 2;
                else if (i == 4) rate = 2;
                else if (i == 5) rate = 2;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Hien_Thi_Tim_Kiem();
        }
        public void Load_San_Pham()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = db.ChiTietHoaDonNhaps.Select(p => new
                {
                    IDChiTietSP = p.IDChiTiet,
                    TenSanPham = p.ChiTietSanPham.SanPham.Ten,
                    GiaNhap = p.ChiTietSanPham.SanPham.GiaNhap,
                    SoLuong = p.SoLuong,
                    IDHoaDon = p.IDHoaDonNhap,
                }).Where(p => p.IDHoaDon == IDHoaDonNhap);
                dataGridView1.DataSource = li.ToList();
            }
        }
        public void Load_Sap_Xep()
        {
            cb_sapxep.Items.Add("ID Chi Tiết Sản Phẩm");
            cb_sapxep.Items.Add("Tên Sản Phẩm");
            cb_sapxep.Items.Add("Số Lượng");
            cb_sapxep.Items.Add("Giá Nhập");
            cb_sapxep.SelectedItem = "ID Chi Tiết Sản Phẩm";
        }
        public void Hien_Thi_Tim_Kiem()
        {
            string id = txt_idsp.Text;
            string sapxep = cb_sapxep.SelectedItem.ToString();
            bool GiamDan = checkBox1.Checked;
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = db.ChiTietHoaDonNhaps.Select(p => new
                {
                    IDChiTietSP = p.IDChiTiet,
                    TenSanPham = p.ChiTietSanPham.SanPham.Ten,
                    GiaNhap = p.ChiTietSanPham.SanPham.GiaNhap,
                    SoLuong = p.SoLuong,
                    IDHoaDon = p.IDHoaDonNhap,
                }).Where(p => p.IDHoaDon == IDHoaDonNhap && p.IDChiTietSP.Contains(id));
                if(sapxep == "ID Chi Tiết Sản Phẩm")
                {
                    if(GiamDan)
                    {
                        li = li.OrderBy(p => p.IDChiTietSP);
                    }
                    else
                    {
                        li = li.OrderByDescending(p => p.IDChiTietSP);
                    }
                }
                else if(sapxep == "Tên Sản Phẩm")
                {
                    if (GiamDan)
                    {
                        li = li.OrderBy(p => p.TenSanPham);
                    }
                    else
                    {
                        li = li.OrderByDescending(p => p.TenSanPham);
                    }
                }
                else if (sapxep == "Số Lượng")
                {
                    if (GiamDan)
                    {
                        li = li.OrderBy(p => p.SoLuong);
                    }
                    else
                    {
                        li = li.OrderByDescending(p => p.SoLuong);
                    }
                }
                else if (sapxep == "Giá Nhập")
                {
                    if (GiamDan)
                    {
                        li = li.OrderBy(p => p.GiaNhap);
                    }
                    else
                    {
                        li = li.OrderByDescending(p => p.GiaNhap);
                    }
                }
                dataGridView1.DataSource = li.ToList();
            }
        }

        private void btn_timkiem_MouseEnter(object sender, EventArgs e)
        {
            btn_timkiem.BackColor = Color.LightGray;
        }

        private void btn_timkiem_MouseLeave(object sender, EventArgs e)
        {
            btn_timkiem.BackColor = Color.FromArgb(248, 249, 250);
        }
    }
}
