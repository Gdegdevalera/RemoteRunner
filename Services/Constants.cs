using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RemoteRunner.Services
{
    public static class Constants
    {
        public const string AuthIssuer = "GGV_RemoteRunner";
        public const string AuthAudience = AuthIssuer;

        public static SymmetricSecurityKey AuthKey { get; } =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
    }
}