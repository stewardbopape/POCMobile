using POC.BusinessObjects;
using POC.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Manager
{
    public class UserManager : IUserManager
    {
        POCDBEntities _context;
        
        public UserManager(POCDBEntities context)
        {
            this._context = context;

        }
        public CL_USERS AddNew(CL_USERS user)
        {
            var _user = GetByUserName(user.USERNAME);
            if (_user != null)
            {
                return _user;
            }
            else
            {
                return this._context.CL_USERS.Add(user);
            }

        }

        public CL_USERS GetByID(int id)
        {
            return this._context.CL_USERS.Where(o => o.USER_ID == id).FirstOrDefault();
        }

        public CL_USERS GetByUserName(string userName)
        {
            return this._context.CL_USERS.Where(o => o.USERNAME.ToLower() == userName).FirstOrDefault();
        }

        public IEnumerable<CL_USERS> GetUsers()
        {
            return this._context.CL_USERS.ToList();
        }

        public CL_USERS Update(CL_USERS user)
        {
            var o = GetByID(user.USER_ID);
            if(user!=null)
            {
                o.PASSWORD = user.PASSWORD;
            }
            else
            {
                throw new Exception("User :" + user.USERNAME + " does not exist");
            }
            return o;
        }
    }
}
