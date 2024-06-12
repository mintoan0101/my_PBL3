using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;

namespace DataAccessLayer
{
    public class ChiTietHoaDonDAO
    {
    
        PBL3Entities1 pbl = new PBL3Entities1();

        private static ChiTietHoaDonDAO _Instance;
        public static ChiTietHoaDonDAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ChiTietHoaDonDAO();
                }
                return _Instance;
            }
            private set { }
        }
        private ChiTietHoaDonDAO()
        {

        }
        public int Insert(ChiTietHoaDon cthd)
        {
            try
            {
                pbl.ChiTietHoaDons.Add(cthd);
                pbl.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                else
                {
                    Console.WriteLine("No inner exception found.");
                }
            }
            return 1;
          
        }
        public List<dynamic> GetData(string ID)
        {
            var li = pbl.ChiTietHoaDons.Where(p => p.IDHoaDon == ID)
                                       .Select(p => new { p.ChiTietSanPham.SanPham.Ten, p.SoLuong, p.ChiTietSanPham.SanPham.GiaBan });
            return li.ToList<dynamic>();
        }

    }
}
