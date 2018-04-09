using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventStoreTools.Core.JWT
{
    public class AuthOptions
    {
        public const int USERROLE = 1;
        public const int ADMINROLE = 0;
        public const string ISSUER = "EventStore-Tools";
        public const string AUDIENCE = "https://localhost:44382/";
        const string KEY = "EventStore is better";   // encrypt key
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
