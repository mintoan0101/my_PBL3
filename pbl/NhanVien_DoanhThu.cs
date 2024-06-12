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
        NhanVienBUS nvBus = new NhanVienBUS();
        public string idnv {  get; set; }
        public NhanVien_DoanhThu()
        {
            InitializeComponent();
            Load_Sap_Xep();
            btn_timkiem.PerformClick();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Tim_Kiem();
            Set_Col();
        }
        private void DoanhThuCaNhan_Load(object sender, EventArgs e)
        {
            DateTime start = new DateTime(2024, 1, 1);
            dateTimePicker1.Value = start;
            dateTimePicker2.Value = DateTime.Now;
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 30;
            btn_timkiem.PerformClick();
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void Set_Col()
        {
            dataGridView1.Columns[0].HeaderText = "Ngày";
            dataGridView1.Columns[1].HeaderText = "Giảm Giá";
            dataGridView1.Columns[2].HeaderText = "Doanh Thu";
            dataGridView1.Columns[3].HeaderText = "Lợi Nhuận";
            dataGridView1.Columns[4].HeaderText = "ID Hóa Đơn";
            dataGridView1.Columns[5].HeaderText = "ID Nhân Viên";
        }
        public void Load_Sap_Xep()
        {
            cb_boloc.Items.Add("Doanh Thu");
            cb_boloc.Items.Add("Lợi Nhuận");
            cb_boloc.Items.Add("Ngày");
            cb_boloc.Items.Add("Giảm Giá");
            cb_boloc.SelectedItem = "Ngày";
        }

        public void Tim_Kiem()
        {
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            string sapxep = cb_boloc.SelectedItem.ToString();
            bool TangDan = checkBox1.Checked;
            dataGridView1.DataSource = nvBus.DoanhThu(start, end, sapxep, TangDan, idnv);
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
