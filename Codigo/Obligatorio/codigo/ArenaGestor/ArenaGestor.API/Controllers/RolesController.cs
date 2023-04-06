using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Roles;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArenaGestor.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class RolesController : ControllerBase, IRolesAppService
    {
        private readonly IRolesService roleService;
        private readonly IMapper mapper;

        public RolesController(IRolesService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpGet("User")]
        public IActionResult GetUserRoles()
        {
            var result = roleService.GetUserRoles();
            var resultDto = mapper.Map<IEnumerable<RolesResultDto>>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpGet("Artist")]
        public IActionResult GetArtistRoles()
        {
            var result = roleService.GetArtistRoles();
            var resultDto = mapper.Map<IEnumerable<RolesArtistResultDto>>(result);
            return Ok(resultDto);
        }
    }
}
