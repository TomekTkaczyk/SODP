using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class User : IdentityUser<int>
    {
        public User() : base() { }
        public User(string userName) : base(userName) { }

//        public bool ActiveStatus { get ; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public override string ToString()
        {
            return Firstname.ToString().Trim() + " " + Lastname.ToString().Trim();
        }
    }
}
