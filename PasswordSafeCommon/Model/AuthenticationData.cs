﻿using System;
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
    }
}
