using System;
using System.Collections.Generic;
using System.Text;

namespace AppDWC.Models
{

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Pin { get; set; }
        public int Token { get; set; }
    }

}
