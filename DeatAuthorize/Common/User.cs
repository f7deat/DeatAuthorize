using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeatAuthorize.Common
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public int Role { get; set; }
    }
}