using System.Text;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace User_Authentication_API
{

    public interface IAuthentication
    {
        bool ValidateUser(string username, string password);
        string GenerateJwtToken(string username);
    }

    public class AuthenticationService : IAuthentication
    {
        private static readonly List<UserModel> _users = new();

        public bool ValidateUser(string username,string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }
        public string GenerateJwtToken(string username)
        {
            var securityKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey12345"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };
            var token = new JwtSecurityToken(
                    issuer: "MyApp",
                    audience: "MyUsers",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
