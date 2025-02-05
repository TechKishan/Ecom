using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static Ecom.Common;

namespace Ecom.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Users/AddUserInfo")]
        public Task<MessageFor> AddUserProfile(UserProfile userProfile)
        {
            return Models.Users.AddUserInfo(userProfile);
        }
       
        [HttpGet]
        [Route("api/Users/GetUserInfo")]
        public DataTable GetUserInfo()
        {
            return Models.Users.GetUserInfo();
        }

        [HttpPost]
        [Route("api/Users/GetSingleDataUser")]
        public DataSet GetSingleDataUser(UserProfile data)
        {
            return Models.Users.GetSingleDataUser(data);
        }

        [HttpPost]
        [Route("api/Users/LogIn")]
        public LoginMessageFor Login(UserProfile data)
        {
            return Models.Users.Login(data);
        }
    }
}
