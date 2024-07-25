using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using API.Model;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public readonly AppDBContext _context;

        public LoginController(IConfiguration configuration, AppDBContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken(User user)
        {
            if (user != null && user.UserName != null && user.PassWord != null)
            {
                var userData = await GetUserInfor(user.UserName, user.PassWord);
                var jwt = Configuration.GetSection("Jwt").Get<Jwt>();

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("UserName", user.UserName.ToString()),
                    new Claim("PassWord", user.PassWord.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleID.ToString()),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signIn
                    );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpGet]
        public async Task<User> GetUserInfor(string username, string password)
        {
            return await _context.user.FirstOrDefaultAsync(x => x.UserName == username && x.PassWord == password);
        }
    }
}
