using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using API.Data;
using System.Reflection;
using Model.Notification;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using API.Service;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly EmailSettings _emailSettings;
        public NotificationController(
            ApplicationDbContext context, 
            IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> SendEmail(Notification model)
        {

            try
            {
                // Mockandos os dados se não tiver paramêtro
                if (String.IsNullOrEmpty(model.ClientName) || model.ClientName == "string")
                {
                    model.ClientName = "Kleber, me deu ATP, professor gente boa dms";
                    model.cupomDeDesconto = "BONE-15";
                }
                var htmlContent = EmailService.WelcomeEmail(model.ClientName, model.cupomDeDesconto);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
                message.To.Add(new MailboxAddress("Destinatário", "matheushenriquecanuto77@gmail.com"));
                message.Subject = "Bem-vindo(a) ao Time dos Caçadores de Relíquias!";

                message.Body = new TextPart("html")
                {
                    Text = htmlContent
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(
                        _emailSettings.SmtpServer,
                        _emailSettings.SmtpPort,
                        false
                    );
                    await client.AuthenticateAsync(
                        _emailSettings.SmtpUsername,
                        _emailSettings.SmtpPassword
                    );
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha ao enviar e-mail.");
            }


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
