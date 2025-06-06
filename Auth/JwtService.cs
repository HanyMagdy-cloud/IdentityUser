using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserIdentity.EF.Auth
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IdentityUser user, IList<string> roles)
        {
            // Create a list of claims (info about the user stored in the token)
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),         // "sub" = Subject = username
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // "jti" = unique token ID
    };

            // Add a claim for each role the user has (e.g., Admin, Customer)
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Get the JWT secret key from appsettings.json and convert it to bytes
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Create signing credentials using the secret key and HMAC-SHA256 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the actual JWT token with claims and expiration
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],       // who issued the token
                audience: _configuration["Jwt:Audience"],   // who the token is for
                claims: claims,                              // include the user’s claims
                expires: DateTime.Now.AddMinutes(30),        // token is valid for 30 minutes
                signingCredentials: creds                    // sign the token with the key
            );

            // Convert the token to a string to return to the client
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}