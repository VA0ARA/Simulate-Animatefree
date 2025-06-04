using APAF.Domain.Core.Contracts.AppServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AFAPIUnity.TokenService
{
    public class GernerateToken : IGenerateToken
    {
        private readonly IConfiguration configuration;
        public DateTime expireAt;

        public GernerateToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

/*        public List<Claim> CreateClaims()
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.MobilePhone, "admin@mywebsite.com"),
                    new Claim("Department", "HR"),
                     new Claim("Admin", "true"),
                    new Claim("Manager", "true"),
                    new Claim("EmploymentDate", "2023-01-01")
            };
            return claims;
        }*/
        public string CtrateToken(IEnumerable<Claim> claims, DateTime expireAt)

        {
            //haker in key nadare
            var secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey") ?? "");

            // generate the JWT
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expireAt,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public string CreateGusetToken( DateTime expireAt){
          var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "***********************************") // You can add other claims if needed
            };

            return CtrateToken(claims, expireAt);
        }
    }
}
