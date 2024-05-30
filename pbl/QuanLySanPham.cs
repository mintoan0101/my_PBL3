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
    public partial class QuanLySanPham : Form
    {
        //CÁC THUỘC TÍNH CƠ BẢN
        SanPhamBUS spbus = new SanPhamBUS();
        public bool isAdmin { get; set; }

        public QuanLySanPham()
        {
            InitializeComponent();
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
            Load_DS_San_Pham();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        //CÁC HÀM XỬ LÍ SỰ KIỆN
        private void btn_them_Click_1(object sender, EventArgs e)
        {

            Themsanpham f = new Themsanpham();
            f.isEdit = false;
            f.Show();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                Themsanpham f = new Themsanpham();
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
                    MessageBox.Show("Sản phẩm này hiện dang được sử dụng, bạn không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            // dataGridView1.DataSource = spbus.Search(txt_tentimkiem.Text, cb_phanloai.SelectedItem.ToString(), cb_boloc.SelectedItem.ToString());
            Tim_Kiem();
        }
        private void btn_chitiet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                XemChiTietSanPham f = new XemChiTietSanPham();
                f.isAdmin = this.isAdmin;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                f.idsanpham = row.Cells[0].Value.ToString();
                f.non_idsanpham = f.idsanpham;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xem thông tin chi tiết", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //CÁC HÀM BỔ TRỢ
        public void Load_DS_San_Pham()
        {
            //dataGridView1.DataSource = spbus.GetData1();
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = db.SanPhams.Select(p => new
                {
                   ID = p.IDSanPham,
                   Ten = p.Ten,
                    PhanLoai = p.PhanLoai,
                    GiaBan = p.GiaBan,
                    GiaNhap = p.GiaNhap,
                });
                dataGridView1.DataSource = li.ToList();
            }
        }
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
