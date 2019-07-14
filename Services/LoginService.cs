using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace RemoteRunner.Services
{
    public class LoginService
    {
        private readonly User[] _users;
        public LoginService(IConfiguration configuration)
        {
            _users = configuration.GetSection("Users").Get<User[]>();
        }
        
        public string Login(string userName, string password)
        {
            var user = _users.FirstOrDefault(x => x.Login == userName && x.Password == password);
           
            return user != null 
                ? CreateToken(user) 
                : null;
        }
        
        private static string CreateToken(User user)
        {
            var credentials = new SigningCredentials(Constants.AuthKey, SecurityAlgorithms.HmacSha256);  
  
            var token = new JwtSecurityToken(Constants.AuthIssuer,  
                Constants.AuthAudience,  
                new [] { new Claim("sub", user.Login) },  
                expires: DateTime.Now.AddMinutes(120),  
                signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private class User
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
    }
}