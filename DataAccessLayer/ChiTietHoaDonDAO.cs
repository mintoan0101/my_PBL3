﻿using System;
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
           
                pbl.ChiTietHoaDon.Add(cthd);
                pbl.SaveChanges();
                return 1;
          
        }
        public List<dynamic> GetData(string ID)
        {
            var li = pbl.ChiTietHoaDon.Where(p => p.IDHoaDon == ID)
                                       .Select(p => new { p.ChiTietSanPham.SanPham.Ten, p.SoLuong, p.ChiTietSanPham.SanPham.GiaBan });
            return li.ToList<dynamic>();
        }

    }
}