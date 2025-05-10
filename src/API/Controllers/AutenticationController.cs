using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.DTO;
using API.Data;
using API.Model.Autentication;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutenticationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            if (_context.Autenticacoes.Any(u => u.Email == dto.Email))
                return BadRequest("Usuário já existe com esse email.");

            var user = new Autentication
            {
                Email = dto.Email,
                Nome = dto.Nome,
                SenhaHash = HashPassword(dto.Senha)
            };

            _context.Autenticacoes.Add(user);
            _context.SaveChanges();

            return Ok("Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var user = _context.Autenticacoes.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || user.SenhaHash != HashPassword(dto.Senha))
                return Unauthorized("Credenciais inválidas.");

            return Ok("Login bem-sucedido!");
        }

        private string HashPassword(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
