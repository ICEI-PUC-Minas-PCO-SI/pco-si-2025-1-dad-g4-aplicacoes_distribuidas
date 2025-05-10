using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Autentication
{
    class Autentication
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string SenhaHash { get; set; } // senha armazenada com hash

    }
}
