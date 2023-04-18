using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Security;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class SecurityController : ControllerBase, ISecurityAppService
    {
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;

        public SecurityController(ISecurityService securityService, IUsersService usersService, IMapper mapper)
        {
            this.securityService = securityService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] SecurityLoginDto LoginRequest)
        {
            string token = this.securityService.Login(LoginRequest.Email, LoginRequest.Password);
            return Ok(new SecurityTokenResponseDto() { Token = token });
        }

        [HttpPost("logout")]
        [AuthorizationFilter(RoleCode.Administrador, RoleCode.Vendedor, RoleCode.Acomodador, RoleCode.Espectador, RoleCode.Artista)]
        public IActionResult Logout([FromHeader] string token)
        {
            this.securityService.Logout(token);
            return Ok(true);
        }

        [HttpGet("user")]
        [AuthorizationFilter(RoleCode.Administrador, RoleCode.Vendedor, RoleCode.Acomodador, RoleCode.Espectador, RoleCode.Artista)]
        public IActionResult LoggedUser([FromHeader] string token)
        {
            var result = this.securityService.GetUserOfToken(token);
            var resultDto = mapper.Map<SecurityLoggedUser>(result);
            return Ok(resultDto);
        }
    }
}
