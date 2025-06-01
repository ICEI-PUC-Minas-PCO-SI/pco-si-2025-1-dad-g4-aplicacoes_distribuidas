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
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
       // public bool IsDeleted { get; set; } = false;

        public class ForgotPasswordRequest
        {
            public string Username { get; set; }
        }


        public Autentication()
        {

        }

    }

}


