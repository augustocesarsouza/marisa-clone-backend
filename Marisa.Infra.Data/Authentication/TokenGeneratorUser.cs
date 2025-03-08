using Marisa.Domain.Authentication;
using Marisa.Domain.Entities;
using Marisa.Domain.InfoErrors;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Marisa.Infra.Data.Authentication
{
    public class TokenGeneratorUser : ITokenGeneratorUser
    {
        private readonly IConfiguration _configuration;

        public TokenGeneratorUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public InfoErrors<TokenOutValue> Generator(User user)
        {
            if (string.IsNullOrEmpty(user.Email))
                return InfoErrors.Fail(new TokenOutValue(), "Email or password null check");

            if (user == null)
                return InfoErrors.Fail(new TokenOutValue(), "user is null");

            if (user.Id == null)
                return InfoErrors.Fail(new TokenOutValue(), "Id is null");

            var userId = user.Id.ToString();

            if(userId == null)
                return InfoErrors.Fail(new TokenOutValue(), "userId is null");

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("userID", userId)
            };

            var keySecret = _configuration["Key:Jwt"];

            if (string.IsNullOrEmpty(keySecret) || keySecret.Length < 16)
                return InfoErrors.Fail(new TokenOutValue(), "error token related");

            var expires = DateTime.UtcNow.AddDays(1);
            //var expires = DateTime.UtcNow.AddSeconds(30);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: expires,
                claims: claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            var tokenValue = new TokenOutValue();
            var sucessfullyCreatedToken = tokenValue.ValidateToken(token, expires);

            if (sucessfullyCreatedToken)
            {
                return InfoErrors.Ok(tokenValue);
            }
            else
            {
                return InfoErrors.Fail(new TokenOutValue(), "error when creating token");
            }
        }
    }
}
