using Microsoft.AspNetCore.Mvc;
using API.Data;
using Model.Order;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            if (order == null || string.IsNullOrEmpty(order.Customer) || order.Total <= 0)
                return BadRequest(new { message = "Dados do pedido inválidos." });

            order.Date = DateTime.UtcNow;
            order.Status = "Pagamento em Processamento";

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Order.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
                return NotFound(new { message = "Pedido não encontrado." });

            return Ok(order);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
                return NotFound(new { message = "Pedido não encontrado." });

            var validStatuses = new List<string>
            {
                "Pagamento em Processamento",
                "Pagamento Aprovado",
                "Enviado",
                "Recebido"
            };

            if (string.IsNullOrEmpty(status) || !validStatuses.Contains(status))
            {
                return BadRequest(new
                {
                    message = "Status inválido. Os status válidos são: " + string.Join(", ", validStatuses)
                });
            }

            order.Status = status;
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}