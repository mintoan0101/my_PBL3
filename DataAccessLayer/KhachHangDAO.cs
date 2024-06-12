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
            foreach (KhachHang item in pbl.KhachHangs)
            {
                if (kh.SDT == item.SDT)
                {
                    return 0;
                }
                    
            }
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
