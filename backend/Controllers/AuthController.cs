using BoschPizza.Data;
using BoschPizza.Models;
using BoschPizza.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BoschPizza.Controllers;

[ApiController]

[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;
    private readonly AppDbContext _context;

    // Metodo construtor
    public AuthController(IConfiguration configuration, TokenService tokenService, AppDbContext context)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        _context = context;
    }

   [HttpPost("login")]
public IActionResult Login(UserLogin login)
{
    var usuario = _context.UserLogins
        .FirstOrDefault(u => u.Username == login.Username);

    if (usuario == null)
    {
        return Unauthorized(new { message = "Usuário ou senha inválidos" });
    }

    var hasher = new PasswordHasher<UserLogin>();

    var resultado = hasher.VerifyHashedPassword(
        usuario,
        usuario.Password,
        login.Password
    );

    if (resultado == PasswordVerificationResult.Failed)
    {
        return Unauthorized(new { message = "Usuário ou senha inválidos" });
    }

    var key = _configuration["Jwt:Key"]!;
    var issuer = _configuration["Jwt:Issuer"]!;
    var audience = _configuration["Jwt:Audience"]!;

    var token = _tokenService.GenerateToken(usuario.Username, key, issuer, audience);

    return Ok(new { token });
}

    [HttpPost("register")]
    public IActionResult Register(UserLogin register)
    {
        // validações básicas
        if (string.IsNullOrEmpty(register.Username) || string.IsNullOrEmpty(register.Password))
        {
            return BadRequest(new { message = "Username e senha são obrigatórios" });
        }

        if (register.Password.Length < 6)
        {
            return BadRequest(new { message = "Senha deve ter no mínimo 6 caracteres" });
        }

        // verifica se já existe
        if (_context.UserLogins.Any(u => u.Username == register.Username))
        {
            return BadRequest(new { message = "Usuário já existe" });
        }

        var hasher = new PasswordHasher<UserLogin>();

var usuario = new UserLogin
{
    Username = register.Username
};

usuario.Password = hasher.HashPassword(usuario, register.Password);

        _context.UserLogins.Add(usuario);
        _context.SaveChanges();

        return Ok(new { message = "Usuário cadastrado com sucesso" });
    }
}