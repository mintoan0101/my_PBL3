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
    public partial class HoaDon_Xem : Form
    {
        private HoaDon hd;
        public HoaDon_Xem(HoaDon hd)
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 9);
            dataGridView1.RowTemplate.Height = 30;
            this.hd = hd;
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
                if (i == 1) rate = 4;
                else if (i == 2) rate = 2;
                else if (i == 3) rate = 3;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void XemHoaDon_Load(object sender, EventArgs e)
        {
            lb_ID.Text = hd.IDHoaDon;
            lb_DateTime.Text = hd.NgayTaoHoaDon.ToString();
            lb_KhachHang.Text = "ID Khách Hàng: " + hd.IDKhachHang;
            lb_NhanVien.Text = "ID Nhân Viên: " + hd.IDNhanVien;
            lb_GiamGia.Text = hd.ChietKhau.ToString();
            lb_Tong.Text = hd.TongTien.ToString();
            lb_ThanhTien.Text = (hd.TongTien + hd.ChietKhau).ToString();
            dataGridView1.DataSource = ChiTietHoaDonBUS.Instance.GetData(hd.IDHoaDon);
            Dieu_Chinh_DataGridView();
        }
    }
}
