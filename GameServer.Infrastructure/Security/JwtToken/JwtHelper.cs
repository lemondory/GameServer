using GameServer.Contracts.Services;
using GameServer.Domain.Entities;
using GameServer.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameServer.Infrastructure.Security.JwtToken
{
    public class JwtHelper : ITokenHelper
    {
        private DateTime _tokenExpirationDate;
        private DateTime _refreshTokenExpirationDate;

        public JwtHelper()
        {
            _tokenExpirationDate = DateTime.Now.AddMinutes(TokenOptions.AccessTokenExpiration);
            _refreshTokenExpirationDate = DateTime.Now.AddMinutes(TokenOptions.RefreshTokenExpiration);
        }

        public AccessToken CreateToken(AccountEntity account)
        {
            var securityKey = CreateSecurityKey();
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityToken(account, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                RefreshToken = GenerateRefreshToken(),
                Expiration = _tokenExpirationDate,
                RefreshTokenExpiration = _refreshTokenExpirationDate
            };
        }

        public SecurityKey CreateSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.SecurityKey));
        }

        private JwtSecurityToken CreateJwtSecurityToken(AccountEntity account, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                expires: _tokenExpirationDate,
                notBefore: DateTime.Now,
                claims: SetClaims(account),
                signingCredentials: signingCredentials
            );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(AccountEntity account)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, account.AccountUid.ToString()),
                    new Claim(ClaimTypes.Name, (account.AccountName == null) ? "" : account.AccountName)
                };

            // 단일 역할 추가
            //if (account.AccountRole != null)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, account.AccountRole.RoleName));
            //}

            return claims;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }
}
