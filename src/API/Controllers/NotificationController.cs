using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using API.Data;
using System.Reflection;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private ApplicationDbContext _context;
        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> SendEmail(Notification todoItem)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Matheus", "matheuscanuto07@gmail.com"));
            message.To.Add(new MailboxAddress("Alice", "matheushenriquecanuto77@gmail.com"));
            message.Subject = "Tamo testando tamo testando";

            message.Body = new TextPart("plain")
            {
                Text = 
                @"Hey Alice,

                    What are you up to this weekend? Monica is throwing one of her parties on
                    Saturday and I was hoping you could make it.
                    
                    Will you be my +1?
                    
                    -- Joey
                    "
            };

            Notification n = new Notification
            {
                Recipient = "matheuscanuto07@gmai.com",
                Sender = "matheushenriquecanuto77@gmail.com",
                Body = "Hey Alice,\r\n\r\n                    What are you up to this weekend? Monica is throwing one of her parties on\r\n                    Saturday and I was hoping you could make it.\r\n                    \r\n                    Will you be my +1?\r\n                    \r\n                    -- Joey",
            };
            _context.Add(n);
            _context.SaveChanges();
            return Ok();
        }

    }
}
