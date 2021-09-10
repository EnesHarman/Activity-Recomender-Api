using Core.Extensions;
using Core.Utilities.Configuration;
using Core.Utilities.Security.Encyption;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        public TokenOptions _tokenOptions { get; set; }
        private DateTime _accessTokenExpiration;

        public JwtHelper()
        {
            Configuration = ConfigurationResolver.GetConfiguration();
            _tokenOptions = Configuration.GetSection("TokenOption").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpitation);
        }

        public AccessToken CreateToken(User user, Role role)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateSecurityToken(_tokenOptions, user, signingCredentials, role);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, Role role)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience : tokenOptions.Audience,
                expires : _accessTokenExpiration,
                notBefore : DateTime.Now,
                claims : SetClaims(user, role),
                signingCredentials : signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, Role role)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRole(role.RoleName);
            return claims;
        }
    }
}
