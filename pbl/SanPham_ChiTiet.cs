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
    public partial class SanPham_ChiTiet : Form
    {
        ChiTietSanPhamBUS bus = new ChiTietSanPhamBUS();
        NhaPhanPhoiBUS nppbus = new NhaPhanPhoiBUS();
        public bool isAdmin { get; set; }

        public bool isFull { get; set; }
        public string idsanpham { get; set; }
        public string non_idsanpham { get; set; }
        public SanPham_ChiTiet()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.RowTemplate.Height = 30;
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
                else if (i == 2) rate = 6;
                else if (i == 3) rate = 6;
                else if (i == 4) rate = 3;
                else if (i == 5) rate = 2;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void ChiTietSanPham_Load(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                btn_edit.Enabled = false;
                btn_them.Enabled = false;
                btn_delete.Enabled = false;
            }
            isFull = false;
            Load_CTSP();
            Load_NPP();
            Load_Bo_Loc();
            Dieu_Chinh_DataGridView();
        }


        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            SanPham_ThemSuaChiTiet f = new SanPham_ThemSuaChiTiet();
            f.isEdit = false;
            f.IDSanPham = idsanpham;
            f.ShowDialog();
        }
        private void btn_edit_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                SanPham_ThemSuaChiTiet f = new SanPham_ThemSuaChiTiet();
                f.isEdit = true;
                f.IDChiTiet = row.Cells[0].Value.ToString();
                f.TenSanPham = row.Cells[1].Value.ToString();
                f.IDSanPham = idsanpham;
                f.TenNPP = row.Cells[2].Value.ToString();
                f.HSD = row.Cells[3].Value.ToString();
                f.SoLuong = int.Parse(row.Cells[4].Value.ToString());
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chi tiết cần chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string id = row.Cells[0].Value.ToString();
                if (bus.CheckCTSPCoTheXoa(id))
                {
                    DialogResult re = MessageBox.Show("Bạn có chắc chắc muốn xóa " + id, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (re == DialogResult.Yes)
                    {
                        if (bus.Delete(id) > 0)
                        {
                            MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chi tiết sản phẩm có ID là "+id+" hiện dang được sử dụng, bạn không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chi tiết bạn muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Tim_Kiem();
        }
        //CÁC HÀM BỔ TRỢ
        public void Load_Bo_Loc()
        {
            cb_boloc.Items.Add("Tất Cả");
            cb_boloc.Items.Add("Đã Hết Hạn");
            cb_boloc.Items.Add("Chưa Hết Hạn");
            cb_boloc.Items.Add("Số Lượng < 50");
            cb_boloc.Items.Add("Số Lượng 50 - 100");
            cb_boloc.Items.Add("Số Lượng > 100");
            cb_boloc.SelectedItem = "Tất Cả";
        }
        public void Load_NPP()
        {
            List<string> li = nppbus.GetName();
            cb_nhaphanphoi.Items.Add("Tất Cả");
            foreach (string s in li)
            {
                cb_nhaphanphoi.Items.Add(s);
            }
            cb_nhaphanphoi.SelectedItem = "Tất Cả";
        }
        public void Load_CTSP()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from c in db.ChiTietSanPhams
                         where c.IDSanPham == idsanpham
                         select new
                         {
                             c.IDChiTiet,
                             c.SanPham.Ten,
                             c.NhaPhanPhoi.TenNhaPhanPhoi,
                             c.HanSuDung,
                             c.SoLuong,
                            
                         };
                dataGridView1.DataSource = li.ToList();
            }

        }
        public void Tim_Kiem()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                string npp = cb_nhaphanphoi.SelectedItem.ToString();
                string boloc = cb_boloc.SelectedItem.ToString();
                var li = from c in db.ChiTietSanPhams
                         where c.IDSanPham.Contains(non_idsanpham)
                         select new
                         {
                             c.IDChiTiet,
                             c.SanPham.Ten,
                             c.NhaPhanPhoi.TenNhaPhanPhoi,
                             c.HanSuDung,
                             c.SoLuong,

                         };
                if (npp != "Tất Cả")
                {
                    li = li.Where(c => c.TenNhaPhanPhoi.Contains(npp));
                }
                if (boloc == "Đã Hết Hạn")
                {
                    li = li.Where(p => p.HanSuDung <= DateTime.Now);
                }
                else if (boloc == "Chưa Hết Hạn")
                {
                    li = li.Where(p => p.HanSuDung > DateTime.Now);
                }
                else if (boloc == "Số Lượng < 50")
                {
                    li = li.Where(p => p.SoLuong < 50);
                }
                else if (boloc == "Số Lượng 50 - 100")
                {
                    li = li.Where(p => p.SoLuong >= 50 && p.SoLuong <= 100);
                }
                else if (boloc == "Số Lượng > 100")
                {
                    li = li.Where(p => p.SoLuong > 100);
                }
                dataGridView1.DataSource = li.ToList();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                if (isFull)
                {
                    var li = from c in db.ChiTietSanPhams
                             where c.IDSanPham == idsanpham
                             select new
                             {
                                 c.IDChiTiet,
                                 c.SanPham.Ten,
                                 c.NhaPhanPhoi.TenNhaPhanPhoi,
                                 c.HanSuDung,
                                 c.SoLuong,
                             };
                    dataGridView1.DataSource = li.ToList();
                    isFull = false;
                    linkLabel1.Text = "Hiển thị tất cả";
                    non_idsanpham = idsanpham;
                    btn_them.Enabled = true;
                }
                else
                {
                    var li = from c in db.ChiTietSanPhams
                           
                             select new
                             {
                                 c.IDChiTiet,
                                 c.SanPham.Ten,
                                 c.NhaPhanPhoi.TenNhaPhanPhoi,
                                 c.HanSuDung,
                                 c.SoLuong,
                             };
                    dataGridView1.DataSource = li.ToList();
                    isFull = true;
                    linkLabel1.Text = "Quay lại";
                    non_idsanpham = "";
                    btn_them.Enabled = false;
                }
            }
           
        }

        private void btn_timkiem_MouseEnter(object sender, EventArgs e)
        {
            btn_timkiem.BackColor = Color.LightGray;
        }

        private void btn_timkiem_MouseLeave(object sender, EventArgs e)
        {
            btn_timkiem.BackColor= Color.FromArgb(248, 249, 250);
        }
    }
}
