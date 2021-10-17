using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Domain.Entidades
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Login()
        {
            Username = "fulano.de.tal";
            Password = "jhk34g6j363j5h3";
        }
    }
}
