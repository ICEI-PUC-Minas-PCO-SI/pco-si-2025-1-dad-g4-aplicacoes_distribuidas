using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Model.Autentication;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutenticationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Cadastro de usuário
        [HttpPost]
        public async Task<ActionResult> Cadastro(Autentication model)
        {
            // Verifica se já existe um usuário com esse nome
            var usuarioExistente = await _context.Autentication
                .AnyAsync(u => u.Username == model.Username);

            if (usuarioExistente)
            {
                return Conflict("Usuário já está cadastrado.");
            }

            // Hasheia a senha antes de salvar
            var hasher = new PasswordHasher<Autentication>();
            model.PasswordHash = hasher.HashPassword(model, model.PasswordHash);

            // Salva no banco
            _context.Autentication.Add(model);
            await _context.SaveChangesAsync();

            return Ok("Usuário cadastrado com sucesso!");
        }


        // Buscar usuário por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Autentication>> GetById(int id)
        {
            var user = await _context.Autentication.FindAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado");

            user.PasswordHash = null; // Não mostra a senha por segurança
            return Ok(user);
        }

        // Atualiza dados do usuário
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Autentication updateModel)
        {
            var user = await _context.Autentication.FindAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado");

            if (!string.IsNullOrWhiteSpace(updateModel.Username))
                user.Username = updateModel.Username;

            if (!string.IsNullOrWhiteSpace(updateModel.Role))
                user.Role = updateModel.Role;

            if (!string.IsNullOrWhiteSpace(updateModel.PasswordHash))
            {
                var hasher = new PasswordHasher<Autentication>();
                user.PasswordHash = hasher.HashPassword(user, updateModel.PasswordHash);
            }

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok("Usuário atualizado com sucesso");
        }


        // Login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Autentication login)
        {
            var user = await _context.Autentication
                .FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null)
                return Unauthorized("Usuário não encontrado");

            var hasher = new PasswordHasher<Autentication>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, login.PasswordHash);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Senha incorreta");

            return Ok("Login realizado com sucesso");
        }

        public class ForgotPasswordRequest
        {
            public string Username { get; set; }
        }


        // Esqueci minha senha
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] string username)
        {
            var user = await _context.Autentication.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return NotFound("Usuário não encontrado");

            // Gera uma nova senha temporária aleatória
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);

            var hasher = new PasswordHasher<Autentication>();
            user.PasswordHash = hasher.HashPassword(user, novaSenha);

            await _context.SaveChangesAsync();

            return Ok($"Nova senha gerada: {novaSenha}. Não esqueça de atualizar para uma senha do seu interesse mais tarde");
        }



        // Deleta usuário pelo ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            var user = await _context.Autentication.FindAsync(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _context.Autentication.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"Usuário {user} deletado com sucesso");
        }

    }
}