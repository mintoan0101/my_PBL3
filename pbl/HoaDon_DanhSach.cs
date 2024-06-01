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
    public partial class HoaDon_DanhSach : Form
    {
        public HoaDon_DanhSach()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.RowTemplate.Height = 30;
            
        }
        private void HoaDon_DanhSach_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = HoaDonBUS.Instance.GetData();
            Load_Bo_Loc();
            cbb_PhanLoai.SelectedIndex = 0;
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
                else if (i == 2) rate = 3;
                else if (i == 3) rate = 2;
                else if (i == 4) rate = 3;
                else if (i == 5) rate = 3;
                else if (i == 6) rate = 3;
                else if (i == 7) rate = 3;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        public void Load_Bo_Loc()
        {
            cbb_BoLoc.Items.Add("Tất Cả");
            cbb_BoLoc.Items.Add("< 100K");
            cbb_BoLoc.Items.Add("100K - 500K");
            cbb_BoLoc.Items.Add("500K - 1000K");
            cbb_BoLoc.Items.Add("> 1000K");
            cbb_BoLoc.SelectedItem = "Tất Cả";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bt_search_Click(object sender, EventArgs e)
        {
            string search = txt_search.Text;
            string phanloai = cbb_PhanLoai.SelectedItem.ToString();
            string boloc = cbb_BoLoc.SelectedItem.ToString();
            dataGridView1.DataSource = HoaDonBUS.Instance.Search(search, phanloai, boloc);
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HoaDon hd = GetHoaDonFromDataGridView(dataGridView1, e.RowIndex);
            HoaDon_Xem f = new HoaDon_Xem(hd);
            f.ShowDialog();

        }
        public HoaDon GetHoaDonFromDataGridView(DataGridView dgv, int rowIndex)
        {
            HoaDon hoaDon = new HoaDon();

            if (!dgv.Rows[rowIndex].IsNewRow)
            {
                DataGridViewRow row = dgv.Rows[rowIndex];

                hoaDon = new HoaDon
                {
                    IDHoaDon = row.Cells["IDHoaDon"].Value.ToString(),
                    NgayTaoHoaDon = DateTime.Parse(row.Cells["NgayTaoHoaDon"].Value.ToString()),
                    TongTien = decimal.Parse(row.Cells["TongTien"].Value.ToString()),
                    IDNhanVien = row.Cells["IDNhanVien"].Value.ToString(),
                    IDKhachHang = row.Cells["IDKhachHang"].Value.ToString()
                };
            }

            return hoaDon;
        }

    }
}
