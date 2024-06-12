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
            var li = pbl.SanPhams.Select(p => p);
            return li.ToList();
        }
        public List<SanPham> GetDataByID(string id)
        {
            var li = pbl.SanPhams.Where(p => p.IDSanPham == id);
            return li.ToList();
        }
        public List<string> GetSeparatedDataByColumn()
        {
            var li = pbl.SanPhams.Select(p => p.PhanLoai).Distinct().ToList();
            return li;
        }
        public int Insert(SanPham sp)
        {
                pbl.SanPhams.Add(sp);
                pbl.SaveChanges();
                return 1;
            
        }
        public int Delete(string id)
        {
                var sp = pbl.SanPhams.Where(p => p.IDSanPham == id).FirstOrDefault();
                pbl.SanPhams.Remove(sp);
                pbl.SaveChanges();
                return 1;
        }
        public int Update(SanPham sp)
        {
            if (sp != null)
            {
                SanPham p = pbl.SanPhams.Find(sp.IDSanPham);
                p.Ten = sp.Ten;
                p.GiaBan = sp.GiaBan;
                pbl.SaveChanges();
                return 1;
            }
            return 0;
        }

        public string GetLastID(string phanloai)
        {
            var li = pbl.SanPhams.Where(p => p.PhanLoai == phanloai)
                                 .OrderByDescending(p => p.IDSanPham)
                                 .FirstOrDefault();
            return li.IDSanPham;
        }
       public string GetIDByIDCTSP(string idct)
        {
            var li = pbl.ChiTietSanPhams.Find(idct);
            return li.SanPham.IDSanPham;
        }
       public bool CheckSanPhamCoTheXoa(string IDSP)
        {
            var li = pbl.ChiTietSanPhams.Where(p => p.IDSanPham.Contains(IDSP)).Select(p => p);
            if(li.Count()>0)
            {
                return false;
            }
            return true;
        }
    }
}
