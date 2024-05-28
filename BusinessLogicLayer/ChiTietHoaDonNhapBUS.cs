using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;
using DataAccessLayer;
namespace BusinessLogicLayer
{
    public class ChiTietHoaDonNhapBUS
    {
        ChiTietHoaDonNhapDAO dao = new ChiTietHoaDonNhapDAO();
        public int Insert(ChiTietHoaDonNhap c)
        {
            return dao.Insert(c);
        }
    }
}
