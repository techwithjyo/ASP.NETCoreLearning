using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using System.Threading;
using WebApplication16;
using System.Security.Cryptography;
using WebApplication16.Models;

namespace MVCCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IConfiguration _config;
        private  string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public SecurityController(IConfiguration config)
        {
            _config = config;
            
        }
        // GET: api/Security
        [HttpGet]
        public IActionResult Get(MyToken TokenVale)
        {

            var token = RefreshToken(TokenVale);
            return Ok(token);
            
        }
      
        private string GenerateJSONWebToken(string username, int expiryTime)
        {
            // header info
            var algo = SecurityAlgorithms.HmacSha256;
            
            // payload info
            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, ""),
                new Claim("IsAdmin", "True"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            // signature
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("victoriasecret@123456"));
            var credentials = new SigningCredentials(securityKey, algo);
            var token = new JwtSecurityToken("Questpond",
               "BrowserClients",
              claims,
              expires: DateTime.Now.AddSeconds(expiryTime),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // GET: api/Security/5


        // POST: api/Security
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            var user = Startup.Users
                        .Find(x => x.UserName == value.UserName);
            if(user is null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "No proper credentials");

            }
            if (user.Password==value.Password) // Check DB
            {
                MyToken tk = new MyToken();
              
                tk.TokenValue = GenerateJSONWebToken(user.UserName,30);
                tk.RefreshToken = GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                user.RefreshToken = tk.RefreshToken;
                return Ok(tk);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized , "No proper credentials");
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "Questpond",
                ValidAudience = "BrowserClients",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("victoriasecret@123456"))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, 
                                        tokenValidationParameters, 
                                        out securityToken);
            var user = Startup.Users
                       .Find(x => x.UserName == principal.Identity.Name);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null 
                || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        public MyToken RefreshToken(MyToken tokenApiModel)
        {
            if (tokenApiModel is null)
                throw new Exception("Invalid client request");
            string accessToken = tokenApiModel.TokenValue;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            
            var user = Startup.Users.SingleOrDefault(u => u.UserName == username);
            if (user is null || user.RefreshToken != refreshToken)
                throw new Exception("Invalid client request");
            
            var newAccessToken = GenerateJSONWebToken(user.UserName,60);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            tokenApiModel.RefreshToken = newRefreshToken;
            tokenApiModel.TokenValue = newAccessToken;

            return tokenApiModel;
        }
        // PUT: api/Security/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    
    
}
