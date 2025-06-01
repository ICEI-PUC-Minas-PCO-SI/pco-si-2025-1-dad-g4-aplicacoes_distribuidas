using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Order
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]

        public string Customer { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]

        public decimal Total { get; set; }

        [Required]
        public string Status { get; set; } 

        public Order() { }
    }
}

