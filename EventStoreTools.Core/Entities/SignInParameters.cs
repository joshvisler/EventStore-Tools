using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreTools.Core.Entities
{
    public class AuthParameters
    {
        public AuthParameters(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }
    }
}
