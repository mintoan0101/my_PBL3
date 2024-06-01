using DataAccessLayer;
using ValueObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Globalization;

namespace BusinessLogicLayer
{
    public class ChiTietSanPhamBUS
    {
        ChiTietSanPhamDAO dao = new ChiTietSanPhamDAO();

      
        public List<ChiTietSanPham_View> GetData1()
        {
            return dao.GetData1();
        }

        public int Insert(ChiTietSanPham ctsp)
        {
            return dao.Insert(ctsp);
        }
        public int Delete(string id)
        {
            return dao.Delete(id);
        }
        public int Update(ChiTietSanPham ctsp)
        {
            return dao.Update(ctsp);
        }
        public ChiTietSanPham GetCTSP(string IDChiTiet)
        {
            return dao.GetCTSP(IDChiTiet);
        }
        public int CountID(string f)
        {
            return dao.CountID(f);
        }
        public ChiTietSanPham GetChiTietSanPhamByDataRow(DataRow i)
        {
            return new ChiTietSanPham
            {
                IDChiTiet = i["IDChiTiet"].ToString(),
                IDSanPham = i["IDSanPham"].ToString(),
                IDNhaPhanPhoi = i["IDNhaPhanPhoi"].ToString(),
                HanSuDung = Convert.ToDateTime(i["HanSuDung"]),
            };
        }
        public List<ChiTietSanPham_View> Search(string phanloai, string ten)
        {
            return dao.Search(phanloai, ten);
        }
        public List<SoLuongCTSP> GetSoLuongs()
        {
            return dao.GetSoLuongs();
        }
         public string GetLastID(string f)
        {
            return dao.GetLastID(f);
        }
        public string GetIDNPP(int i)
        {
            return dao.GetIDNPP(i);
        }
        public int DeleteLastID(string IDSP)
        {
            return dao.DeleteLastID(IDSP);
        }
        public int UpdateThongSo(string IDSP, string NPP)
        {
            return dao.UpdateThongSo(IDSP, NPP);
        }
        public string GetLastIDByIDSP(string IDSP)
        {
            return dao.GetLastIDByIDSP(IDSP);
        }
        public bool CheckCTSPCoTheXoa(string ID)
        {
            return dao.CheckCTSPCoTheXoa(ID);
        }
        public int GetSoLuongByLastID(string IDSP)
        {
            return dao.GetSoLuongByLastID(IDSP);
        }
    }
}
