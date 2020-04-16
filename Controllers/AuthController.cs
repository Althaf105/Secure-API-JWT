using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api_demo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("token")]
        public IActionResult Token()
        {
            
            //Creating the jwt token for authentication

            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic".Length).Trim();
                var credDetails = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
                var infoDetails = credDetails.Split(":");

                //checking the basic authentication credentials passed from the Postman
                if (infoDetails[0] == "Admin" && infoDetails[1] == "pass")
                {

                    var claiminfo = new[] { new Claim(ClaimTypes.Name, infoDetails[0]) };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_my_secret_login_key"));
                    var algorithm = SecurityAlgorithms.HmacSha256;
                    var signinCred = new SigningCredentials(key, algorithm);

                    var token = new JwtSecurityToken(
                        issuer: "api_demo",
                        audience: "api_demo",
                        expires: DateTime.Now.AddMinutes(1),
                        claims: claiminfo,
                        signingCredentials: signinCred
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
            }
            return BadRequest("Wrong request");
        }
    }
}