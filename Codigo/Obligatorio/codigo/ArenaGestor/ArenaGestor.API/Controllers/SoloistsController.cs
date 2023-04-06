using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Soloist;
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
    public class SoloistsController : ControllerBase, ISoloistsAppService
    {
        private readonly ISoloistsService soloistService;
        private readonly IMapper mapper;

        public SoloistsController(ISoloistsService soloistService, IMapper mapper)
        {
            this.soloistService = soloistService;
            this.mapper = mapper;
        }

        [HttpGet("{soloistId}")]
        public IActionResult GetSoloistById([FromRoute] int soloistId)
        {
            var result = soloistService.GetSoloistById(soloistId);
            var resultDto = mapper.Map<SoloistResultSoloistDto>(result);
            return Ok(resultDto);
        }

        [HttpGet]
        public IActionResult GetSoloists([FromQuery] SoloistGetSoloistsDto soloistFilter)
        {
            var soloist = mapper.Map<Soloist>(soloistFilter);
            var result = soloistService.GetSoloists(soloist);
            var resultDto = mapper.Map<IEnumerable<SoloistResultSoloistDto>>(result);
            return Ok(resultDto);
        }

        [HttpGet("ByArtist")]
        public IActionResult GetSoloistsByArtist([FromQuery] SoloistGetArtistsDto artistFilter)
        {
            var artist = mapper.Map<Artist>(artistFilter);
            var result = soloistService.GetSoloistsByArtist(artist);
            var resultDto = mapper.Map<IEnumerable<SoloistResultSoloistDto>>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPost]
        public IActionResult PostSoloist([FromBody] SoloistInsertSoloistDto insertSoloist)
        {
            var soloist = mapper.Map<Soloist>(insertSoloist);
            var result = soloistService.InsertSoloist(soloist);
            var resultDto = mapper.Map<SoloistResultSoloistDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPut]
        public IActionResult PutSoloist([FromBody] SoloistUpdateSoloistDto updateSoloist)
        {
            var soloist = mapper.Map<Soloist>(updateSoloist);
            var result = soloistService.UpdateSoloist(soloist);
            var resultDto = mapper.Map<SoloistResultSoloistDto>(result);
            return Ok(resultDto);

        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpDelete("{soloistId}")]
        public IActionResult DeleteSoloist([FromRoute] int soloistId)
        {
            soloistService.DeleteSoloist(soloistId);
            return Ok();
        }

    }
}
