using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.IdentityServer.Model
{
    public class UserLoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
