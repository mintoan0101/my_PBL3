using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using ValueObject;
using System.Data.Entity;
namespace DataAccessLayer
{
    public class TaiKhoanDAO
    {
        PBL3Entities1 db = new PBL3Entities1();
        public bool CheckAccount(string username, string password)
        {
            var anccount = db.TaiKhoans.Where(p => p.TenTaiKhoan == username).FirstOrDefault();
            if (anccount != null) //có tài khoản trùng tên
            {
                if (anccount.MatKhau == password)//trùng cả mật khẩu
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public bool CheckAdmin(string username)
        {
            var anccount = db.TaiKhoans.Where(p => p.TenTaiKhoan == username).FirstOrDefault();
            if (anccount.isAdmin == 1)
            {
                return true;
            }
            return false;
        }
        public string GetLastID()
        {
            var anccount = db.TaiKhoans.OrderByDescending(p => p.IDTaiKhoan).FirstOrDefault();
            if (anccount != null)
            {
                return anccount.IDTaiKhoan;
            }
            return null;
        }
        public int Insert(TaiKhoan tk)
        {
            db.TaiKhoans.Add(tk);
            db.SaveChanges();
            return 1;
        }
        public int Delete(string id)
        {
            var nv = db.TaiKhoans.Where(p => p.IDTaiKhoan == id).FirstOrDefault();
            db.TaiKhoans.Remove(nv);
            db.SaveChanges();
            return 1;
        }
        public int Update(TaiKhoan tk)
        {
            if (tk != null)
            {
                TaiKhoan t = db.TaiKhoans.Find(tk.IDTaiKhoan);
                t.TenTaiKhoan = tk.TenTaiKhoan;
                t.MatKhau = tk.MatKhau;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public bool CheckPassByIDNV(string idnv, string matkhaucu)
        {
            bool exists = db.NhanViens.Any(p => p.IDNhanVien == idnv && p.TaiKhoan.MatKhau == matkhaucu);

            if (exists)
            {
                return true;
            }
            return false;
        }
        public int UpdatePasswordByIDNV(string idnv, string matkhaumoi)
        {
            if (idnv != "" && matkhaumoi != "")
            {
                NhanVienDAO nvdao = new NhanVienDAO();
                string idtk = nvdao.GetIDTaiKhoanByNhanVien(idnv);
                TaiKhoan tk = db.TaiKhoans.Find(idtk);
                tk.MatKhau = matkhaumoi;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
    }
}
