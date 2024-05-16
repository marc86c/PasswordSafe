using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSafeCommon.Model
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public List<AuthenticationData> AuthenticationDatas { get; set; } = new List<AuthenticationData>();
    }
}
