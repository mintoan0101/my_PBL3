using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;

namespace DataAccessLayer
{
    public class ChiTietHoaDonNhapDAO
    {
        PBL3Entities1 db = new PBL3Entities1();
        public int Insert(ChiTietHoaDonNhap c)
        {
            db.ChiTietHoaDonNhaps.Add(c);
            db.SaveChanges();
            return 1;
        }
    }
}
