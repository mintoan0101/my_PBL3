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
            var li = pbl.KhachHang.Select(p => new
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
            foreach (KhachHang item in pbl.KhachHang)
            {
                if (kh.SDT == item.SDT)
                {
                    return 0;
                }
                    
            }
            pbl.KhachHang.Add(kh);
                pbl.SaveChanges();
                return 1;
           
        }
        public int Delete(string id)
        {
            
                var kh = pbl.KhachHang.Where(p => p.IDKhachHang == id).FirstOrDefault();
                pbl.KhachHang.Remove(kh);
                pbl.SaveChanges();
                return 1;

        }
        public int Update(KhachHang kh)
        {
             var khachHang = pbl.KhachHang.Where(p => p.IDKhachHang == kh.IDKhachHang).FirstOrDefault();
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
            kh = pbl.KhachHang.Where(p => p.SDT == SDT).FirstOrDefault();
            return kh;
        }

        public string GetLastID()
        {
            var li = pbl.KhachHang.OrderByDescending(p => p.IDKhachHang).FirstOrDefault().IDKhachHang;
            return li;
        }

        public double GetTongHoaDon(string idKhachHang)
        {
            double? tongHoaDon = pbl.HoaDon.Where(hd => hd.IDKhachHang == idKhachHang).Sum(hd => (double?)hd.TongTien);
            return tongHoaDon ?? 0;
        }

        public bool CheckEnableToDelete(string IDKH)
        {
            var li = pbl.HoaDon.Where(p => p.IDKhachHang.Contains(IDKH));
            if(li.Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
