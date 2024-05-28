﻿using ValueObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace DataAccessLayer
{
    public class NhanVienDAO
    {
        PBL3Entities1 pbl = new PBL3Entities1();
        public int Insert(NhanVien nv)
        {
            pbl.NhanVien.Add(nv);
            pbl.SaveChanges();
            return 1;
        }
        public int Delete(string id)
        {
            var nv = pbl.NhanVien.Where(p => p.IDNhanVien == id).FirstOrDefault();
            pbl.NhanVien.Remove(nv);
            pbl.SaveChanges();
            return 1;
        }
        public int Update(NhanVien nv)
        {
            if (nv != null)
            {

                NhanVien n = pbl.NhanVien.Find(nv.IDNhanVien);
                n.TenNhanVien = nv.TenNhanVien;
                n.DiaChi = nv.DiaChi;
                n.CCCD = nv.CCCD;
                n.Email = nv.Email;
                n.MucLuong = nv.MucLuong;
                n.Nam = nv.Nam;
                n.NgaySinh = nv.NgaySinh;
                n.SoDienThoai = nv.SoDienThoai;
                pbl.SaveChanges();
                var nhanVien = pbl.NhanVien.Where(p => p.IDNhanVien == nv.IDNhanVien).FirstOrDefault();
                if (nhanVien != null)
                {
                    nhanVien.TenNhanVien = nv.TenNhanVien;
                    nhanVien.DiaChi = nv.DiaChi;
                    nhanVien.CCCD = nv.CCCD;
                    nhanVien.Email = nv.Email;
                    nhanVien.MucLuong = nv.MucLuong;
                    nhanVien.Nam = nv.Nam;
                    nhanVien.NgaySinh = nv.NgaySinh;
                    nhanVien.SoDienThoai = nv.SoDienThoai;
                    pbl.SaveChanges();
                    return 1;
                }

            }
            return 0;
        }
        public string GetLastId()
        {
            var re = pbl.NhanVien.OrderByDescending(p => p.IDNhanVien).FirstOrDefault();
            return re.IDNhanVien;
        }
        public NhanVien GetNVByID(string ID)
        {
            var n = pbl.NhanVien.Find(ID);
            return n;
        }
        public string GetIDNVByUsername(string un)
        {
            var re = pbl.NhanVien.Where(p => p.TaiKhoan.TenTaiKhoan.Contains(un)).FirstOrDefault();
            return re.IDNhanVien;
        }
        public int UpdateByNhanVien(NhanVien nv)
        {
            NhanVien n = pbl.NhanVien.Find(nv.IDNhanVien);
            n.TenNhanVien = nv.TenNhanVien;
            n.Email = nv.Email;
            n.SoDienThoai = nv.SoDienThoai;
            n.DiaChi = nv.DiaChi;
            n.CCCD = nv.CCCD;
            n.NgaySinh = nv.NgaySinh;
            n.Nam = nv.Nam;
            pbl.SaveChanges();
            return 1;
        }
        public string GetIDTaiKhoanByNhanVien(string idnv)
        {
            var re = pbl.NhanVien.Find(idnv).TaiKhoan;
            return re.IDTaiKhoan;
        }
        public List<string> GetNameNhanVien()
        {
            var re = pbl.NhanVien.Select(p => p.TenNhanVien);
            return re.ToList();
        }
    }
}