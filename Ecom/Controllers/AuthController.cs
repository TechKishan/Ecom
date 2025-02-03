using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecom.Controllers
{
    public class AuthController : ApiController
    {
        // POST api/auth/login
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] LoginModel model)
        {
            // Validate user credentials (this is just an example)
            if (model == null || model.Email != "kishangour7421@gmail.com" || model.Password != "Kishan@123")
            {
                return Unauthorized();
            }

            // Generate the JWT token
            var token = TokenManager.GenerateToken(model.Email);

            return Ok(new { token });
        }
    }
}
public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
