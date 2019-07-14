using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RemoteRunner.Services
{
    public class LoginService
    {
        public string Login(string userName, string password)
        {
            if (userName != "test" && password != "test")
                return null;

            return CreateToken(userName);
        }
        
        private string CreateToken(string userName)
        {
            var credentials = new SigningCredentials(Constants.AuthKey, SecurityAlgorithms.HmacSha256);  
  
            var token = new JwtSecurityToken(Constants.AuthIssuer,  
                Constants.AuthAudience,  
                new [] { new Claim("sub", userName) },  
                expires: DateTime.Now.AddMinutes(120),  
                signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}