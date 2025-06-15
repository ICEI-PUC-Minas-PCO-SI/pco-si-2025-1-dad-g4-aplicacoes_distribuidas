using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Payments;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Notification;
using Microsoft.Extensions.Options;
using API.Service;
using API.ViewModel;
using Model;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSettings _emailSettings;

        public PaymentsController(ApplicationDbContext context,
            IOptions<EmailSettings> emailSettings
            )
        {
            _context = context;
            _emailSettings = emailSettings.Value;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<Payments>> CreatePayment(Payments payment)
        {
            //payment.Status = DefaultValues.StatusPedido.Pendente;

            //try
            //{
            //    // Um payment tem uma order e uma ordem tem um id usuário
            //    var user = await OrderService.GetUserNameById(payment.Order, _context);
            //    NotificationViewModel notificationViewModel = new NotificationViewModel
            //    {
            //        Customer = user,
            //        Sender = "atendimento@puroosso.com",
            //        Status = payment.Status,
            //    };
            //    EmailService.SendStatusEmail(notificationViewModel, _emailSettings);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }

        // READ - GET by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Payments>> GetPaymentById(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // READ - GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payments>>> GetAllPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, Payments updatedPayment)
        {
            if (id != updatedPayment.Id)
            {
                return BadRequest();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            //payment.Order = updatedPayment.Order;
            payment.Amount = updatedPayment.Amount;
            payment.PaymentMethod = updatedPayment.PaymentMethod;
            payment.Status = updatedPayment.Status;
            payment.PaidAt = updatedPayment.PaidAt;

            //var user = await OrderService.GetUserNameById(payment.Order, _context);
            //NotificationViewModel notificationViewModel = new NotificationViewModel
            //{
            //    Customer = user,
            //    Sender = "atendimento@puroosso.com",
            //    Status = payment.Status,
            //};

            //try
            //{
            //    EmailService.SendStatusEmail(notificationViewModel, _emailSettings);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}

            //Notification n = new Notification
            //{
            //    Recipient = notificationViewModel.Recipient,
            //    Sender = "atendimento@puroosso.com",
            //    Body = "SendStatusPurchase",
            //    SentAt = DateTime.Now,
            //    CreatedAt = DateTime.Now,
            //    Priority = "Alta",
            //    Retries = 0,
            //    cupomDeDesconto = "BONE-15",
            //    Status = notificationViewModel.Status
            //};

            //_context.Notification.Add(n);

            //_context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
