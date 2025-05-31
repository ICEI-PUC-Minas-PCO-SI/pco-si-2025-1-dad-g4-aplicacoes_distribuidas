using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Autentication
{
    public class Autentication
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // pode ser "Admin", "User", "Vendedor", etc.

        public Autentication() 
        {

        }

    }
}

