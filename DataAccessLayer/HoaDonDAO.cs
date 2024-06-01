using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;

namespace DataAccessLayer
{
    public class HoaDonDAO
    {

        PBL3Entities1 pbl = new PBL3Entities1();

        private static HoaDonDAO _Instance;
        public static HoaDonDAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new HoaDonDAO();
                }
                return _Instance;
            }
            private set { }
        }
        private HoaDonDAO()
        {

        }
        public List<dynamic> GetData()
        {
            var list = pbl.HoaDons.Select(p => new
            {
                p.IDHoaDon,
                p.NgayTaoHoaDon,
                p.IDNhanVien,
                p.IDKhachHang,
                p.ChietKhau,
                p.TongTien,
                p.LoiNhuan,
                
            });
            return list.ToList<dynamic>();
        }

        public int Insert(HoaDon hd)
        {
            
                pbl.HoaDons.Add(hd);
                pbl.SaveChanges();
                return 1;
            
          
        }

        public string GetLastID()
        {
            var li = pbl.HoaDons.OrderByDescending(p => p.IDHoaDon).FirstOrDefault().IDHoaDon;
            return li;
        }

        public List<HoaDon> Search(string txt, string phanloai, string boloc)
        {
            var query = pbl.HoaDons.Select(p => p);
            if (string.IsNullOrEmpty(txt) == false)
            {
                switch (phanloai)
                {
                    case "ID Hoá Đơn":
                        query = query.Where(hd => hd.IDHoaDon.Contains(txt));
                        break;
                    case "Ngày Tạo Hoá Đơn":
                        // Không thể sử dụng like trong LINQ to Entities với DateTime
                        break;
                    case "Nhân Viên":
                        query = query.Where(hd => hd.NhanVien.IDNhanVien.Contains(txt) || hd.NhanVien.TenNhanVien.Contains(txt));
                        break;
                    case "Khách Hàng":
                        query = query.Where(hd => hd.KhachHang.IDKhachHang.Contains(txt) || hd.KhachHang.Ten.Contains(txt));
                        break;
                    default:
                        break;
                }
            }

            if (boloc != "Tất Cả")
            {
                switch (boloc)
                {
                    case "< 100K":
                        query = query.Where(hd => hd.TongTien < 100);
                        break;
                    case "100K - 500K":
                        query = query.Where(hd => hd.TongTien >= 100 && hd.TongTien <= 500);
                        break;
                    case "500K - 1000K":
                        query = query.Where(hd => hd.TongTien >= 500 && hd.TongTien <= 1000);
                        break;
                    case "> 1000K":
                        query = query.Where(hd => hd.TongTien > 1000);
                        break;
                }
            }

            return query.ToList();
        }
        public int CountSoLuongSanPham(string IDHD)
        {
            var query = from c in pbl.ChiTietHoaDons
                        where c.IDHoaDon == IDHD
                        select c.SoLuong;

            List<int> soLuongList = query.ToList();
            int soLuong = 0;
            foreach (int soLuongChiTiet in soLuongList)
            {
                soLuong += soLuongChiTiet;
            }

            return soLuong; 
        }
        public decimal DoanhThuTheoNhanVien(string IDNV)
        {
            var li = pbl.HoaDons.GroupBy(p=> p.IDNhanVien).Select(p => new
            {
                IDNhanVien = p.Key,
                TongDoanhThu = p.Sum(hd => hd.TongTien)
            }) ;
            foreach(var item in li)
            {
                if(item.IDNhanVien == IDNV)
                {
                    return item.TongDoanhThu;
                }
            }
            return 0;
        }

        public decimal TinhLoiNhuan(List<ChiTietHoaDon> listChiTietHoaDon)
        {
            decimal loiNhuan = 0.000m;
            foreach (ChiTietHoaDon item in listChiTietHoaDon)
            {
                var sp = pbl.SanPhams.Find(pbl.ChiTietSanPhams.Find(item.IDChiTiet).IDSanPham);
                decimal giaNhap = sp.GiaNhap;
                decimal giaBan = sp.GiaBan;
                loiNhuan += (giaBan - giaNhap) * item.SoLuong;
            }
            return loiNhuan;
        }

    }
}

