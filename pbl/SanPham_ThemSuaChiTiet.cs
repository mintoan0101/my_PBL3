using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;
using System.Windows.Forms;

namespace pbl
{
    public partial class SanPham_ThemSuaChiTiet : Form
    {
        #region
        public string IDChiTiet { get; set; }
        public string IDSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string TenNPP { get; set; }
        public string HSD { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public bool isEdit { get; set; }
        public bool isNhapKho { get; set; }
        #endregion
        ChiTietSanPhamBUS bus = new ChiTietSanPhamBUS();
        NhaPhanPhoiBUS nppbus = new NhaPhanPhoiBUS();
        public SanPham_ThemSuaChiTiet()
        {
            InitializeComponent();
        }

        private void Them_SuaChiTietSanPham_Load(object sender, EventArgs e)
        {
            
            if(isNhapKho)
            {
                cb_npp.Enabled = false; ;
            }
            Load_Nha_Phan_Phoi();
            if (isEdit == true)
            {
                Load_Thong_Tin_Chi_Tiet();
                lbl_infor.Text = "Cập nhật thông tin chi tiết sản phẩm";

            }
            else
            {
                Load_IDChiTiet();

            }
        }
        //CÁC HÀM XỬ LÍ SỰ KIỆN

        private void btn_ok_Click(object sender, EventArgs e)
        {

            string warning = "";
            if (!Check_Day_Du_Thong_Tin()) warning = " Vui lòng nhập đầy đủ thông tin";
            else if (!CheckSoLuongHopLe()) warning = "Số lượng nhập vào không hợp lệ. ";
            else if (!CheckGiaSanPham()) warning = "Giá nhập vào không hợp lệ. ";
            else if (!Check_So_Luong_Khong_Qua_Lon()) warning = " Số lượng nhập vào quá lớn. ";
            else if (!Check_Gia_Khong_Qua_Lon()) warning = " Giá nhập vào quá lớn. ";
            if (warning.Trim().Length == 0)
            {
                if (isEdit == true)
                {
                    if (bus.Update(GetCTSP()) > 0)
                    {
                        MessageBox.Show("Đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    if (bus.Insert(GetCTSP()) > 0)
                    {
                        if (!isNhapKho)
                        {
                            MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show(warning, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //CÁC HÀM BỔ TRỢ
        public void Load_Thong_Tin_Chi_Tiet()
        {
            lbl_id.Text = IDChiTiet;
            cb_npp.SelectedItem = TenNPP;
            dateTimePicker1.Value = Convert.ToDateTime(HSD);
            txt_soluong.Text = SoLuong + "";
            txt_gianhap.Text = GiaNhap.ToString();

        }
        public void Load_Nha_Phan_Phoi()
        {
            List<string> ds = nppbus.GetName();
            foreach (string s in ds)
            {
                cb_npp.Items.Add(s);
            }
            cb_npp.SelectedIndex = 0;
        }

        public void Load_IDChiTiet()
        {
            string pre = "CT" + IDSanPham.Substring(0, 1);
            string lastID = bus.GetLastID(pre);
            int num = int.Parse(lastID.Substring(3)) + 1;
            if (num < 10)
            {
                lbl_id.Text = pre + "00" + num;
            }
            else if (num < 100)
            {
                lbl_id.Text = pre + "0" + num;

            }
            else
            {
                lbl_id.Text = pre + num;
            }
        }
        public ChiTietSanPham GetCTSP()
        {
      
            ChiTietSanPham ctsp = new ChiTietSanPham();
            ctsp.IDChiTiet = lbl_id.Text;
            ctsp.IDNhaPhanPhoi = bus.GetIDNPP(cb_npp.SelectedIndex + 1);
            ctsp.IDSanPham = IDSanPham;
            ctsp.HanSuDung = dateTimePicker1.Value;
            ctsp.SoLuong = int.Parse(txt_soluong.Text);
            ctsp.GiaNhap = decimal.Parse(txt_gianhap.Text);
            return ctsp;
        }
        private bool Check_Day_Du_Thong_Tin()
        {
            if(String.IsNullOrEmpty(txt_gianhap.Text) || String.IsNullOrEmpty(txt_soluong.Text))
            {
                return false;
            }
            return true;
        }
        private bool CheckSoLuongHopLe()
        {
            int res = 0;
            if (int.TryParse(txt_soluong.Text, out res))
            {
                return true;
            }
            return false;
        }
        private bool Check_So_Luong_Khong_Qua_Lon()
        {
            if(int.Parse(txt_soluong.Text.Trim()) <= 10000)
            {
                return true;
            }
            return false;
        }
        private bool CheckGiaSanPham()
        {
            decimal res2 = 0;
            if (decimal.TryParse(txt_gianhap.Text, out res2))
            {
                return true;
            }
            return false;
        }
        private  bool Check_Gia_Khong_Qua_Lon()
        {
            if(decimal.Parse(txt_gianhap.Text.Trim()) <= 10000000)
            {
                return true;
            }
            return false;
        }
        
        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_soluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_gianhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
