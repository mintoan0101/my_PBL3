using BusinessLogicLayer;
using DataAccessLayer;
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
    public partial class KhachHang_NhanVien : Form
    {
        KhachHangBUS bus = new KhachHangBUS();

        //CÁC HÀM KHỞI TẠO CƠ BẢN
        public KhachHang_NhanVien()
        {
            InitializeComponent();
        }
        private void Khachhang_Nhanvien_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Font = new Font("Segoe UI Semibold", 12);
            dataGridView1.RowTemplate.Height = 30;
            Load_BoLoc();
            btn_timkiem.PerformClick();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
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
                if (i == 1) rate = 3;
                else if (i == 2) rate = 6;
                else if (i == 3) rate = 6;
                else if (i == 4) rate = 5;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void Set_Col()
        {
            dataGridView1.Columns[0].HeaderText = "ID Khách Hàng";
            dataGridView1.Columns[1].HeaderText = "Tên Khách Hàng";
            dataGridView1.Columns[2].HeaderText = "Số Điện Thoại";
            dataGridView1.Columns[3].HeaderText = "Điểm Tích Lũy";

        }
        //CÁC HÀM XỬ LÍ SỰ KIỆN
        private void btn_them_Click(object sender, EventArgs e)
        {
            KhachHang_Them f = new KhachHang_Them(null);
            f.isEdit = false;
            f.ShowDialog();
            btn_timkiem.PerformClick();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dataGridView1.SelectedRows[0];
                KhachHang_Them f = new KhachHang_Them(r.Cells["SDT"].Value.ToString());
                f.isEdit = true;
                f.kh = KhachHangBUS.Instance.GetKhachHangBySDT(r.Cells[0].Value.ToString());
                f.ShowDialog();
                btn_timkiem.PerformClick();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng bạn muốn chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            Tim_Kiem();
            Set_Col();
        }
        private void btn_xoa_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                string IDKH = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string TenKH = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                if (bus.CheckEnableToDelete(IDKH))
                {
                    DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng " + TenKH, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (re == DialogResult.Yes)
                    {
                        if (bus.Delete(IDKH) > 0)
                        {
                            MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btn_timkiem.PerformClick();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xóa vì khách hàng "+TenKH+" hiện đang được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng bạn muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //CÁC HÀM BỔ TRỢ
        
        public void Load_BoLoc()
        {
            cbb_BoLoc.Items.Add("< 500k");
            cbb_BoLoc.Items.Add("500k - 1 triệu");
            cbb_BoLoc.Items.Add("1 - 5 triệu");
            cbb_BoLoc.Items.Add("5 - 17 triệu");
            cbb_BoLoc.Items.Add(" > 17 triệu");
            cbb_BoLoc.Items.Add("Tất cả");
            cbb_BoLoc.SelectedItem = "Tất cả";
        }

        private void Tim_Kiem()
        {
            using (PBL3Entities1 pbl = new PBL3Entities1())
            {
                var query = pbl.KhachHangs.Select(p => new
                {
                    IDKhachHang = p.IDKhachHang,
                    Ten = p.Ten,
                    SDT = p.SDT,
                    Diem = p.Diem,
                });
                string txt = txt_timkiem.Text;
                string BoLoc = cbb_BoLoc.SelectedItem.ToString();
                Dictionary<string, double> tongHoaDonDict = new Dictionary<string, double>();
                foreach (var khachHang in query)
                {
                    tongHoaDonDict[khachHang.IDKhachHang] = KhachHangDAO.Instance.GetTongHoaDon(khachHang.IDKhachHang);
                }
                if (!string.IsNullOrEmpty(txt))
                {
                    txt = txt.ToLower();
                    query = query.Where(kh => kh.Ten.ToLower().Contains(txt) || kh.SDT.Contains(txt));
                }

                if (BoLoc != "Tất cả")
                {
                    switch (BoLoc)
                    {
                        case "< 500k":
                            query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] < 500);
                            break;
                        case "500k - 1 triệu":
                            query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] >= 500 && tongHoaDonDict[kh.IDKhachHang] <= 1000);
                            break;
                        case "1 - 5 triệu":
                            query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] >= 1000 && tongHoaDonDict[kh.IDKhachHang] <= 5000);
                            break;
                        case "5 - 17 triệu":
                            query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] >= 5000 && tongHoaDonDict[kh.IDKhachHang] <= 17000);
                            break;
                        case "> 17 triệu":
                            query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] > 17000);
                            break;
                    }
                }
                dataGridView1.DataSource = query.ToList();
            }
        }

    }
}
