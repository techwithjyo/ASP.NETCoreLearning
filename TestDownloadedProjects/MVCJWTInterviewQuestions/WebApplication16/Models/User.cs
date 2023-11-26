using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication16.Models
{
    public class MyToken
    {
        public string RefreshToken { get; set; }
        public string TokenValue { get; set; }
    }
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
