﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValueObject;
using BusinessLogicLayer;
using System.Web.UI.WebControls.WebParts;

namespace pbl
{
    public partial class HoaDonNhap_DanhSach : Form
    {
        HoaDonNhapBUS bus = new HoaDonNhapBUS();
        public Form currentFormChild;
        public HoaDonNhap_DanhSach()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.RowTemplate.Height = 30;
        }
        private void HoaDonNhap_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Load_Sap_Xep_Theo();
            Load_Trang_Thai();
            checkBox1.Checked = false;
            btn_timkiem.PerformClick();
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
        private void Set_Col()
        {
            dataGridView1.Columns[0].HeaderText = "ID Hóa Đơn Nhập";
            dataGridView1.Columns[1].HeaderText = "Ngày Tạo";
            dataGridView1.Columns[2].HeaderText = "Tổng Tiền";
            dataGridView1.Columns[3].HeaderText = "Trạng Thái";
        }
        //CÁC HÀM SỰ KIỆN
        private void btn_them_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhoHang_NhapHang());
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Hien_Thi_Tim_Kiem();
            Set_Col();
        }
        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if(bus.UpdateStatus(id)>0)
                {
                    MessageBox.Show("Cập nhật trạng thái hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn bạn muốn cập nhật trạng thái", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //CÁC HÀM BỔ TRỢ
        public void Load_Trang_Thai()
        {
            cb_trangthai.Items.Add("Tất Cả");
            cb_trangthai.Items.Add("Chưa Xử Lí");
            cb_trangthai.Items.Add("Đã Xử Lí");
            cb_trangthai.SelectedItem = "Tất Cả";
        }
        public void Load_Sap_Xep_Theo()
        {
            cb_sapxep.Items.Add("ID");
            cb_sapxep.Items.Add("Ngày Tạo");
            cb_sapxep.Items.Add("Tổng Tiền");
            cb_sapxep.SelectedItem = "Ngày Tạo";
        }
       
        public void Hien_Thi_Tim_Kiem()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                string status = cb_trangthai.SelectedItem.ToString();
                string order = cb_sapxep.SelectedItem.ToString();
                bool TangDan = checkBox1.Checked;
                if (status == "Tất Cả") status = "";
                var li = db.HoaDonNhaps.Select(h => new
                {
                    ID = h.IDHoaDonNhap,
                    NgayTao = h.NgayTao,
                    TongTien = h.TongTien,
                    TrangThai = h.TrangThai,
                }).Where(h => h.TrangThai.Contains(status));
                if(order == "ID")
                {
                    if(TangDan)
                    {
                        li = li.OrderBy(h => h.ID);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.ID);
                    }
                }
                else if(order == "Ngày Tạo")
                {
                    if (TangDan)
                    {
                        li = li.OrderBy(h => h.NgayTao);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.NgayTao);
                    }
                }
                else if (order == "Tổng Tiền")
                {
                    if (TangDan)
                    {
                        li = li.OrderBy(h => h.TongTien);
                    }
                    else
                    {
                        li = li.OrderByDescending(h => h.TongTien);
                    }
                }
                dataGridView1.DataSource = li.ToList();
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HoaDonNhap_ChiTiet x = new HoaDonNhap_ChiTiet();
            x.IDHoaDonNhap = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            OpenChildForm(x);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
