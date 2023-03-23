using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB.application.DTOs.Security
{
    public class PasswordAndSaltedStringInfo
    { 
        public string HashedPassword { get; set; }
        public string SecurityKey { get; set; }

    }
}
