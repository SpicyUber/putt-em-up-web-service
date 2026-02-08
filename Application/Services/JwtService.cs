using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JwtService
    {
        private readonly UserManager<Domain.Player> userManager;
        private readonly string secret;
        public JwtService(UserManager<Domain.Player> userManager, IConfiguration config) { this.userManager = userManager;  this.secret = config["tokenSecret"]; if (string.IsNullOrEmpty(secret)) throw new Exception("Please set up a tokenSecret in Config/appConfig.json"); }
        public async Task<string> GenerateToken(Domain.Player user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.NormalizedUserName), new Claim(ClaimTypes.Role, (await userManager.GetRolesAsync(user))[0])

            };

             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)); 
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "PuttEmUpServer",
                audience: "PuttEmUpClient",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
