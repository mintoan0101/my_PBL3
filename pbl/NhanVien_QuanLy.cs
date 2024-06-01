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
    public partial class NhanVien_QuanLy : Form
    {
        NhanVienBUS nvbus = new NhanVienBUS();
        TaiKhoanBUS tkbus = new TaiKhoanBUS();
        //CÁC HÀM KHỞI TẠO CƠ BẢN
        public NhanVien_QuanLy()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 10);
            dataGridView1.RowTemplate.Height = 30;
        }
        private void Quanlynhanvien_Load(object sender, EventArgs e)
        {
            Load_DS_Nhan_Vien();
            Load_Thuoc_Tinh();
            Load_Bo_Loc();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        //CÁC HÀM XỬ LÍ SỰ KIỆN

        private void btn_sua_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                NhanVien_Them f = new NhanVien_Them();
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                f.idnv = row.Cells[0].Value.ToString();
                f.isEdit = true;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
            }
        }

        private void btn_xoa_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string IDNV = row.Cells[0].Value.ToString();
                string TenNV = row.Cells[2].Value.ToString();
                if (nvbus.CheckEnableToDelete(IDNV))
                {
                    DialogResult res = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên " + TenNV , "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        if (nvbus.Delete(row.Cells[0].Value.ToString()) > 0)
                        {
                            MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn không thể xóa nhân viên "+TenNV+" vì nhân viên này hiện đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên bạn muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
            }
        }
        private void btn_them_Click_1(object sender, EventArgs e)
        {
            NhanVien_Them f = new NhanVien_Them();
            f.isEdit = false;
            f.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            NhanVien_DoiMatKhau f = new NhanVien_DoiMatKhau();
            f.ShowDialog();
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Hien_Thi_Tim_Kiem();
        }
        //CÁC HÀM BỔ TRỢ
        public void Load_DS_Nhan_Vien()
        {
            //dataGridView1.DataSource = nvbus.GetData();
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from p in db.NhanViens
                         select new
                         {
                             p.IDNhanVien,
                             p.IDTaiKhoan,
                             p.TenNhanVien,
                             p.TaiKhoan.TenTaiKhoan,
                             p.TaiKhoan.MatKhau,
                             p.SoDienThoai,
                             p.Nam,
                             p.NgaySinh,
                             p.Email,
                             p.CCCD,
                             p.MucLuong,
                             p.DiaChi,
                         };
                dataGridView1.DataSource = li.ToList();
            }

        }
        public void Load_Thuoc_Tinh()
        {
            cb_thuoctinh.Items.Add("ID Nhân Viên");
            cb_thuoctinh.Items.Add("Họ và Tên");
            cb_thuoctinh.Items.Add("Số Điện Thoại");
            cb_thuoctinh.SelectedItem = "ID Nhân Viên";
        }
        public void Load_Bo_Loc()
        {
            cb_boloc.Items.Add("Tất Cả");
            cb_boloc.Items.Add("Nam");
            cb_boloc.Items.Add("Nữ");
            cb_boloc.SelectedItem = "Tất Cả";
        }
        public void Hien_Thi_Tim_Kiem()
        {
            //dataGridView1.DataSource = nvbus.Search(txt_timkiem.Text, cb_thuoctinh.SelectedItem.ToString(), cb_boloc.SelectedItem.ToString());
            using (PBL3Entities1 pbl = new PBL3Entities1())
            {
                string text = txt_timkiem.Text;
                string thuoctinh = cb_thuoctinh.SelectedItem.ToString();
                string boloc = cb_boloc.SelectedItem.ToString();
                var res = from p in pbl.NhanViens
                          select new
                          {
                              p.IDNhanVien,
                              p.IDTaiKhoan,
                              p.TenNhanVien,
                              p.TaiKhoan.TenTaiKhoan,
                              p.TaiKhoan.MatKhau,
                              p.SoDienThoai,
                              p.Nam,
                              p.NgaySinh,
                              p.Email,
                              p.CCCD,
                              p.MucLuong,
                              p.DiaChi,
                          };
                if (string.IsNullOrEmpty(text) == false)
                {
                    if (thuoctinh == "ID Nhân Viên")
                    {
                        res = res.Where(p => p.IDNhanVien.Contains(text));
                    }
                    else if (thuoctinh == "Họ và Tên")
                    {
                        res = res.Where(p => p.TenNhanVien.Contains(text));
                    }
                    else if (thuoctinh == "Số Điện Thoại")
                    {
                        res = res.Where(p => p.SoDienThoai.Contains(text));
                    }
                }
                if (boloc != "Tất Cả")
                {
                    res = res.Where(p => p.Nam == (boloc == "Nam") ? true : false);
                }
                dataGridView1.DataSource = res.ToList();
            }
        }

        private void cb_thuoctinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
