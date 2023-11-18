using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Data.EF;
using WebApplication1.Function;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly SymmetricSecurityKey _secretKey;
        public AuthController(MyDbContext context)
        {
            _context = context;
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ntqbqhrychczumzisfmojgjtpvpsfgwm"));
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _context.Users.SingleOrDefault(p => p.userName == model.userName);
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.passWord, model.passWord);
                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    var userrole = _context.userRoles.SingleOrDefault(p => p.userId == user.Id);
                    var role = _context.Roles.SingleOrDefault(p=>p.roleId==userrole.roleId);
                    var claims = new List<Claim>
                {
                    new Claim("Username", user.userName),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("PhoneNumber", user.phoneNumber ),
                    new Claim("Role", role.roleName)
                };


                    var token = new JwtSecurityToken(
                        issuer: "https://localhost:7145/swagger",
                        audience: "api", // Hoặc "api/login" tùy theo cấu hình
                        claims: claims,
                        expires: DateTime.UtcNow.AddHours(1),
                        signingCredentials: new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { Token = tokenString, Message = "Login success" });
                }
                else
                {
                    return Ok(new { Message = "Login fail ! try again" });
                }
            }
            else
            {
                return Ok(new { Message = "Login fail ! try again" });
            }
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!_context.Users.Any(u => u.userName == model.userName))
            {
                var user = new User
                {
                    userName = model.userName,
                    passWord = new PasswordHasher<User>().HashPassword(null, model.passWord),
                    Email = model.Email,
                    phoneNumber = model.phoneNumber
                };
                SendMail.SendEmail(model.Email, "Xác nhận tài khoản", "Bạn đã xác nhận thành công", "");
                _context.Users.Add(user);
                _context.SaveChanges();
                var user1 = _context.Users.SingleOrDefault(u => u.userName == model.userName);
                var role = _context.Roles.SingleOrDefault(r => r.roleName == "Member");
                if (user != null && role != null)
                {
                    var userRole = new UserRole
                    {
                        userId = user1.Id,
                        roleId = role.roleId
                    };
                    _context.userRoles.Add(userRole);
                    _context.SaveChanges();
                }

                return Ok("Registration successful");
            }

            return BadRequest("Username is already taken");
        }
        [HttpGet("google-login")]
        public IActionResult GoogleLogin(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback", "Auth", new { ReturnUrl = returnUrl })
            };
            return Challenge(properties, "Google");
        }

        [HttpGet("facebook-login")]
        public IActionResult FacebookLogin(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties { RedirectUri = returnUrl };
            return Challenge(properties, "Facebook");
        }

    }
}

