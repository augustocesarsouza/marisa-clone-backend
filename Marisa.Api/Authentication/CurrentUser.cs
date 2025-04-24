using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Marisa.Domain.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Marisa.Api.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        public string? Email { get; private set; }
        public bool IsValid { get; private set; }

        private readonly IConfiguration _configuration;

        public CurrentUser(IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeFromHttpContext(httpContext);
            
        }

        private void InitializeFromHttpContext(IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext == null)
                return;

            if (httpContext == null) return;

            var claims = httpContext.HttpContext.User.Claims;

            var keySecret = Environment.GetEnvironmentVariable("KEY_JWT") ?? _configuration["Key:Jwt"];

            if(keySecret == null)
            {
                IsValid = false;
                return;
            }

            var authHeader = httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var isValidSignature = ValidateTokenSignature(token, keySecret);

                if (!isValidSignature)
                {
                    IsValid = false;
                    return;
                }
            }

            string? headerValue = httpContext.HttpContext.Request.Headers["uid"];
            var claimEmailValue = claims.Any(x => x.Type == "Email");

            if (claims != null && claimEmailValue)
            {
                try
                {
                    var guidId = claims.First(x => x.Type == "userID").Value; // se não mandar valor ele lança exception 
                    Email = claims.First(x => x.Type == "Email").Value;

                    if (headerValue == null)
                    {
                        IsValid = false;
                        return;
                    }

                    if (!headerValue.Equals(guidId))
                        IsValid = false;
                    else
                    {
                        var time = long.Parse(claims.First(x => x.Type == "exp").Value);
                        DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
                        IsValid = dt.DateTime > DateTime.UtcNow;
                    }
                }
                catch (Exception ex)
                {
                    IsValid = false;
                }
            }

            //if (claims != null && claims.Any(x => x.Type == "Password"))
            //{
            //    Password = claims.First(x => x.Type == "Password").Value;
            //}
        }

        private bool ValidateTokenSignature(string token, string keySecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(keySecret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                // Se o token for inválido ou a assinatura estiver errada, lança exceção
                tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void SetEmail(string? email)
        {
            Email = email;
        }

        public void SetIsValid(bool isValid)
        {
            IsValid = isValid;
        }
    }
}
