using POC.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.IProvider
{
    public interface IUserProvider
    {
        ResultObj<IEnumerable<CL_USERS>> GetUsers();
        ResultObj<CL_USERS> GetUserByUserName(string userName);
        ResultObj<CL_USERS> AuthenticateUser(string usr, string pwd);
        ResultObj<CL_USERS> CreateUser(CL_USERS user);
    }
}
