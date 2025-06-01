using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payments
{
    public class Payments
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Relacionado ao pedido
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // ex: "CreditCard", "Pix", "Boleto"
        public string Status { get; set; } // Ex: "Pending", "Paid", "Failed"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; } // Quando foi pago (se foi)
    }
}
