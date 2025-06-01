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
        public int Id { get; set; }

        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public string Status { get; set; }
        public Order()
        {

        }
    }
}
