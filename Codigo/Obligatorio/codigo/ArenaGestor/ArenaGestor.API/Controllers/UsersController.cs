using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Users;
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
    public class UsersController : ControllerBase, IUsersAppService
    {
        private readonly IUsersService userService;
        private readonly IMapper mapper;

        public UsersController(IUsersService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpGet("{userId}")]
        public IActionResult GetUserById([FromRoute] int userId)
        {
            var result = userService.GetUserById(userId);
            var resultDto = mapper.Map<UserResultUserDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpGet]
        public IActionResult GetUsers([FromQuery] UserGetUsersDto userFilter)
        {
            var user = mapper.Map<User>(userFilter);
            var result = userService.GetUsers(user);
            var resultDto = mapper.Map<IEnumerable<UserResultUserDto>>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPost]
        public IActionResult PostUser([FromBody] UserInsertUserDto insertUser)
        {
            var user = mapper.Map<User>(insertUser);
            var result = userService.InsertUser(user);
            var resultDto = mapper.Map<UserResultUserDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPut]
        public IActionResult PutUser([FromBody] UserUpdateUserDto updateUser)
        {
            var user = mapper.Map<User>(updateUser);
            var result = userService.UpdateUser(user);
            var resultDto = mapper.Map<UserResultUserDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Espectador)]
        [HttpPut("loggedIn")]
        public IActionResult PutUserLoggedIn([FromBody] UserUpdateUserDto updateUser)
        {
            var token = this.HttpContext.Request.Headers["token"];
            var user = mapper.Map<User>(updateUser);
            var result = userService.UpdateUser(token, user);
            var resultDto = mapper.Map<UserResultUserDto>(result);
            return Ok(resultDto);

        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPut("password")]
        public IActionResult PutUserPassword([FromBody] UserChangePasswordDto updatePassword)
        {
            var newPassword = mapper.Map<UserChangePassword>(updatePassword);
            userService.ChangePassword(newPassword);
            return Ok();
        }

        [AuthorizationFilter(RoleCode.Espectador, RoleCode.Acomodador, RoleCode.Administrador, RoleCode.Vendedor)]
        [HttpPut("loggedInPassword")]
        public IActionResult PutUserLoggedInPassword([FromBody] UserChangePasswordDto updatePassword)
        {
            var token = this.HttpContext.Request.Headers["token"];
            var newPassword = mapper.Map<UserChangePassword>(updatePassword);
            userService.ChangePassword(token, newPassword);
            return Ok();
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser([FromRoute] int userId)
        {
            userService.DeleteUser(userId);
            return Ok();
        }

    }
}
