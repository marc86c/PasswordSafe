using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSafeCommon.Model
{
    public class AuthenticationData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
        public AuthenticationDataType Type { get; set; }
    }

    public enum AuthenticationDataType
    {
        None = 0,
        Private = 1,
        School = 2,
        Work = 3,
        Finances = 4,
        SocialMedia = 5,
        Health = 6,
        Entertainment = 7,
        Other = 8
    }
}
