using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Band;
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
    public class BandsController : ControllerBase, IBandsAppService
    {
        private readonly IBandsService bandService;
        private readonly IMapper mapper;

        public BandsController(IBandsService bandService, IMapper mapper)
        {
            this.bandService = bandService;
            this.mapper = mapper;
        }

        [HttpGet("{bandId}")]
        public IActionResult GetBandById([FromRoute] int bandId)
        {
            var result = bandService.GetBandById(bandId);
            var resultDto = mapper.Map<BandResultBandDto>(result);
            return Ok(resultDto);
        }

        [HttpGet]
        public IActionResult GetBands([FromQuery] BandGetBandsDto bandFilter)
        {
            var band = mapper.Map<Band>(bandFilter);
            var result = bandService.GetBands(band);
            var resultDto = mapper.Map<IEnumerable<BandResultBandDto>>(result);
            return Ok(resultDto);
        }

        [HttpGet("ByArtist")]
        public IActionResult GetBandsByArtist([FromQuery] BandGetArtistsDto artistFilter)
        {
            var artist = mapper.Map<Artist>(artistFilter);
            var result = bandService.GetBandsByArtist(artist);
            var resultDto = mapper.Map<IEnumerable<BandResultBandDto>>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPost]
        public IActionResult PostBand([FromBody] BandInsertBandDto insertBand)
        {
            var band = mapper.Map<Band>(insertBand);
            var result = bandService.InsertBand(band);
            var resultDto = mapper.Map<BandResultBandDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPut]
        public IActionResult PutBand([FromBody] BandUpdateBandDto updateBand)
        {
            var band = mapper.Map<Band>(updateBand);
            var result = bandService.UpdateBand(band);
            var resultDto = mapper.Map<BandResultBandDto>(result);
            return Ok(resultDto);

        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpDelete("{bandId}")]
        public IActionResult DeleteBand([FromRoute] int bandId)
        {
            bandService.DeleteBand(bandId);
            return Ok();
        }

    }
}
