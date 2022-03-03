using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserModel user = new UserModel();
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            }
        [HttpPost("register")]
        public ActionResult<UserModel> Register(User request)
        {
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            user.username = request.username;
            user.Passwordhash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return Ok(user);
        }
        [HttpPost("login")]
        public ActionResult<string> Login(User request)
        {
            if (user.username != request.username)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPassword(request.password, user.Passwordhash, user.PasswordSalt))
            {
                return BadRequest("Wrong password ,try again");
            }
            string token = CreateToken(user);
            return Ok("login Succesfully");
        }
        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void CreatePasswordHash(string passowrd, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passowrd));
            }
        }
        private bool VerifyPassword(string password, byte[] passwordhash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedhash.SequenceEqual(passwordhash);
            }
        }

    }
}

