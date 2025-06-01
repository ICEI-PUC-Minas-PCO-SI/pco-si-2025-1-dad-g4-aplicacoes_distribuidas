using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using API.Data;
<<<<<<< HEAD
<<<<<<< HEAD
using Model.Notification;
using Microsoft.Extensions.Options;
using API.Service;
using Model.Order;
using Model;
using API.ViewModel;
=======
using System.Reflection;
=======
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
using Model.Notification;
using Microsoft.Extensions.Options;
using API.Service;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
>>>>>>> 6f91623 (send email)
=======
using Model.Order;
using Model;
using API.ViewModel;
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)

namespace API.Controllers
{
    [Route("api/notification")]
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
<<<<<<< HEAD
<<<<<<< HEAD
        [Route("sendwelcomeemail")]
        public async Task<ActionResult<Notification>> SendWelcomeEmail(Notification model)
        {
            try
            {
                EmailService.SendEmail(model, _emailSettings);
=======
        public async Task<ActionResult<Notification>> SendEmail(Notification model)
=======
        [Route("sendwelcomeemail")]
        public async Task<ActionResult<Notification>> SendWelcomeEmail(Notification model)
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
        {
            try
            {
<<<<<<< HEAD
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

>>>>>>> 6f91623 (send email)
=======
                EmailService.SendEmail(model, _emailSettings);
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha ao enviar e-mail.");
            }
<<<<<<< HEAD
<<<<<<< HEAD
=======


>>>>>>> 6f91623 (send email)
=======
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
            Notification n = new Notification
            {
                Recipient = model.Recipient,
                Sender = "atendimento@puroosso.com",
                Body = "SendWelcomeEmail",
                SentAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                Priority = "Alta",
                Retries = 0,
                ErrorMessage = "",
                cupomDeDesconto = "BONE-15",
            };
            _context.Add(n);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("sendstatuspurchase")]
        public async Task<ActionResult<Notification>> SendStatusPurchase(NotificationViewModel notificationViewModel)
        {
            try
            {
                EmailService.SendStatusEmail(notificationViewModel, _emailSettings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha ao enviar e-mail.");
            }

            Notification n = new Notification
            {
                Recipient = notificationViewModel.Recipient,
                Sender = "atendimento@puroosso.com",
                Body = "SendStatusPurchase",
                SentAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                Priority = "Alta",
                Retries = 0,
                cupomDeDesconto = "BONE-15",
                Status = notificationViewModel.Status
            };
            _context.Add(n);
            _context.SaveChanges();
            return Ok();
        }
    }
}
