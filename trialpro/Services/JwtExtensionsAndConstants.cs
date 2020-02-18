using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace trialpro.Services
{
    public static class JwtExtensionsAndConstants
    {
        public const string JwtAuthenticationScheme = "JwtAuthScheme";
        public const string USERID_STRING = "userID";
#nullable enable
        public static string? GetUserIDFromJWTHeader(this HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                return identity.FindFirst(USERID_STRING)?.Value;
            }
            return null;
        }
        public static string? GetClaimValueFromHeader(this HttpContext httpContext, string claim)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                return identity.FindFirst(claim)?.Value;
            }
            return null;
        }

        /// <summary>
        /// Helper that adds jwt authentication to the AuthenticationBuilder with options validating issuer key and save token set to true
        /// </summary>
        /// <param name="builder">The application builder</param>
        /// <param name="key">The unhashed key used in signing the token</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtAuthenticationWithKey(this AuthenticationBuilder builder, string key)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return builder.AddJwtBearer(JwtAuthenticationScheme, x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Validate();
            });
        }
        public static AuthenticationBuilder AddJwtAuthenticationWithKeyAndIssuer(this AuthenticationBuilder builder, string key, string issuer)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return builder.AddJwtBearer(JwtAuthenticationScheme, x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidIssuer = issuer,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = true
                };
                x.Validate();
            });
        }
    }
}
