using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreTools.Core.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Password incorrect"){ }
    }
}
