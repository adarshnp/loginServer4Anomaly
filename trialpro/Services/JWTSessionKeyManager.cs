using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace trialpro.Services
{
    public class JWTSessionKeyManager : ISessionKeyManager
    {
        private readonly SigningCredentials credentials;
        private readonly string issuer;
        private readonly int secondsBeforeExpire;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        private readonly HashSet<string> refreshTokens = new HashSet<string>();


        public JWTSessionKeyManager(JwtConfig config)
        {

            issuer = config.Issuer;
            secondsBeforeExpire = config.Duration;
            var key = Encoding.UTF8.GetBytes(config.Key);

            var securityKey = new SymmetricSecurityKey(key);

            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string GenerateNewSessionKey(string userID)
        {
            refreshTokens.Add(userID);

            string key = GenerateJSONWebToken(userID);
            return key;
        }

        public void ReleaseSessionKey(string userID)
        {
            if (refreshTokens.Contains(userID))
            {
                refreshTokens.Remove(userID);
            }
        }

        public string RefreshSessionKey(string userID)
        {
            if (refreshTokens.Contains(userID))
            {
                return GenerateJSONWebToken(userID);
            }
            return "Session key was not found";
        }


        private string GenerateJSONWebToken(string userInfo)
        {

            var token = new JwtSecurityToken(
              issuer: issuer,
              claims: new[] { new Claim(JwtExtensionsAndConstants.USERID_STRING, userInfo) },
              expires: DateTime.Now.AddSeconds(secondsBeforeExpire),
              signingCredentials: credentials);

            return jwtSecurityTokenHandler.WriteToken(token);
        }

    }
}
