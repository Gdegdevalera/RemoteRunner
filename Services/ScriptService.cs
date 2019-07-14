using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace RemoteRunner.Services
{
    public class ScriptService
    {
        public IEnumerable<string> Run(string token, string scriptName)
        {
            ValidateToken(token);
            
            int i = 0;
            yield return scriptName;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
            yield return "line" + i++;
        }

        private static void ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateLifetime = true, 
                ValidateAudience = true, 
                ValidateIssuer = true, 
                ValidIssuer = Constants.AuthIssuer,
                ValidAudience = Constants.AuthAudience,
                IssuerSigningKey = Constants.AuthKey
            };
        }
    }
}