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
    public partial class NhaPhanPhoi_QuanLy : Form
    {
        NhaPhanPhoiBUS bus = new NhaPhanPhoiBUS();

        public NhaPhanPhoi_QuanLy()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 10);
            dataGridView1.RowTemplate.Height = 30;
        }
        private void QuanLyNhaPhanPhoi_Load(object sender, EventArgs e)
        {
            Load_NPP();
            Load_ThuocTinh();
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
                else if (i == 2) rate = 6;
                else if (i == 3) rate = 2;
                else if (i == 4) rate = 12;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        //CÁC HÀM XỬ LÍ SỰ KIỆN
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = bus.Search(txt_timkiem.Text, cb_thuoctinh.SelectedItem.ToString());
            Tim_Kiem();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                NhaPhanPhoi_Them f = new NhaPhanPhoi_Them();
                f.isEdit = true;
                f.npp = new NhaPhanPhoi();
                f.npp.IDNhaPhanPhoi = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                f.npp.TenNhaPhanPhoi = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                f.npp.SoDienThoai = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                f.npp.DiaChi = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhà phân phối cần chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {

            NhaPhanPhoi_Them f = new NhaPhanPhoi_Them();
            f.isEdit = false;
            f.ShowDialog();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string TenNPP = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                if (bus.ChekNPP(id))
                {
                    DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà phân phối " + TenNPP + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                    MessageBox.Show("Bạn không thể xóa "+TenNPP+" vì nhà phân phối này hiện đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
             else
             {
                 MessageBox.Show("Vui lòng chọn nhà phân phối bạn muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
            
        }


        //CÁC HÀM BỔ TRỢ

        public void Load_ThuocTinh()
        {
            cb_thuoctinh.Items.Add("ID");
            cb_thuoctinh.Items.Add("Tên");
            cb_thuoctinh.Items.Add("SDT");
            cb_thuoctinh.Items.Add("Địa Chỉ");
            cb_thuoctinh.SelectedItem = "Tên";
        }
        public void Load_NPP()
        {
            // dataGridView1.DataSource = bus.GetData();
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from p in db.NhaPhanPhois
                         select new
                         {
                            ID =  p.IDNhaPhanPhoi,
                            Ten = p.TenNhaPhanPhoi,
                            SDT = p.SoDienThoai,
                            DiaChi = p.DiaChi,
                         };
                dataGridView1.DataSource = li.ToList();
            }
        }
        public void Tim_Kiem()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                string txt = txt_timkiem.Text;
                string thuoctinh = cb_thuoctinh.SelectedItem.ToString();
                var re = from p in db.NhaPhanPhois
                         select new
                         {
                             ID = p.IDNhaPhanPhoi,
                             Ten = p.TenNhaPhanPhoi,
                             SDT = p.SoDienThoai,
                             DiaChi = p.DiaChi,
                         };
                if (txt != "")
                {
                    if (thuoctinh == "ID")
                    {
                        re = re.Where(p => p.ID.Contains(txt));
                    }
                    else if (thuoctinh == "Tên")
                    {
                        re = re.Where(p => p.Ten.Contains(txt));
                    }
                    else if (thuoctinh == "SDT")
                    {
                        re = re.Where(p => p.SDT.Contains(txt));
                    }
                    else if (thuoctinh == "Địa Chỉ")
                    {
                        re = re.Where(p => p.DiaChi.Contains(txt));
                    }
                }
                dataGridView1.DataSource = re.ToList();
            }
        }

        
    }
}
