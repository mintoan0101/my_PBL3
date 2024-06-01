using DataAccessLayer;
using ValueObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class NhanVienBUS
    {
        NhanVienDAO dao = new NhanVienDAO();
        public int Insert(NhanVien nv)
        {
            return dao.Insert(nv);
        }
        public int Delete(string id)
        {
            return dao.Delete(id);
        }
        public int Update(NhanVien nv)
        {
            return dao.Update(nv);
        }
        public string GetLastID()
        {
            return dao.GetLastId();
        }
        public NhanVien GetNVByID(string ID)
        {
            return dao.GetNVByID(ID);
        }
        public string GetIDNVByUsername(string username)
        {
            return dao.GetIDNVByUsername(username);
        }
        public int UpdateByNhanVien(NhanVien nv)
        {
            return dao.UpdateByNhanVien(nv);
        }
        public string GetIDTaiKhoanByNhanVien(string idnv)
        {
            return dao.GetIDTaiKhoanByNhanVien(idnv);
        }
        public List<string> GetNameNhanVien()
        {
            return dao.GetNameNhanVien();
        }
        public string GetID(string tentaikhoan)
        {
            return dao.GetID(tentaikhoan);
        }
        public bool CheckEnableToDelete(string IDNV)
        {
            return dao.CheckEnableToDelete(IDNV);
        }
    }
}
