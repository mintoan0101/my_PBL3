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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pbl
{
    public partial class SanPham_QuanLy : Form
    {
        //CÁC THUỘC TÍNH CƠ BẢN
        SanPhamBUS spbus = new SanPhamBUS();
        public bool isAdmin { get; set; }
        private Form currentFormChild;
        public SanPham_QuanLy()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold",12 );
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
                else if (i == 2) rate = 8;
                else if (i == 3) rate = 5;
                else if (i == 4) rate = 2;
                else if (i == 5) rate = 2;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void QuanLySanPham_Load(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                btn_sua.Enabled = false;
                btn_them.Enabled = false;
                btn_xoa.Enabled = false;
            }
            Load_Phan_Loai();
            Load_Bo_Loc();
            btn_timkiem.PerformClick();
            Dieu_Chinh_DataGridView();
        }
        //CÁC HÀM XỬ LÍ SỰ KIỆN
        private void btn_them_Click_1(object sender, EventArgs e)
        {

            SanPham_Them f = new SanPham_Them();
            f.isEdit = false;
            f.Show();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                SanPham_Them f = new SanPham_Them();
                f.isEdit = true;
                f.idsanpham = row.Cells[0].Value.ToString();
                f.phanloai = row.Cells[2].Value.ToString();
                f.tensanpham = row.Cells[1].Value.ToString();
                f.giaban = row.Cells[3].Value.ToString();
                f.gianhap = row.Cells[4].Value.ToString();
                f.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa đổi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string ten = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                if (spbus.CheckSapPhamCoTheXoa(id))
                {
                    DialogResult res = MessageBox.Show("Bạn có chắn chắn muốn xóa sản phẩm " + ten, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        if (spbus.Delete(id) == 1)
                        {
                            MessageBox.Show("Đã xóa thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn không thể xóa vì sản phẩm "+ten+" hiện đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Tim_Kiem();
        }
        private void btn_chitiet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                SanPham_ChiTiet f = new SanPham_ChiTiet();
                f.isAdmin = this.isAdmin;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                f.idsanpham = row.Cells[0].Value.ToString();
                f.non_idsanpham = f.idsanpham;
                OpenChildForm(f);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xem thông tin chi tiết", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        //CÁC HÀM BỔ TRỢ

        public void Load_Phan_Loai()
        {
            List<string> ds_danhmuc = spbus.GetSeperatedDataByColumn();
            cb_phanloai.Items.Add("Tất Cả");
            foreach (string s in ds_danhmuc)
            {
                cb_phanloai.Items.Add(s);
            }
            cb_phanloai.SelectedItem = "Tất Cả";
        }
        public void Load_Bo_Loc()
        {
            cb_boloc.Items.Add("Tất Cả");
            cb_boloc.Items.Add("<30K");
            cb_boloc.Items.Add("30K - 100K");
            cb_boloc.Items.Add("100K - 200K");
            cb_boloc.Items.Add(">200K");
            cb_boloc.Items.Add("Giá giảm dần");
            cb_boloc.Items.Add("Giá tăng dần");
            cb_boloc.SelectedItem = "Tất Cả";
        }
        public void Tim_Kiem()
        {
            using (PBL3Entities1 pbl = new PBL3Entities1())
            {
                string txt = txt_tentimkiem.Text;
                string boloc = cb_boloc.SelectedItem.ToString();
                string phanloai = cb_phanloai.SelectedItem.ToString();
                var list = pbl.SanPhams.Select(p => new
                {
                    ID = p.IDSanPham,
                    Ten = p.Ten,
                    PhanLoai = p.PhanLoai,
                    GiaBan = p.GiaBan,
                    GiaNhap = p.GiaNhap,
                });
                if (txt != null)
                {
                    list = list.Where(p => p.Ten.Contains(txt));
                }
                if (phanloai != "Tất Cả")
                {
                    list = list.Where(p => p.PhanLoai == phanloai);
                }
                if (boloc != "Tất cả")
                {
                    if (boloc == "<30K")
                    {
                        list = list.Where(p => p.GiaBan < 30);
                    }
                    else if (boloc == "30K - 100K")
                    {
                        list = list.Where(p => p.GiaBan >= 30 && p.GiaBan <= 100);
                    }
                    else if (boloc == "100K - 200K")
                    {
                        list = list.Where(p => p.GiaBan > 100 && p.GiaBan <= 200);
                    }
                    else if (boloc == ">200K")
                    {
                        list = list.Where(p => p.GiaBan > 200);
                    }
                    else if (boloc == "Giá giảm dần")
                    {
                        list = list.OrderByDescending(p => p.GiaBan);
                    }
                    else if (boloc == "Giá tăng dần")
                    {
                        list = list.OrderBy(p => p.GiaBan);
                    }

                }
                dataGridView1.DataSource = list.ToList();
            }
        }
    }
}
