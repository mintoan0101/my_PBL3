//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ValueObject
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietHoaDonNhap
    {
        public string IDHoaDonNhap { get; set; }
        public string IDChiTiet { get; set; }
        public int SoLuong { get; set; }
        public ChiTietHoaDonNhap(string id, string idct, int sl )
        {
            this.IDHoaDonNhap = id;
            this.IDChiTiet = idct;
            this.SoLuong = sl;
        }
        public virtual ChiTietSanPham ChiTietSanPham { get; set; }
        public virtual HoaDonNhap HoaDonNhap { get; set; }
    }
}
