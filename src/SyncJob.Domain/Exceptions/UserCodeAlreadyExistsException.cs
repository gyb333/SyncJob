using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace Exceptions
{
    public class UserCodeAlreadyExistsException : BusinessException
    {
        public UserCodeAlreadyExistsException(string userCode)
           : base("PM:000001", $"A User with code {userCode} has already exists!")
        {

        }
    }
}
