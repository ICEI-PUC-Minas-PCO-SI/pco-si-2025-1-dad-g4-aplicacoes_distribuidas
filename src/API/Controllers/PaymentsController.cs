using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Payments;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<Payments>> CreatePayment(Payments payment)
        {
            payment.Status = "Pending";
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

            payment.OrderId = updatedPayment.OrderId;
            payment.Amount = updatedPayment.Amount;
            payment.PaymentMethod = updatedPayment.PaymentMethod;
            payment.Status = updatedPayment.Status;
            payment.PaidAt = updatedPayment.PaidAt;

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
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
