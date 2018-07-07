using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC.BusinessObjects;
using POC.IManager;
using POC.IProvider;

namespace POC.Provider
{
    public class UserProvider : IUserProvider
    {

        private IUnitOfWork _unitOfWork;

        public UserProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public ResultObj<CL_USERS> AuthenticateUser(string usr, string pwd)
        {
            ResultObj<CL_USERS> result = new ResultObj<CL_USERS>() { isSuccessful = false};
            var user = this._unitOfWork.UserManager.GetByUserName(usr);

            if (user != null)
                result.isSuccessful = true;
          


            result.Data = user;
           
            return result;

        }

        public ResultObj<CL_USERS> CreateUser(CL_USERS user)
        {
            ResultObj<CL_USERS> result = new ResultObj<CL_USERS>() { isSuccessful = true };

            var existingUser = this._unitOfWork.UserManager.GetByUserName(user.USERNAME);

            if (existingUser != null)
            {
                result.isSuccessful = false;
            }
            
            var added = this._unitOfWork.UserManager.AddNew(user);

            this._unitOfWork.SaveChanges();


            result.Data = added;
            return result;


        }

        public ResultObj<CL_USERS> GetUserByUserName(string userName)
        {
            ResultObj<CL_USERS> result = new ResultObj<CL_USERS>() { isSuccessful = false };


            var user = this._unitOfWork.UserManager.GetByUserName(userName);


            if (user != null)
                result.isSuccessful = true;



            result.Data = user;

            return result;
        }

            public ResultObj<IEnumerable<CL_USERS>> GetUsers()
        {
            ResultObj< IEnumerable< CL_USERS >> result = new ResultObj<IEnumerable<CL_USERS>>() { isSuccessful = false };

            var users = this._unitOfWork.UserManager.GetUsers();

            if (users != null)
                result.isSuccessful = true;



            result.Data = users;

            return result;

        }
    }
}
