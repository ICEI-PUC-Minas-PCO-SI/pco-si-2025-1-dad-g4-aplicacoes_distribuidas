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

        // ✅ Criar novo pagamento
        [HttpPost]
        public async Task<ActionResult<Payments>> CreatePayment([FromBody] Payments payment)
        {
            if (payment == null || payment.Amount <= 0 || payment.OrderId <= 0 || string.IsNullOrEmpty(payment.PaymentMethod))
            {
                return BadRequest(new { message = "Dados do pagamento inválidos." });
            }

            payment.Status = "Pending";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }

        // ✅ Buscar pagamento por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Payments>> GetPaymentById(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound(new { message = "Pagamento não encontrado." });
            }

            return Ok(payment);
        }

        // ✅ Listar todos os pagamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payments>>> GetAllPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }

        // ✅ Atualizar pagamento completo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payments updatedPayment)
        {
            if (id != updatedPayment.Id)
            {
                return BadRequest(new { message = "ID do pagamento não corresponde ao ID informado." });
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound(new { message = "Pagamento não encontrado." });
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
                    return NotFound(new { message = "Pagamento não encontrado durante a atualização." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ✅ Atualizar apenas o status do pagamento
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, [FromBody] string status)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound(new { message = "Pagamento não encontrado." });
            }

            var validStatuses = new List<string>
            {
                "Pending",
                "Paid",
                "Failed",
                "Refunded"
            };

            if (string.IsNullOrEmpty(status) || !validStatuses.Contains(status))
            {
                return BadRequest(new
                {
                    message = "Status inválido. Os status válidos são: " + string.Join(", ", validStatuses)
                });
            }

            payment.Status = status;
            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        // ✅ Remover pagamento
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound(new { message = "Pagamento não encontrado." });
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