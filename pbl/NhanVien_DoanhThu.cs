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
    public partial class NhanVien_DoanhThu : Form
    {
        HoaDonBUS bus = new HoaDonBUS();
        public string idnv {  get; set; }
        public NhanVien_DoanhThu()
        {
            InitializeComponent();
            Load_Sap_Xep();
            Load_Doanh_Thu();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Tim_Kiem();
        }
        private void DoanhThuCaNhan_Load(object sender, EventArgs e)
        {
            DateTime start = new DateTime(2024, 1, 1);
            dateTimePicker1.Value = start;
            dateTimePicker2.Value = DateTime.Now;
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 30;
            Tim_Kiem();
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        public void Load_Sap_Xep()
        {
            cb_boloc.Items.Add("Doanh thu");
            cb_boloc.Items.Add("Lợi nhuận");
            cb_boloc.Items.Add("Ngày");
            cb_boloc.Items.Add("Giảm giá");
            cb_boloc.SelectedItem = "Ngày";
        }
        public void Load_Doanh_Thu()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from h in db.HoaDons
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
            string sapxep = cb_boloc.SelectedItem.ToString();
            bool TangDan = checkBox1.Checked;
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = db.HoaDons.Select(h => new
                {
                    Ngay = h.NgayTaoHoaDon,
                    GiamGia = h.ChietKhau,
                    DoanhThu = h.TongTien,
                    LoiNhuan = h.LoiNhuan,
                    ID = h.IDHoaDon,
                    IDNV = h.IDNhanVien,
                }).Where(h => h.Ngay >= start && h.Ngay < end && h.IDNV.Contains(idnv));
                if(sapxep == "Doanh thu")
                {
                    if(TangDan)
                    {
                        li = li.OrderBy(h => h.DoanhThu);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.DoanhThu);
                    }
                }
                else if(sapxep == "Lợi nhuận")
                {
                    if (TangDan)
                    {
                        li = li.OrderBy(h => h.LoiNhuan);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.LoiNhuan);
                    }
                }
                else if(sapxep == "Ngày")
                {
                    if (TangDan)
                    {
                        li = li.OrderBy(h => h.Ngay);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.Ngay);
                    }
                }
                else if(sapxep == "Giảm giá")
                {
                    if (TangDan)
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
    }
}
