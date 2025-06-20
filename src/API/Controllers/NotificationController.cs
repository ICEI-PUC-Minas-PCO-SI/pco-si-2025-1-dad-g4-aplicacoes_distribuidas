﻿using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using API.Data;
using Model.Notification;
using Microsoft.Extensions.Options;
using API.Service;
using Model.Order;
using Model;
using API.ViewModel;

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
        [Route("welcomeemail")]
        public async Task<ActionResult<Notification>> SendWelcomeEmail(Notification model)
        {
            try
            {
                EmailService.SendEmail(model, _emailSettings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            Notification n = new Notification
                {
                    Recipient = model.Recipient,
                    ClientName = model.ClientName,
                    Sender = "atendimento@puroosso.com",
                    Body = "SendWelcomeEmail",
                    SentAt = model.SentAt,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    Priority = "Alta",
                    Retries = 0,
                    ErrorMessage = "",
                    cupomDeDesconto = "BONE-15",
                    Status = "1"
                };
            _context.Add(n);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("statuspurchase")]
        public async Task<ActionResult<Notification>> SendStatusPurchase(NotificationViewModel notificationViewModel)
        {
            try
            {
                EmailService.SendStatusEmail(notificationViewModel, _emailSettings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            Notification n = new Notification
            {
                Recipient = notificationViewModel.Recipient,
                Sender = "atendimento@puroosso.com",
                Body = "SendStatusPurchase",
                Priority = "Alta",
                Retries = 0,
                cupomDeDesconto = "BONE-15",
                Status = notificationViewModel.Status
            };
            _context.Notification.Add(n);
            _context.SaveChanges();
            return Ok();
        }
    }
}
