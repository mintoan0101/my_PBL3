using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;
namespace BusinessLogicLayer
{
    public class HoaDonNhapBUS
    {
        HoaDonNhapDAO dao = new HoaDonNhapDAO();
        public int Insert(HoaDonNhap n )
        {
            return dao.Insert( n );
        }
        public int Update(HoaDonNhap n )
        {
            return dao.Update( n );
        }
        public int Delete(string id ) {
            return dao.Delete( id );
        }
        public string GetLastID()
        {
            return dao.GetLastId();
        }
        public int UpdateStatus(string ID)
        {
            return dao.UpdateStatus( ID );
        }
    }
}
