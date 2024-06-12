using BusinessLogicLayer;
using DataAccessLayer;
using MySqlX.XDevAPI.Relational;
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
    public partial class HoaDon_Them : Form
    {
        public string IDNhanVien;
        private KhachHang kh = new KhachHang();
        private ChiTietSanPhamBUS ctspBUS = new ChiTietSanPhamBUS();
        private NhanVienBUS nvBUS = new NhanVienBUS();
        private bool DaDoiDiem = new bool();
        private List<ChiTietSanPham_View> list = new List<ChiTietSanPham_View>();
        private List<SoLuongCTSP> list_sl = new List<SoLuongCTSP>();
        public HoaDon_Them()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 10);
            dataGridView1.RowTemplate.Height = 30;
            dataGridView2.Font = new Font("Segoe UI Semibold", 8);
            dataGridView2.RowTemplate.Height = 28;
            list = ctspBUS.GetData1();
            list_sl = ctspBUS.GetSoLuongs();
            SetBill();
            AddColumnsToDataGridView2();
            panel10.Visible = false;
            panel11.Visible = false;
            cbb_PhanLoai.SelectedIndex = 0;
            bt_TimKiem_Click(this, new EventArgs());
        }
        private void Dieu_Chinh_DataGridView1()
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
                else if (i == 3) rate = 6;
                else if (i == 4) rate = 3;
                else if (i == 5) rate = 2;
                else if(i == 6) rate = 5;
                else if (i == 7) rate = 2;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void Dieu_Chinh_DataGridView2()
        {
            int total = dataGridView2.Width;
            int part = total / 20;
            int i = 1;
            int count = dataGridView2.Columns.Count;
            int rate = 1;
            foreach (DataGridViewColumn c in dataGridView2.Columns)
            {
                if (i == 1) rate = 3;
                else if (i == 2) rate = 6;
                else if (i == 3) rate = 4;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void Set_Col1()
        {
            dataGridView1.Columns[0].HeaderText = "ID Sản Phẩm";
            dataGridView1.Columns[1].HeaderText = "Phân Loại";
            dataGridView1.Columns[2].HeaderText = "Tên Sản Phẩm";
            dataGridView1.Columns[3].HeaderText = "Hạn Sử Dụng";
            dataGridView1.Columns[4].HeaderText = "Giá Bán";
            dataGridView1.Columns[5].HeaderText = "Số Lượng";
            dataGridView1.Columns[6].HeaderText = "Check";
        }
        private void SetBill()
        {
            lb_DateTime.Text = DateTime.Now.ToString();
            lb_ID.Text = HoaDonBUS.Instance.SetID();

        }

        //List lưu tên các cột trong DataGridView
        public List<string> GetColumnNamesFromDataGridView(DataGridView dataGridView)
        {
            List<string> columnNames = new List<string>();
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                columnNames.Add(column.HeaderText);
            }
            return columnNames;
        }
       
        private void AddColumnsToDataGridView2()
        {
            dataGridView2.Columns.Add("IDChiTiet", "IDChiTiet");
            dataGridView2.Columns.Add("Ten", "Tên");
            dataGridView2.Columns.Add("SoLuong", "Số Lượng");
            dataGridView2.Columns.Add("ThanhTien", "Thành Tiền");
            dataGridView2.ColumnHeadersVisible = true;
            dataGridView2.Columns["IDChiTiet"].Visible = false;
        }
        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bt_ThanhToan_Click(object sender, EventArgs e)
        {
            if (lb_Tong.Text != "0" && lb_IDKhachHang.Text != "ID:")
            {
                HoaDon hd = new HoaDon();
                hd.IDHoaDon = lb_ID.Text;
                hd.IDNhanVien = nvBUS.GetID(IDNhanVien);
                hd.IDKhachHang = lb_IDKhachHang.Text.Substring(4);
                hd.NgayTaoHoaDon = DateTime.Now;
                hd.ChietKhau = Convert.ToDecimal(lb_GiamGia.Text);
                hd.TongTien = Convert.ToDecimal(lb_Tong.Text);
                List<ChiTietHoaDon> listChiTietHoaDon = new List<ChiTietHoaDon>();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon
                        {
                            IDHoaDon = hd.IDHoaDon,
                            IDChiTiet = row.Cells["IDChiTiet"].Value.ToString(),
                            SoLuong = Convert.ToInt32(row.Cells["SoLuong"].Value)
                        };
                        ChiTietSanPham chiTietSanPham = ctspBUS.GetCTSP(chiTietHoaDon.IDChiTiet);
                        chiTietSanPham.SoLuong -= chiTietHoaDon.SoLuong;
                        listChiTietHoaDon.Add(chiTietHoaDon);
                        ctspBUS.Update(chiTietSanPham);
                    }
                }
                hd.ChiTietHoaDons = listChiTietHoaDon;
                hd.LoiNhuan = HoaDonBUS.Instance.TinhLoiNhuan(listChiTietHoaDon);
                HoaDonBUS.Instance.Insert(hd);
                foreach (ChiTietHoaDon chitiethoadon in hd.ChiTietHoaDons)
                {
                    ChiTietHoaDonDAO.Instance.Insert(chitiethoadon);
                }
                int diem = Convert.ToInt32(Convert.ToDecimal(lb_Tong.Text) / 20);
                kh.Diem += diem;
                KhachHangBUS.Instance.Update(kh);
                MessageBox.Show("Thanh toán thành công");
                this.Close();
            }
            else
            {
                MessageBox.Show("Hoá đơn không hợp lệ");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sdt = txt_STDKhachHang.Text;
            kh = KhachHangBUS.Instance.GetKhachHangBySDT(sdt);
            if (kh != null)
            {
                panel10.Visible = true;
                panel11.Visible = true;
                lb_IDKhachHang.Text = "ID: " + kh.IDKhachHang;
                lb_TenKhachHang.Text = "Tên: " + kh.Ten;
                lb_DiemThuong.Text = "Điểm Thưởng: " + kh.Diem.ToString();
            }
            else
            {
                KhachHang_Them f = new KhachHang_Them(null);
                f.ShowDialog();
            }

        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Check" && e.RowIndex >= 0 && e.RowIndex < list.Count)
            {
                ChiTietSanPham_View selectedItem = null;
                foreach (ChiTietSanPham_View item in list)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() == item.IDChiTiet)
                    {
                        selectedItem = item;
                        break;
                    }
                }

                if (selectedItem == null) return;

                int SoLuongCoSan = selectedItem.SoLuong;

                if (dataGridView1.Rows[e.RowIndex].Cells["Check"].Value != null)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Check"].Value))
                    {
                        if (SoLuongCoSan > 0)
                        {
                            selectedItem.SoLuong = SoLuongCoSan - 1;
                            selectedItem.Check = true;
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(dataGridView2);
                            newRow.Cells[0].Value = selectedItem.IDChiTiet;
                            newRow.Cells[1].Value = selectedItem.Ten;
                            newRow.Cells[2].Value = 1;
                            newRow.Cells[3].Value = selectedItem.GiaBan;
                            dataGridView2.Rows.Add(newRow);
                        }
                        else
                        {
                            MessageBox.Show("Sản phẩm không đủ số lượng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dataGridView1.Rows[e.RowIndex].Cells["Check"].Value = false;
                        }
                    }
                    else
                    {
                        string idChiTiet = selectedItem.IDChiTiet;
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == idChiTiet)
                            {
                                int SoLuong = Convert.ToInt32(row.Cells[2].Value);
                                selectedItem.SoLuong += SoLuong;
                                dataGridView2.Rows.Remove(row);
                                break;
                            }
                        }

                    }

                    if (DaDoiDiem)
                    {
                        kh.Diem += (int)Convert.ToDecimal(lb_GiamGia.Text);
                        lb_DiemThuong.Text = "Điểm Thưởng: " + kh.Diem.ToString();
                        lb_GiamGia.Text = "0";
                        DaDoiDiem = false;
                    }
                }

                TinhTien();
                Refresh_DataGridView();
            }
        }

        private void bt_TimKiem_Click(object sender, EventArgs e)
        {
            string PhanLoai = cbb_PhanLoai.SelectedItem.ToString();
            string txt = txt_TimKiem.Text;
            if (PhanLoai != null && txt != null)
            {
                dataGridView1.DataSource = ctspBUS.Search(PhanLoai, txt);
            }
            Set_Col1();
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell != null && dataGridView2.CurrentCell.OwningColumn.Name == "SoLuong")
            {
                DataGridViewRow currentRow = dataGridView2.CurrentCell.OwningRow;
                try
                {
                    // Kiểm tra nếu giá trị sau khi chỉnh sửa là null, chuỗi rỗng hoặc <= 0
                    if (currentRow.Cells["SoLuong"].Value == null ||
                        string.IsNullOrWhiteSpace(currentRow.Cells["SoLuong"].Value.ToString()) ||
                        Convert.ToInt32(currentRow.Cells["SoLuong"].Value) <= 0)
                    {
                        MessageBox.Show("Giá trị số lượng phải lớn hơn 0 và không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        currentRow.Cells["SoLuong"].Value = 1; // Hoặc giá trị mặc định nào đó
                        return;
                    }

                    int soLuongMoi = Convert.ToInt32(currentRow.Cells["SoLuong"].Value);
                    string idChiTiet = currentRow.Cells["IDChiTiet"].Value.ToString();
                    int tongSoLuong = 0;
                    foreach (SoLuongCTSP sl in list_sl)
                    {
                        if (sl.IDChiTiet == idChiTiet)
                        {
                            tongSoLuong = sl.SoLuong;
                            break;
                        }
                    }
                    foreach (ChiTietSanPham_View item in list)
                    {
                        if (item.IDChiTiet == idChiTiet)
                        {
                            if (tongSoLuong < soLuongMoi)
                            {
                                MessageBox.Show("Số lượng trong kho không đủ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                currentRow.Cells["SoLuong"].Value = tongSoLuong;
                            }
                            item.SoLuong = tongSoLuong - Convert.ToInt32(currentRow.Cells["SoLuong"].Value);
                            TinhTien();
                            Refresh_DataGridView();
                            break;
                        }
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Giá trị số lượng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentRow.Cells["SoLuong"].Value = 1; // giá trị mặc định 
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Giá trị số lượng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentRow.Cells["SoLuong"].Value = 1; // giá trị mặc định 
                }
            }
        }


        private void bt_DoiDiem_Click(object sender, EventArgs e)
        {
            if (DaDoiDiem == false)
            {
                HoaDon hd = new HoaDon();
                hd.IDHoaDon = lb_ID.Text;
                hd.ChietKhau = Convert.ToDecimal(lb_GiamGia.Text);
                hd.TongTien = Convert.ToDecimal(lb_Tong.Text);
                KhachHangBUS.Instance.DoiDiem(hd, kh);
                lb_DiemThuong.Text = "Điểm Thưởng: " + kh.Diem.ToString();
                lb_GiamGia.Text = hd.ChietKhau.ToString();
                lb_Tong.Text = hd.TongTien.ToString();
                DaDoiDiem = true;
            }

        }
        private void Refresh_DataGridView()
        {

            dataGridView1.DataSource = list;
            Dieu_Chinh_DataGridView1();
            dataGridView2.Refresh();

        }
        private void TinhTien()
        {
            Decimal thanhtien = 0.000m;
            foreach (DataGridViewRow item in dataGridView2.Rows)
            {
                thanhtien += Convert.ToDecimal(item.Cells[2].Value) * Convert.ToDecimal(item.Cells[3].Value);
            }
            lb_ThanhTien.Text = thanhtien.ToString();
            Decimal chietkhau = 0;
            if (DaDoiDiem)
            {
                chietkhau = Convert.ToDecimal(lb_GiamGia.Text);
            }
            lb_Tong.Text = (thanhtien - chietkhau).ToString();
        }

        private void HoaDon_Them_Load(object sender, EventArgs e)
        {
            Dieu_Chinh_DataGridView1();
            Dieu_Chinh_DataGridView2();
        }

        public List<ChiTietSanPham_View> LoadDataFromDataGridView(DataGridView dataGridView)
        {
            List<ChiTietSanPham_View> list = new List<ChiTietSanPham_View> ();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;
                ChiTietSanPham_View sp = new ChiTietSanPham_View
                {
                    IDChiTiet = row.Cells[0].Value.ToString(),
                    PhanLoai = row.Cells[1].Value.ToString(),
                    Ten = row.Cells[2].Value.ToString(),
                    HanSuDung = Convert.ToDateTime(row.Cells[3].Value),
                    GiaBan = Convert.ToDecimal(row.Cells[4].Value),
                    SoLuong = Convert.ToInt32(row.Cells[5].Value),
                    Check = Convert.ToBoolean(row.Cells[6].Value)
                };

                list.Add(sp);
            }

            return list;
        }

        private void txt_STDKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
    }
}

