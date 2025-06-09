namespace API.ViewModel
{
    public class NotificationViewModel
    {
        public string Recipient { get; set; }
        public string ClientName { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Priority { get; set; }
        public int Retries { get; set; }
        public string ErrorMessage { get; set; }
        public string cupomDeDesconto { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string previsaoEntrega { get; set; }


    }
}
