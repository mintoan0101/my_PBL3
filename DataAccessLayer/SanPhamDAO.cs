using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data.MySqlClient;
using ValueObject;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace DataAccessLayer
{
    public class SanPhamDAO
    {
        PBL3Entities1 pbl = new PBL3Entities1();

        public List<SanPham> GetData()
        {
            var li = pbl.SanPham.Select(p => p);
            return li.ToList();
        }
        public List<SanPham> GetDataByID(string id)
        {
            var li = pbl.SanPham.Where(p => p.IDSanPham == id);
            return li.ToList();
        }
        public List<string> GetSeparatedDataByColumn()
        {
            var li = pbl.SanPham.Select(p => p.PhanLoai).Distinct().ToList();
            return li;
        }
        public int Insert(SanPham sp)
        {
                pbl.SanPham.Add(sp);
                pbl.SaveChanges();
                return 1;
            
        }
        public int Delete(string id)
        {
                var sp = pbl.SanPham.Where(p => p.IDSanPham == id).FirstOrDefault();
                pbl.SanPham.Remove(sp);
                pbl.SaveChanges();
                return 1;
        }
        public int Update(SanPham sp)
        {
            if (sp != null)
            {
                SanPham p = pbl.SanPham.Find(sp.IDSanPham);
                p.Ten = sp.Ten;
                p.GiaBan = sp.GiaBan;
                pbl.SaveChanges();
                return 1;
            }
            return 0;
        }

        public string GetLastID(string phanloai)
        {
            var li = pbl.SanPham.Where(p => p.PhanLoai == phanloai)
                                 .OrderByDescending(p => p.IDSanPham)
                                 .FirstOrDefault();
            return li.IDSanPham;
        }
       public string GetIDByIDCTSP(string idct)
        {
            var li = pbl.ChiTietSanPham.Find(idct);
            return li.SanPham.IDSanPham;
        }
       public bool CheckSanPhamCoTheXoa(string IDSP)
        {
            var li = pbl.ChiTietSanPham.Where(p => p.IDSanPham.Contains(IDSP)).Select(p => p);
            if(li.Count()>0)
            {
                return false;
            }
            return true;
        }
    }
}
