﻿using BusinessLogicLayer;
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
    public partial class Quanlynhanvien : Form
    {
        NhanVienBUS nvbus = new NhanVienBUS();
        TaiKhoanBUS tkbus = new TaiKhoanBUS();
        //CÁC HÀM KHỞI TẠO CƠ BẢN
        public Quanlynhanvien()
        {
            InitializeComponent();
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
                ThemNhanvien f = new ThemNhanvien();
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
                DialogResult res = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên " + row.Cells[2].Value.ToString(), "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                MessageBox.Show("Vui lòng chọn nhân viên bạn muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
            }
        }
        private void btn_them_Click_1(object sender, EventArgs e)
        {
            ThemNhanvien f = new ThemNhanvien();
            f.isEdit = false;
            f.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DatLaiMatKhau f = new DatLaiMatKhau();
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
                var li = from p in db.NhanVien
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
                var res = from p in pbl.NhanVien
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

       
    }
}