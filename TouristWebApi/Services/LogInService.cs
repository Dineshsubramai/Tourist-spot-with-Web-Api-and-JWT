using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TouristWebApi.Dbcontext;
using TouristWebApi.Services;
using TouristWebApi.Tourist_DTO;
namespace TourWebAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly Tourist_DbContext touristDbContext;

        private readonly IConfiguration config;
        public LoginService(Tourist_DbContext _touristDbContext, IConfiguration _config)
        {
            touristDbContext = _touristDbContext;
            config = _config;
        }


         public string PerformLogin(string username, string password)
        { 
            var hashed_pass = ConvertToHash(password);

            var count = touristDbContext.Users.Where(x => x.user == username && x.password
            == hashed_pass).Count();

            

            if (count > 0)
            {
                var jwt_token = GenerateJWt_token(username);
                return jwt_token;
            }
            else
                return "";
           
        }
        private string GenerateJWt_token(string usr_name)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"]!;
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = double.Parse(jwtSettings["ExpiryInMinutes"] ?? "60");
           
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new Dictionary<string, object>
     {
         { ClaimTypes.Name, usr_name },
         { ClaimTypes.Role, "Admin" }, // Hook up your roles here
         { "Company", "Kaar infotech" }  // Custom user parameters
     };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Claims = claims,
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                SigningCredentials = credentials
            };
            var handler = new JsonWebTokenHandler();
            return handler.CreateToken(tokenDescriptor);

        }


        private string ConvertToHash(string rawpass)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(rawpass);
            byte[] hashBytes = SHA256.HashData(inputBytes);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

    }
}

