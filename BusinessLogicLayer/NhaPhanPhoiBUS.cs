using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;

namespace BusinessLogicLayer
{
    public class NhaPhanPhoiBUS
    {
        NhaPhanPhoiDAO dao = new NhaPhanPhoiDAO();
        public int Insert(NhaPhanPhoi p)
        {
            return dao.Insert(p);
        }
        public int Delete(string id)
        {
            return dao.Delete(id);
        }
        public int Update(NhaPhanPhoi p)
        {
            return dao.Update(p);
        }
        public List<string> GetName()
        {
            return dao.GetName();
        }
        public string GetLastID()
        {
            return dao.GetLastID();
        }
        public bool ChekNPP(string id)
        {
            return dao.CheckNPP(id);
        }
    }
}
