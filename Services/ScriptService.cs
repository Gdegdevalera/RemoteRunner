using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace RemoteRunner.Services
{
    public class ScriptService
    {
        private readonly Script[] _scripts;
        
        public ScriptService(IConfiguration configuration)
        {
            _scripts = configuration.GetSection("Scripts").Get<Script[]>();
        }
        
        public IEnumerable<string> Run(string token, string scriptName)
        {
            if (!ValidateToken(token))
            {
                yield break;
            }

            var script = _scripts.FirstOrDefault(x => x.Name == scriptName);

            if (script == null)
            {
                yield return "Script has not found!";
                yield break;
            }
            
            yield return script.Name;
            yield return script.Command;
        }

        private static bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
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

        private class Script
        {
            public string Name { get; set; }
            public string Command { get; set; }
        }
    }
}