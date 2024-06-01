using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;
using ZstdSharp.Unsafe;

namespace DataAccessLayer
{
    public class KhachHangDAO
    {
        PBL3Entities1 pbl = new PBL3Entities1();  
        private static KhachHangDAO _Instance;
        public static KhachHangDAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new KhachHangDAO();
                }
                return _Instance;
            }
            private set { }
        }
        private KhachHangDAO()
        {

        }
        public List<dynamic> GetData()
        {
            var li = pbl.KhachHangs.Select(p => new
            {
                p.IDKhachHang,
                p.Ten,
                p.SDT,
                p.Diem
            });
            return li.ToList<dynamic>();
        }
      
        public int Insert(KhachHang kh)
        {
           
                pbl.KhachHangs.Add(kh);
                pbl.SaveChanges();
                return 1;
           
        }
        public int Delete(string id)
        {
            
                var kh = pbl.KhachHangs.Where(p => p.IDKhachHang == id).FirstOrDefault();
                pbl.KhachHangs.Remove(kh);
                pbl.SaveChanges();
                return 1;

        }
        public int Update(KhachHang kh)
        {
             var khachHang = pbl.KhachHangs.Where(p => p.IDKhachHang == kh.IDKhachHang).FirstOrDefault();
                if (khachHang != null)
                {
                    khachHang.SDT = kh.SDT;
                    khachHang.Ten = kh.Ten;
                    khachHang.Diem = kh.Diem;
                    pbl.SaveChanges();
                    return 1;
                }
                return 0;
        }

        public KhachHang GetDataBySDT(string SDT)
        {
            KhachHang kh = new KhachHang();
            kh = pbl.KhachHangs.Where(p => p.SDT == SDT).FirstOrDefault();
            return kh;
        }

        public string GetLastID()
        {
            var li = pbl.KhachHangs.OrderByDescending(p => p.IDKhachHang).FirstOrDefault().IDKhachHang;
            return li;
        }

        public double GetTongHoaDon(string idKhachHang)
        {
            double? tongHoaDon = pbl.HoaDons.Where(hd => hd.IDKhachHang == idKhachHang).Sum(hd => (double?)hd.TongTien);
            return tongHoaDon ?? 0;
        }

        public List<KhachHang> Search(string txt, string PhanLoai, string BoLoc)
        {
            var query = pbl.KhachHangs.ToList();
            Dictionary<string, double> tongHoaDonDict = new Dictionary<string, double>();
            foreach (var khachHang in query)
            {
                tongHoaDonDict[khachHang.IDKhachHang] = GetTongHoaDon(khachHang.IDKhachHang);
            }
            if (!string.IsNullOrEmpty(txt))
            {
                if (PhanLoai == "Tên")
                {
                    txt = txt.ToLower();
                    query = query.Where(kh => kh.Ten.ToLower().Contains(txt)).ToList();
                }
                if (PhanLoai == "SĐT")
                {
                    query = query.Where(kh => kh.SDT.Contains(txt)).ToList();
                }
            }

            if (BoLoc != "Tất cả")
            {
                switch (BoLoc)
                {
                    case "< 500k":
                        query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] < 500).ToList();
                        break;
                    case "500k - 1 triệu":
                        query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] >= 500 && tongHoaDonDict[kh.IDKhachHang] <= 1000).ToList();
                        break;
                    case "1 - 5 triệu":
                        query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] >= 1000 && tongHoaDonDict[kh.IDKhachHang] <= 5000).ToList();
                        break;
                    case "5 - 17 triệu":
                        query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] >= 5000 && tongHoaDonDict[kh.IDKhachHang] <= 17000).ToList();
                        break;
                    case "> 17 triệu":
                        query = query.Where(kh => tongHoaDonDict[kh.IDKhachHang] > 17000).ToList();
                        break;
                }
            }

            return query;
        }
        public bool CheckEnableToDelete(string IDKH)
        {
            var li = pbl.HoaDons.Where(p => p.IDKhachHang.Contains(IDKH));
            if(li.Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
