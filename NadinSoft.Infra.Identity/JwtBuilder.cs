using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NadinSoft.Infra.Identity
{
    public class JwtBuilder
    {
        private UserManager<IdentityUser> _userManager;

        private AppJwtSettings _appJwtSettings;

        private IdentityUser _user;

        private ICollection<Claim> _userClaims;

        private ICollection<Claim> _jwtClaims;

        private ClaimsIdentity _identityClaims;

        public JwtBuilder WithUserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentException("userManager");
            return this;
        }

        public JwtBuilder WithJwtSettings(AppJwtSettings appJwtSettings)
        {
            _appJwtSettings = appJwtSettings ?? throw new ArgumentException("appJwtSettings");
            return this;
        }

        public JwtBuilder WithEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("email");
            }

            _user = _userManager.FindByEmailAsync(email).Result;
            _userClaims = new List<Claim>();
            _jwtClaims = new List<Claim>();
            _identityClaims = new ClaimsIdentity();
            return this;
        }

        public JwtBuilder WithJwtClaims()
        {
            _jwtClaims.Add(new Claim("sub", _user.Id.ToString()));
            _jwtClaims.Add(new Claim("email", _user.Email));
            _jwtClaims.Add(new Claim("jti", Guid.NewGuid().ToString()));
            _jwtClaims.Add(new Claim("nbf", ToUnixEpochDate(DateTime.UtcNow).ToString()));
            _jwtClaims.Add(new Claim("iat", ToUnixEpochDate(DateTime.UtcNow).ToString(), "http://www.w3.org/2001/XMLSchema#integer64"));
            _identityClaims.AddClaims(_jwtClaims);
            return this;
        }

        public JwtBuilder WithUserClaims()
        {
            _userClaims = _userManager.GetClaimsAsync(_user).Result;
            _identityClaims.AddClaims(_userClaims);
            return this;
        }

        public JwtBuilder WithUserRoles()
        {
            _userManager.GetRolesAsync(_user).Result.ToList().ForEach(delegate (string r)
            {
                _identityClaims.AddClaim(new Claim("role", r));
            });
            return this;
        }

        public string BuildToken()
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] bytes = Encoding.ASCII.GetBytes(_appJwtSettings.SecretKey);
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appJwtSettings.Issuer,
                Audience = _appJwtSettings.Audience,
                Subject = _identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appJwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256")
            });
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}
