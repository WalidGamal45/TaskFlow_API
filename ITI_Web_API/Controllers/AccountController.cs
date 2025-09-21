using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITI_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public ActionResult Login(UserTokenDto user)
        {
            if (user.Username != "admin" && user.PassWord != "123")
            {
                return Unauthorized();
            }
            // generate token
            // create cliams 
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "admin"));
            claims.Add(new Claim(ClaimTypes.MobilePhone, "010"));

            //Secret Key
            string Key = "Hello Wella 123 , wkkejlkflfpkfkfokfokop#"; // المفروض تكون صعبه وتوضع فى AppSettings

            var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
            //Creat token 
            var signCre = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            // token object 

            var token = new JwtSecurityToken(

                claims: claims,
                signingCredentials: signCre,
                expires: DateTime.Now.AddDays(1)

                );
            //object ==> encoded string 
            string stringtoken = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(stringtoken);
            //{the Token}
            //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
            //eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9tb2JpbGVwaG9uZSI6IjAxMCIsImV4cCI6MTc1ODI4NTQ0NH0.
            //LENbLzHQs1r5GBHhAOLxCFn8IBqCxRdfc_D8peOOCNQ

        }
        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok();
        }
    }
}
