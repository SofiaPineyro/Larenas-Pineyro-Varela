using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts.Snack;
using ArenaGestor.APIContracts;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ArenaGestor.BusinessInterface;

namespace ArenaGestor.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class SnacksController : ControllerBase, ISnacksAppService
    {
        private readonly ISnacksService snackService;
        private readonly IMapper mapper;

        public SnacksController(ISnacksService isnackService, IMapper mapper)
        {
            snackService = isnackService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSnacks()
        {
            var result = snackService.GetSnacks();
            var resultDto = mapper.Map<IEnumerable<SnackResultSnackDto>>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPost]
        public IActionResult PostSnack([FromBody] InsertSnackDto insertSnack)
        {
            var snack = mapper.Map<Snack>(insertSnack);
            var result = snackService.InsertSnack(snack);
            var resultDto = mapper.Map<SnackResultSnackDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpDelete("{snackId}")]
        public IActionResult DeleteSnack([FromRoute] int snackId)
        {
            snackService.DeleteSnack(snackId);
            return Ok();
        }

        [AuthorizationFilter(RoleCode.Espectador)]
        [HttpPost("Buy")]
        public IActionResult PostSnacks([FromBody] BuySnackDto buySnack)
        {
            var snackBuy = mapper.Map<Snack>(buySnack);
            Snack snack = snackService.BuySnack(snackBuy);
            var resultDto = mapper.Map<SnackResultSnackDto>(snack);
            return Ok(resultDto);
        }

    }

}
