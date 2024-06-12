using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;

namespace DataAccessLayer
{
    public class HoaDonNhapDAO
    {
        PBL3Entities1 db = new PBL3Entities1();
        public int Insert(HoaDonNhap n)
        {
            db.HoaDonNhaps.Add(n);
            db.SaveChanges();
            return 1;
        }
        public int Delete(string id)
        {
            HoaDonNhap h = db.HoaDonNhaps.Find(id);
            db.HoaDonNhaps.Remove(h);
            db.SaveChanges();
            return 1;
        }
        public int Update(HoaDonNhap n)
        {
            HoaDonNhap h = db.HoaDonNhaps.Find(n.IDHoaDonNhap);
            h.NgayTao = n.NgayTao;
            h.TrangThai = n.TrangThai;
            h.TongTien = n.TongTien;
            db.SaveChanges();
            return 1;
        }
        public string GetLastId()
        {
            var li = db.HoaDonNhaps.Select(p => p).OrderByDescending(p => p.IDHoaDonNhap).FirstOrDefault();
            return li.IDHoaDonNhap;
        }
        public int UpdateStatus(string IDHD)
        {
            var re = db.HoaDonNhaps.Select(p => p).Where(p => p.IDHoaDonNhap.Contains(IDHD)).FirstOrDefault();
            string s= re.TrangThai.ToString();
            if(s =="Đã Xử Lí")
            {
                re.TrangThai = "Chưa Xử Lí";
            }
            else
            {
                re.TrangThai = "Đã Xử Lí";
            }
            db.SaveChanges();
            return 1;
        }
    }
}
