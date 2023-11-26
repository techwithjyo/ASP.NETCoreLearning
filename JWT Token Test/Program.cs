// See https://aka.ms/new-console-template for more information

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

Console.WriteLine("Hello, World!");

string token = GenerateJSONWebToken();

Console.WriteLine(token);

string GenerateJSONWebToken()
{
    //header info
    var algo = SecurityAlgorithms.HmacSha256;
    
    //payload info
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, "Jyotimoy"),
        new Claim(JwtRegisteredClaimNames.Email, "test@gmail.com"),
        new Claim("Location", "India"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
    
    //signature
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkeysecretkeysecretkeysecretkeysecretkey"));
    var credentials = new SigningCredentials(securityKey, algo);
    var token = new JwtSecurityToken("IssuerIsMe", "AudienceIsMe", claims,
        expires: DateTime.Now.AddMinutes(120),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
