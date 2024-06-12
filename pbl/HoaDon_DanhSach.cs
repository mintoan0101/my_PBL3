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
using static Guna.UI2.Native.WinApi;
using static System.Net.Mime.MediaTypeNames;

namespace pbl
{
    public partial class HoaDon_DanhSach : Form
    {
        public string IDNhanVien ;
        public bool isAdmin;
        public HoaDon_DanhSach(string ID)
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.RowTemplate.Height = 30;
            IDNhanVien = ID;
        }
        private void HoaDon_DanhSach_Load(object sender, EventArgs e)
        {
            if (isAdmin == true)
            {
                
                dataGridView1.DataSource = HoaDonBUS.Instance.GetData();
            }
            else
            {
                dataGridView1.DataSource = HoaDonBUS.Instance.GetDataByNhanVien(IDNhanVien);
            }
            Load_Tim_kiem_Theo();
            Load_Bo_Loc();
            Dieu_Chinh_DataGridView();
            Set_Col();
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
                else if (i == 4) rate = 4;
                else if (i == 5) rate = 2;
                else if (i == 6) rate = 3;
                else if (i == 7) rate = 3;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void Set_Col()
        {
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Nhân Viên";
            dataGridView1.Columns[2].HeaderText = "Ngày Tạo";
            dataGridView1.Columns[3].HeaderText = "Khách Hàng";
            dataGridView1.Columns[4].HeaderText = "Chiết Khấu";
            dataGridView1.Columns[5].HeaderText = "Tổng Tiền";
            dataGridView1.Columns[6].HeaderText = "Lợi Nhuận";

        }
        private void Load_Tim_kiem_Theo()
        {
            cbb_PhanLoai.Items.Add("ID");
            cbb_PhanLoai.Items.Add("Khách Hàng");
            if(isAdmin) cbb_PhanLoai.Items.Add("Nhân Viên");
            cbb_PhanLoai.SelectedIndex = 0;
        }
        public void Load_Bo_Loc()
        {
            cbb_BoLoc.Items.Add("Tất Cả");
            cbb_BoLoc.Items.Add("< 100K");
            cbb_BoLoc.Items.Add("100K - 500K");
            cbb_BoLoc.Items.Add("500K - 1000K");
            cbb_BoLoc.Items.Add("> 1000K");
            cbb_BoLoc.Items.Add("Tăng dần");
            cbb_BoLoc.Items.Add("Giảm dần");
            cbb_BoLoc.SelectedItem = "Tất Cả";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bt_search_Click(object sender, EventArgs e)
        {
            string search = txt_search.Text.Trim();
            string phanloai = cbb_PhanLoai.SelectedItem.ToString();
            string boloc = cbb_BoLoc.SelectedItem.ToString();
            string idnv = "";
            if (!isAdmin) idnv = IDNhanVien;
            Search(search, phanloai, boloc,idnv);
            Set_Col();
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
                    IDHoaDon = row.Cells[0].Value.ToString(),
                    NgayTaoHoaDon = DateTime.Parse(row.Cells[2].Value.ToString()),
                    TongTien = decimal.Parse(row.Cells[5].Value.ToString()),
                    IDNhanVien = row.Cells[1].Value.ToString(),
                    IDKhachHang = row.Cells[3].Value.ToString(),
                    ChietKhau = Convert.ToDecimal(row.Cells[4].Value)
                };
            }

            return hoaDon;
        }
        private void Search(string txt, string phanloai, string boloc, string idnv)
        {
            using(PBL3Entities1 pbl = new PBL3Entities1())
            {
                var query = pbl.HoaDons.OrderByDescending(p => p.NgayTaoHoaDon)
               .ThenByDescending(p => p.IDHoaDon)
               .Where(p => p.IDNhanVien.Contains(idnv))
               .Select(p => new
               {
                   p.IDHoaDon,
                   p.NhanVien.TenNhanVien,
                   p.NgayTaoHoaDon,
                   p.KhachHang.Ten,
                   p.ChietKhau,
                   p.TongTien,
                   p.LoiNhuan,

               });

                if (string.IsNullOrEmpty(txt) == false)
                {
                    switch (phanloai)
                    {
                        case "ID":
                            query = query.Where(hd => hd.IDHoaDon.Contains(txt));
                            break;
                        case "Nhân Viên":
                            query = query.Where(hd => hd.TenNhanVien.Contains(txt) );
                            break;
                        case "Khách Hàng":
                            query = query.Where(hd => hd.Ten.Contains(txt));
                            break;
                        default:
                            break;
                    }
                }

                if (boloc != "Tất Cả")
                {
                    switch (boloc)
                    {
                        case "< 100K":
                            query = query.Where(hd => hd.TongTien < 100);
                            break;
                        case "100K - 500K":
                            query = query.Where(hd => hd.TongTien >= 100 && hd.TongTien <= 500);
                            break;
                        case "500K - 1000K":
                            query = query.Where(hd => hd.TongTien >= 500 && hd.TongTien <= 1000);
                            break;
                        case "> 1000K":
                            query = query.Where(hd => hd.TongTien > 1000);
                            break;
                        case "Tăng dần":
                            query = query.OrderBy(hd => hd.TongTien);
                            break;
                        case "Giảm dần":
                            query = query.OrderByDescending(hd => hd.TongTien);
                            break;
                    }
                }
                dataGridView1.DataSource = query.ToList();
            }
        }
    }
}
