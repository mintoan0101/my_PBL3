using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;

namespace DataAccessLayer
{
    public class NhaPhanPhoiDAO
    {
        PBL3Entities1 db = new PBL3Entities1();

        public int Insert(NhaPhanPhoi p)
        {
            if (p != null)
            {
                db.NhaPhanPhois.Add(p);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int Delete(string id )
        {
            var p =db.NhaPhanPhois.Find(id);
            if (p != null)
            { 
                db.NhaPhanPhois.Remove(p);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int Update(NhaPhanPhoi p)
        {
            if (p != null)
            {
                var n = db.NhaPhanPhois.Find(p.IDNhaPhanPhoi);
                n.TenNhaPhanPhoi = p.TenNhaPhanPhoi;
                n.DiaChi = p.DiaChi;
                n.SoDienThoai = p.SoDienThoai;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public List<string> GetName()
        {
            var li = db.NhaPhanPhois.Select(p => p.TenNhaPhanPhoi);
            return li.ToList();
        }
        public string GetLastID()
        {
            var re = db.NhaPhanPhois.Select(p => p).OrderByDescending(p => p.IDNhaPhanPhoi).FirstOrDefault();
            return re.IDNhaPhanPhoi;
        }
        public bool CheckNPP(string id)
        {
            var l = db.ChiTietSanPhams.Where(p => p.IDNhaPhanPhoi.Contains(id)).Count();
            if(l >0)
            {
                return false;
            }
            return true;
        }
    }
}
