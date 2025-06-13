using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Model.Autentication;

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

        // Retorna todos os usuários cadastrados
        // Funciona utilizando o mesmo caminho de pesquisa por ID, mas sem incluir o ID
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
        {
            var users = await _context.Autentication
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.Role
                })
                .ToListAsync();

            return Ok(users);
        }


        // Cadastra usuário
        [HttpPost]
        public async Task<ActionResult> Register(Autentication model)
        {
            // hashear a senha
            var hasher = new PasswordHasher<Autentication>();

            // senha digitada é transformada em um hash seguro
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

            user.PasswordHash = null;
            return Ok(user);
        }

        // Atualiza dados do usuário
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Autentication updateModel)
        {
            var user = await _context.Autentication.FindAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado");

            user.Username = updateModel.Username;
            user.Role = updateModel.Role;

           
            if (!string.IsNullOrWhiteSpace(updateModel.PasswordHash))
            {
                var hasher = new PasswordHasher<Autentication>();
                user.PasswordHash = hasher.HashPassword(user, updateModel.PasswordHash);
            }

            await _context.SaveChangesAsync();
            return Ok("Usuário atualizado");
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

        // Esqueci minha senha
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] string username)
        {
            var user = await _context.Autentication.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return NotFound("Usuário não encontrado");

            // Gera uma nova senha até o usuário atualizar
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);

            var hasher = new PasswordHasher<Autentication>();
            user.PasswordHash = hasher.HashPassword(user, novaSenha);

            await _context.SaveChangesAsync();

            return Ok($"Nova senha gerada: {novaSenha}");
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

            return Ok($"Usuário deletado com sucesso");
        }
    }
}
