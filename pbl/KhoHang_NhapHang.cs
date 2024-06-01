using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Xml;
using ValueObject;

namespace pbl
{
    public partial class KhoHang_NhapHang : Form
    {
        NhaPhanPhoiBUS nppbus = new NhaPhanPhoiBUS();
        HoaDonNhapBUS hdnbus = new HoaDonNhapBUS();
        ChiTietHoaDonNhapBUS ctbus = new ChiTietHoaDonNhapBUS();
        SanPhamBUS spbus = new SanPhamBUS();
        ChiTietSanPhamBUS ctspbus = new ChiTietSanPhamBUS();
        public KhoHang_NhapHang()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Semibold", 11);
            dataGridView1.RowTemplate.Height = 28;
            dataGridView2.Font = new Font("Segoe UI Semibold", 8);
            dataGridView2.RowTemplate.Height = 26;
        }
        private void NhapKho_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Load_CTSP();
            Load_Phan_Loai();
            Load_ID_Tu_Dong();
            Load_trang_Thai();
            Load_Nha_Phan_Phoi();
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Dieu_Chinh_DataGridView();
        }
        private void Dieu_Chinh_DataGridView()
        {
            int total = dataGridView2.Width;
            int part = total / 20;
            int i = 1;
            int count = dataGridView1.Columns.Count;
            int rate = 1;
            foreach (DataGridViewColumn c in dataGridView2.Columns)
            {
                if (i == 1) rate = 2;
                else if (i == 2) rate = 7;
                else if (i == 3) rate = 2;
                else if (i == 4) rate = 3;
                c.Width = rate * part;
                i++;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        //CÁC HÀM SỰ KIỆN
        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bt_ThanhToan_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count>1)
            {
                if(hdnbus.Insert(GetHoaDonNhap())>0)
                {
                    Cap_Nhap_CTSP_Vua_Tao();
                    Them_Chi_Tiet_Hoa_Don_Nhap();    
                    MessageBox.Show("Đã thêm hóa đơn nhập thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Load_ID_Tu_Dong();
                    btn_reset.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Hiện hóa đơn chưa có sán phẩm nào được chọn", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void bt_TimKiem_Click(object sender, EventArgs e)
        {
            Hien_Thi_Tim_Kiem();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Them_San_Pham_Vao_Hoa_Don();
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Xoa_Bot_San_Pham();
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            Xoa_Toan_Bo_CTSP();
            lbl_tongtien.Text = "0";
            cb_trangthai.SelectedItem = "Đã Xử Lí";
        }
        //CÁC HÀM BỔ TRỢ
        //THAO TÁC LOAD CÁC DỮ LIỆU VÀO TÌM KIẾM
        public void Load_ID_Tu_Dong()
        {
            string lastID = hdnbus.GetLastID();
            int num = int.Parse(lastID.Substring(3)) + 1;
            string id = "HDN";
            if (num < 10)
            {
                id += "00" + num;
            }
            else if (num < 100)
            {
                id += "0" + num;
            }
            else
            {
                id += num;
            }
            lbl_id.Text = id.ToString();
        }
        public void Load_CTSP()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var li = from c in db.SanPhams
                         select new { 
                             ID = c.IDSanPham,
                             Ten = c.Ten,
                             PhanLoai =c.PhanLoai,
                             GiaNhap = c.GiaNhap,
                         };
                dataGridView1.DataSource = li.ToList();
            }
        }
        public void Load_trang_Thai()
        {
            cb_trangthai.Items.Add("Đã Xử Lí");
            cb_trangthai.Items.Add("Chưa Xử Lí");
            cb_trangthai.SelectedItem = "Đã Xử Lí";
        }
        public void Load_Phan_Loai()
        {
            cb_phanloai.Items.Add("Tất Cả");
            foreach (string s in spbus.GetSeperatedDataByColumn())
            {
                cb_phanloai.Items.Add(s);
            }
            cb_phanloai.SelectedItem = "Tất Cả";
        }
        public void Load_Nha_Phan_Phoi()
        {
            foreach(string s in nppbus.GetName())
            {
                cb_npp.Items.Add(s);
            }
            cb_npp.SelectedIndex = 0;
        }
        public void Hien_Thi_Tim_Kiem()
        {
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                string id = txt_id.Text;
                string npp = cb_phanloai.SelectedItem.ToString();
                if (npp == "Tất Cả") npp = "";
                var li = db.SanPhams.Select(c => new
                {
                    ID = c.IDSanPham,
                    Ten = c.Ten,
                    PhanLoai = c.PhanLoai,
                    GiaNhap = c.GiaNhap,
                }).Where(c => c.PhanLoai.Contains(npp) && c.ID.Contains(id));
                dataGridView1.DataSource = li.ToList();
            }
        }
        //THAO TÁC VỚI DATAGRIDVIEW HÓA ĐƠN 
        public void Them_San_Pham_Vao_Hoa_Don()
        {
            string sanpham = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string idsp = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            int vitri = Check_Trung_San_Pham(idsp);
            if ( vitri == -1)
            {
                string gianhap = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                Them_Moi_CTSP(idsp);
                int sl = ctspbus.GetSoLuongByLastID(idsp);
                dataGridView2.Rows.Add(idsp, sanpham, gianhap, sl);
            }
            Load_Tong_Tien();
        }
        public void Xoa_Bot_San_Pham()
        {
            DataGridViewRow r = dataGridView2.SelectedRows[0];
            Xoa_Bot_CTSP(r.Cells[0].Value.ToString());
            dataGridView2.Rows.RemoveAt(r.Index);
            Load_Tong_Tien();
        }
        public void Xoa_Toan_Bo_CTSP()
        {
            if(dataGridView2.Rows.Count > 1)
            {
                foreach(DataGridViewRow r in dataGridView2.Rows)
                {
                    Xoa_Bot_CTSP(r.Cells[0].Value.ToString());
                    dataGridView2.Rows.RemoveAt(r.Index);
                }
                Load_Tong_Tien();
            }
        }
        public int Check_Trung_San_Pham(string idsp)
        {
            int vitri = 0;
            if (dataGridView2.Rows.Count > 1)
            {
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    if (vitri == dataGridView2.Rows.Count -1) break;
                    if (idsp == r.Cells[0].Value.ToString())
                    {
                        return vitri; ;
                    }
                    vitri++;
                }
            }
            return -1;
        }
        //THÊM MỚI CHI TIẾT SP MỖI KHI THÊM MỚI 1 SẢN PHẨM VÀO HÓA ĐƠN
        public void Them_Moi_CTSP(string idsp)
        {
            SanPham_ThemSuaChiTiet f = new SanPham_ThemSuaChiTiet();
            f.isNhapKho = true;
            f.IDSanPham = idsp;
            f.ShowDialog();
        }
        public void Xoa_Bot_CTSP(string idsp)
        {
            ctspbus.DeleteLastID(idsp);
        }
        //lOAD CÁC THÔNG SỐ KHÁC CỦA HÓA ĐƠN
        public void Load_Tong_Tien()
        {
            decimal tong = 0;
            int vt = 1;
            if (dataGridView2.Rows.Count>1)
            {
                foreach(DataGridViewRow r in dataGridView2.Rows)
                {
                   
                    if (dataGridView2.Rows.Count == vt) break;
                    decimal gianhap = decimal.Parse(r.Cells[2].Value.ToString()); 
                    int sl = int.Parse(r.Cells[3].Value.ToString());
                    tong += sl * gianhap;
                    vt++;
                }
            }
            lbl_tongtien.Text = tong+"" ;
        } 
        public HoaDonNhap GetHoaDonNhap()
        {
            string id = lbl_id.Text;
            DateTime ngay = DateTime.Now;
            decimal tongtien = decimal.Parse(lbl_tongtien.Text);
            string trangthai = cb_trangthai.SelectedItem.ToString();
            HoaDonNhap hd = new HoaDonNhap(id,ngay,tongtien,trangthai);
            return hd;
        }
        public void Them_Chi_Tiet_Hoa_Don_Nhap()
        {
            int vt = 1;
            string id = lbl_id.Text;
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (dataGridView2.Rows.Count == vt) break;
                string idsp = r.Cells[0].Value.ToString();
                string idct = ctspbus.GetLastIDByIDSP(idsp);
                int sl = int.Parse(r.Cells[3].Value.ToString());
                ChiTietHoaDonNhap c = new ChiTietHoaDonNhap(id,idct,sl);
                ctbus.Insert(c);
                vt++;
            }
        }
        public List<string> GetIDSP_HoaDon()
        {
            List<string> ds_IDSP = new List<string>();
            int vt = 1;
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (vt == dataGridView2.Rows.Count) break;
                ds_IDSP.Add(r.Cells[0].Value.ToString());
                vt++;
            }
            return ds_IDSP;
        }
        public void Cap_Nhap_CTSP_Vua_Tao()
        {
            List<string> ds_IDSP = GetIDSP_HoaDon();
            string npp = cb_npp.SelectedItem.ToString();
            int vt = 1;
            int length = dataGridView2.Rows.Count;
            foreach(DataGridViewRow r in dataGridView2.Rows)
            {
                if (vt == length) break;
                string idsp = r.Cells[0].Value.ToString();
                int sl = int.Parse(r.Cells[3].Value.ToString());
                ctspbus.UpdateThongSo(idsp,npp);
                vt++;
            }
        }

     
    }
}
