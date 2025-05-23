using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.Order
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }  // Nome do cliente

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public decimal Total { get; set; }

        public string Status { get; set; } = "Pendente";
    }
}
