//using Mysqlx.Crud;
using ValueObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ChiTietSanPhamDAO
    {
        PBL3Entities1 pbl = new PBL3Entities1();
        public List<ChiTietSanPham_View> GetData1()
        {
            var li = pbl.ChiTietSanPhams.Select(p => new ChiTietSanPham_View
            {
                IDChiTiet = p.IDChiTiet,
                PhanLoai = p.SanPham.PhanLoai,
                Ten = p.SanPham.Ten,
                HanSuDung = p.HanSuDung,
                GiaBan = p.SanPham.GiaBan,
                SoLuong = p.SoLuong,
                Check = false
            }).ToList();
            return li;
        }

        public int Insert(ChiTietSanPham ctsp)
        {
            if (ctsp != null)
            {
                pbl.ChiTietSanPhams.Add(ctsp);
                pbl.SaveChanges();
                return 1;
            }
            return 0;
        }
        public List<ChiTietSanPham_View> Search(string PhanLoai, string txt)
        {
            var list = GetData1();

            if (!string.IsNullOrEmpty(txt))
            {
                switch (PhanLoai)
                {
                    case "ID Chi Tiết":
                        list = list.Where(p => p.IDChiTiet.Contains(txt)).ToList();
                        break;
                    case "Tên Sản Phẩm":
                        list = list.Where(p => p.Ten.Contains(txt)).ToList();
                        break;
                    case "Phân Loại":
                        list = list.Where(p => p.PhanLoai.Contains(txt)).ToList();
                        break;
                    default:
                        break;
                }
            }

            return list;
        }
        public int Delete(string id)
        {
            var ct = pbl.ChiTietSanPhams.Where(p => p.IDChiTiet.Contains( id)).FirstOrDefault();
            pbl.ChiTietSanPhams.Remove(ct);
            pbl.SaveChanges();
            return 1;
        }
        public int Update(ChiTietSanPham ctsp)
        {
            var sp = pbl.ChiTietSanPhams.Where(p => p.IDChiTiet == ctsp.IDChiTiet).FirstOrDefault();
            if (sp != null)
            {
                sp.IDNhaPhanPhoi = ctsp.IDNhaPhanPhoi;
                sp.IDSanPham = ctsp.IDSanPham;
                sp.HanSuDung = ctsp.HanSuDung;
                sp.SoLuong = ctsp.SoLuong;
                pbl.SaveChanges();
                return 1;
            }
            return 0;
        }
        public ChiTietSanPham GetCTSP(string IDChiTiet)
        {
            ChiTietSanPham ctsp = pbl.ChiTietSanPhams.Where(p => p.IDChiTiet == IDChiTiet).FirstOrDefault();
            return ctsp;
        }
        public string GetLastID(string f)
        {
            var c = pbl.ChiTietSanPhams.Where(p => p.IDChiTiet.Contains(f)).OrderByDescending(p => p.IDChiTiet).FirstOrDefault();
            return c.IDChiTiet;
        }

        public string GetIDNPP(int index)
        {
            var n = pbl.NhaPhanPhois.Where(p => p.IDNhaPhanPhoi.Contains(index + "")).OrderBy(p => p.IDNhaPhanPhoi).FirstOrDefault();
            return n.IDNhaPhanPhoi;
        }
        public int CountID(string f)
        {
            var count = pbl.ChiTietSanPhams.Count(p => p.IDChiTiet.Contains("CT" + f));
            return count;
        }
        public List<SoLuongCTSP> GetSoLuongs()
        {
            var li = pbl.ChiTietSanPhams.Select(p => new SoLuongCTSP
            {
                IDChiTiet = p.IDChiTiet,
                SoLuong = p.SoLuong,
            });
            return li.ToList();
        }
       public int DeleteLastID(string IDSP)
        {
            var re = pbl.ChiTietSanPhams.Select(p => new
            {
                IDCT = p.IDChiTiet,
                IDSP = p.IDSanPham,
            }).Where(p => p.IDSP.Contains(IDSP)).OrderByDescending(p => p.IDCT).FirstOrDefault();
            if(Delete(re.IDCT)>0)
            {
                return 1;
            }
            return 0;
        }
        public int UpdateThongSo(string IDSP,string NPP)
        {
            var re = pbl.ChiTietSanPhams.Where(p => p.IDSanPham.Contains(IDSP)).OrderByDescending(p => p.IDChiTiet).FirstOrDefault();
            re.NhaPhanPhoi.TenNhaPhanPhoi = NPP;
            pbl.SaveChanges();
            return 1;
        }
        public string GetLastIDByIDSP(string IDSP)
        {
            var li = pbl.ChiTietSanPhams.Where(p => p.IDSanPham.Contains(IDSP)).OrderByDescending(p => p.IDChiTiet).FirstOrDefault();
            return li.IDChiTiet;
        }
        public bool CheckCTSPCoTheXoa(string ID)
        {
            var li = pbl.ChiTietHoaDonNhaps.Where(p => p.IDChiTiet.Contains(ID) ).Select(p => p);
            var li1 = pbl.ChiTietHoaDons.Where(p => p.IDChiTiet.Contains(ID)).Select(p => p);
            if(li.Count()>0 || li1.Count()>0)
            {
                return false;
            }
            return true;
        }
        public int GetSoLuongByLastID(string IDSP)
        {
            var re = pbl.ChiTietSanPhams.Where(p => p.IDSanPham == IDSP).OrderByDescending(p => p.IDChiTiet).FirstOrDefault();
            return re.SoLuong;
        }
    }
}