using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValueObject;

namespace pbl
{
    public partial class DoanhThuCaNhan : Form
    {
        HoaDonBUS bus = new HoaDonBUS();
        public string idnv {  get; set; }
        public DoanhThuCaNhan()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Tim_Kiem();
        }
        private void DoanhThuCaNhan_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DateTime start = new DateTime(2020, 1, 1, 0, 0, 0);
            dateTimePicker1.Value = start;
            dateTimePicker2.Value = DateTime.Now;
            Load_Doanh_Thu();
            Tim_Kiem();

        }
        public void Load_Doanh_Thu()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from h in db.HoaDon
                         where h.IDNhanVien == idnv
                         select new
                         { 
                             Ngay = h.NgayTaoHoaDon,
                             GiamGia = h.ChietKhau,
                             DoanhThu =  h.TongTien,
                             LoiNhuan = h.LoiNhuan,
                             ID = h.IDHoaDon,
                         };
                dataGridView1.DataSource = li.ToList();
            }
        }
        public void Tim_Kiem()
        {
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from h in db.HoaDon
                         where h.NgayTaoHoaDon >= start && h.NgayTaoHoaDon < end
                               && h.IDNhanVien == idnv
                         select new
                         {
                             Ngay = h.NgayTaoHoaDon,
                             GiamGia = h.ChietKhau,
                             DoanhThu = h.TongTien,
                             LoiNhuan = h.LoiNhuan,
                             ID = h.IDHoaDon,
                         };
                dataGridView1.DataSource = li.ToList();
            }
            Hien_Thi_Thong_Ke();
        }
        public void Hien_Thi_Thong_Ke()
        {
            int num = dataGridView1.Rows.Count;
            txt_tonghoadon.Text = num + "";
            decimal doanhthu = 0;
            decimal loinhuan = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                doanhthu += decimal.Parse(r.Cells[2].Value.ToString());
                loinhuan += decimal.Parse(r.Cells[3].Value.ToString());
            }
            txt_doanhthu.Text = doanhthu + "";
            txt_loinhuan.Text = loinhuan + "";
            int sl = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                sl += HoaDonBUS.Instance.CountSoLuongSanPham(r.Cells[4].Value.ToString());
            }
            txt_daban.Text = sl.ToString();
        }
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
