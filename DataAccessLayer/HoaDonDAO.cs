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
            var list = pbl.HoaDon.OrderByDescending(p => p.NgayTaoHoaDon)
                .ThenByDescending(p => p.IDHoaDon)
                .Select(p => new
                {
                    p.IDHoaDon,
                    p.NhanVien.TenNhanVien,
                    p.NgayTaoHoaDon,
                    p.KhachHang.Ten,
                    p.ChietKhau,
                    p.TongTien,
                    p.LoiNhuan,

                });
            
            return list.ToList<dynamic>();
        }
        public List<dynamic> GetDataByNhanVien(string idnv)
        {
            var list = pbl.HoaDon.OrderByDescending(p => p.NgayTaoHoaDon)
                                  .ThenByDescending(p => p.IDHoaDon)
                                  .Where(p => p.IDNhanVien == idnv)
                                  .Select(p => new
                                  {
                                      p.IDHoaDon,
                                      p.NhanVien.TenNhanVien,
                                      p.NgayTaoHoaDon,
                                      p.KhachHang.Ten,
                                      p.ChietKhau,
                                      p.TongTien,
                                      p.LoiNhuan,
                                  })
                                  .ToList<dynamic>();
            return list;
        }

        public int Insert(HoaDon hd)
        {
            
                pbl.HoaDon.Add(hd);
                pbl.SaveChanges();
                return 1;
            
          
        }

        public string GetLastID()
        {
            var li = pbl.HoaDon.OrderByDescending(p => p.IDHoaDon).FirstOrDefault().IDHoaDon;
            return li;
        }

     
        public int CountSoLuongSanPham(string IDHD)
        {
            var query = from c in pbl.ChiTietHoaDon
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
            var li = pbl.HoaDon.GroupBy(p=> p.IDNhanVien).Select(p => new
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
                var ctsp = pbl.ChiTietSanPham.Find(item.IDChiTiet);
                var sp = pbl.SanPham.Find(ctsp.IDSanPham);
                decimal giaNhap = ctsp.GiaNhap;
                decimal giaBan = sp.GiaBan;
                loiNhuan += (giaBan - giaNhap) * item.SoLuong;
            }
            return loiNhuan;
        }

    }
}

