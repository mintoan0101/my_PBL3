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
    public partial class SanPham_Them : Form
    {
        SanPhamBUS sanphambus = new SanPhamBUS();
        public bool isEdit { get; set; }
        public string idsanpham {  get; set; }
        public string phanloai {  get; set; }
        public string tensanpham { get; set; }
        public string giaban { get; set; }
        public string gianhap { get; set; }
        //CÁC HÀM KHỞI TẠO CƠ BẢN
        public SanPham_Them()
        {
            InitializeComponent();
           
        }
        private void Themsanpham_Load(object sender, EventArgs e)
        {
            Load_Phan_Loai();
            if(isEdit)
            {
                cb_phanloai.Enabled = false;
                Load_Thong_Tin();
                lbl_infor.Text = "Cập nhật thông tin sản phẩm";
            }
       
        }
        //CÁC HÀM XỬ LÍ SỰ KIỆN
       
        private void btn_ok_Click(object sender, EventArgs e)
        {
            string mess = "";
            if (!Check_Day_Du_ThongTin()) mess = " Vui lòng điền đầy đủ thông tin. ";
            else if (!Check_Gia_Tien_Hop_Le()) mess = " Giá tiền chỉ được chứa kí tự số. ";
            else if (!Check_gia_Tien_Khong_Am()) mess = " Giá tiền không được phép âm. ";
            else if (!Check_Gia_Tien_Khong_Qua_Lon()) mess = " Giá trị sản phẩm quá lớn. ";
            if(String.IsNullOrEmpty(mess))
            {
                if (isEdit)
                {
                    Chinh_Sua_Thong_Tin();
                }
                else
                {
                    Them_San_Pham(San_Pham_Can_Them());
                }
            }
            else
            {
                MessageBox.Show(mess,"Lỗi nhập vào",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cb_phanloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hien_Thi_ID_Tu_Dong(cb_phanloai.SelectedItem.ToString());
        }
        private void txt_tensp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        // CÁC HÀM CHECK LỖI
        private bool Check_Day_Du_ThongTin()
        {
            if( !string.IsNullOrEmpty(txt_tensp.Text) &&
                !string.IsNullOrEmpty(lbl_id.Text) &&
                !string.IsNullOrEmpty(txt_giatien.Text))
            {
                return true;
            }
            return false;
        }
        private bool Check_Gia_Tien_Hop_Le()
        {
            decimal a = 0;
            if(decimal.TryParse(txt_giatien.Text.Trim(), out a))
            {
                return true;
            }
            return false;
        }
        private bool Check_gia_Tien_Khong_Am()
        {
            if(decimal.Parse(txt_giatien.Text.Trim())>=0)
            {
                return true;
            }
            return false;
        }
        private bool Check_Gia_Tien_Khong_Qua_Lon()
        {
            if(decimal.Parse(txt_giatien.Text.Trim()) <= 10000000)
            {
                return true;
            }
            return false;
        }
        //CÁC HÀM BỔ TRỢ
        public void Load_Phan_Loai()
        {
            List<string> list = sanphambus.GetSeperatedDataByColumn();
            foreach (string s in list) 
            {
                cb_phanloai.Items.Add(s);                             
            }
        }
        public void Hien_Thi_ID_Tu_Dong(string ten_danh_muc)
        {
            string id = "";
            string last_id = sanphambus.GetLastID(ten_danh_muc);
            string prefix = last_id.Substring(0, 1); // Lấy ký tự đầu tiên
            int number = int.Parse(last_id.Substring(1))+1;//Lấy các kí tự còn lại và ép kiểu
            if (number <= 9)
            {
                id += prefix + "0" + number;
            }
            else
            {
                id += prefix + number;
            }
            lbl_id.Text = id;
        }
        public SanPham San_Pham_Can_Them()
        {
            SanPham sp = new SanPham();
            sp.IDSanPham = lbl_id.Text;
            sp.Ten = txt_tensp.Text;
            sp.PhanLoai = cb_phanloai.SelectedItem.ToString();
            sp.GiaBan = decimal.Parse(txt_giatien.Text);
            return sp;
        }
        public bool CheckGiaSanPham()
        {
            double res = 0;
            if(double.TryParse(txt_giatien.Text, out res))
            {
                return true;
            }
            return false;
        }
        public void Them_San_Pham(SanPham sp)
        {
            if(CheckGiaSanPham())
            {
                if(sanphambus.Insert(sp) == 1)
                {
                    MessageBox.Show("Đã thêm sản phẩm thành công");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng giá tiền");
            }
            
        }
  
       public void Load_Thong_Tin()
        {
            lbl_id.Text = idsanpham;
            cb_phanloai.SelectedItem = phanloai;
            txt_tensp.Text = tensanpham;
            txt_giatien.Text = giaban; 
        }
        public void Chinh_Sua_Thong_Tin()
        {
            SanPham sp = new SanPham();
            sp.IDSanPham = idsanpham;
            sp.PhanLoai = phanloai;
            sp.Ten = txt_tensp.Text;
            sp.GiaBan = decimal.Parse(txt_giatien.Text);
            if(sanphambus.Update(sp) == 1)
            {
                MessageBox.Show("Đã sửa đổi thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa đổi không thành công");
            }
        }

        private void txt_giatien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
