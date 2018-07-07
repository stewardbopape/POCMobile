using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POC.BusinessObjects;
using POC.IProvider;

namespace POC.WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IUserProvider _userProvider;

        public UserController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [Route("login/{usr}/{pwd}")]
        [HttpGet]
        public IHttpActionResult Login(string usr,string pwd)
        {

            var result = _userProvider.AuthenticateUser(usr, pwd);
            return Ok(result);
        }
        [HttpPost, Route("save")]
        public IHttpActionResult SaveUser(CL_USERS user)
        {           
            var result = this._userProvider.CreateUser(user);
            return Ok(result);
        }
    }
}
