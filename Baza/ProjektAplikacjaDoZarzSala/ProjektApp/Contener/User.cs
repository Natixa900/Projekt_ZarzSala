using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektApp.Contener
{
   public class User
    {
        public string Name { get; set; }
        public string Pass { get; set; }
        public string mail { get; set; }
        public bool isAdmin { get; set; }

        public User(string name, string pass, string mail, bool admin)
        {
            this.Name = name;
            this.Pass = pass;
            this.mail = mail;
            this.isAdmin = admin;
        }
    }
}
