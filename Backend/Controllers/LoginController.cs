using System.Linq;
using Microsoft.AspNetCore.Mvc;
using tasinmaz_v3;
using tasinmaz_V3.Dtos;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace tasinmaz_V3.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : Controller
    {
        private TasinmazDbContext _db;
        private IConfiguration _conf;
        public LoginController(TasinmazDbContext db ,
        IConfiguration conf)
        {
            _conf = conf;
            _db = db;
        }

        [HttpPost]
        [Route("{loginUser}")]
        public IActionResult Login(UserForLoginDto loginUser)
        {
            var logAttemptPasswordHashed = encryptSha256(loginUser.password);
            var user = _db.kullanicilar.Where(x => x.email == loginUser.userName)
            .FirstOrDefault(x => x.passwordHash == logAttemptPasswordHashed.ToString());
            if (user == null)
            {
                return BadRequest(null);
            }
            string roleName;
            if(!user.role)
            {  
                roleName = "Admin";
            }else{
                roleName = "Kullanici";
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_conf.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier , user.kullaniciID.ToString()),
                    new Claim(ClaimTypes.Name , user.email),
                    new Claim(ClaimTypes.Role , roleName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , 
                SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }

        private object encryptSha256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}