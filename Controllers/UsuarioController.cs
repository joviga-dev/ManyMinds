using AutoMapper;
using ManyMinds.Data;
using ManyMinds.Models;
using ManyMindsApi.Data.Dto.Login;
using ManyMindsApi.Data.Dto.Produto;
using ManyMindsApi.Data.Dto.Usuario;
using ManyMindsApi.Models;
using ManyMindsApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyMindsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private ManyMindsApiContext context;
        private IMapper mapper;
        private GerenciadorDeSenhas gerenciador;
        private readonly ISystemLogService logService;

        public UsuarioController(ManyMindsApiContext context, IMapper mapper, GerenciadorDeSenhas gerenciador, ISystemLogService logService)
        {
            this.context = context;
            this.mapper = mapper;
            this.gerenciador = gerenciador;
            this.logService = logService;
        }

        [HttpPost("criar")]
        public IActionResult CriaUsuario([FromBody] AdicionarUsuarioDto dto)
        {
            Usuario usuario = this.mapper.Map<Usuario>(dto);

            usuario.Senha = gerenciador.HashPassword(dto.Senha);

            this.context.Usuarios.Add(usuario);
            this.context.SaveChanges();

            logService.AdicionaLog(
            "Adicionar",
            usuario.Login,
            usuario.Id,
            DateTime.Now,
            "Usuario " + usuario.Login + " criado com sucesso");

            return Ok("Usuario " + dto.Login + " criado com sucesso");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            var usuario = this.BuscaUsuario(dto.Login);

            if (usuario.DataBloqueio.HasValue && usuario.DataBloqueio + usuario.DuracaoBloqueio >= DateTime.Now)
            {
                logService.AdicionaLog(
                    "Bloqueio",
                    usuario.Login,
                    usuario.Id,
                    DateTime.Now,
                    "Usuario " + usuario.Login + " bloqueado por " + usuario.DuracaoBloqueio);
                return BadRequest($"O acesso para o usuário está bloqueado até {usuario.DataBloqueio + usuario.DuracaoBloqueio}");
            }

            if (!gerenciador.VerifyPassword(dto.Senha, usuario.Senha))
            {
                usuario.nTentativas += 1;
                usuario.Ip = remoteIpAddress;
                if (usuario.nTentativas >= 3 && usuario.Ip.Equals(remoteIpAddress))
                {
                    usuario.DataBloqueio = DateTime.Now;
                    usuario.DuracaoBloqueio = TimeSpan.FromMinutes(30);
                }
                this.context.SaveChanges();
                return ValidationProblem("Senha errada");

            }
            else
            {
                logService.AdicionaLog(
                    "Login",
                    usuario.Login,
                    usuario.Id,
                    DateTime.Now,
                    "Usuario " + usuario.Login + " Logado");
                return Ok();
            }
        }

        [HttpGet("/buscar/{Login}")]
        public IActionResult FindUsuarioByLogin(String Login)
        {
            var usuario = this.BuscaUsuario(Login);

            var usuarioDto = this.mapper.Map<PesquisarUsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        private Usuario BuscaUsuario(String Login)
        {
            var usuario = this.context.Usuarios
                .FirstOrDefault(usuario => usuario.Login == Login);

            if (usuario == null)
                throw new BadHttpRequestException("Usuario com o login " + Login + " não encontrado!");

            return usuario;
        }
    }
}
