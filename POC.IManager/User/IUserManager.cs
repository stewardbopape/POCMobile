using POC.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.IManager
{
    public interface IUserManager
    {
        IEnumerable<CL_USERS> GetUsers();
        CL_USERS GetByUserName(string userName);
        CL_USERS GetByID(int id);
        CL_USERS AddNew(CL_USERS user);
        CL_USERS Update(CL_USERS user);
 

    }
}
