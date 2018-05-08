using System;

namespace EventStoreTools.Core.Exceptions
{
    public class UserExistException:Exception
    {
        public UserExistException() : base("User exist") { }
    }
}
