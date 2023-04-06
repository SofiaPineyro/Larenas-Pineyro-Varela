using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Artist;
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
    public class ArtistsController : ControllerBase, IArtistsAppService
    {
        private readonly IArtistsService artistService;
        private readonly IMapper mapper;

        public ArtistsController(IArtistsService artistService, IMapper mapper)
        {
            this.artistService = artistService;
            this.mapper = mapper;
        }


        [HttpGet("{artistId}")]
        [ExceptionFilter]
        public IActionResult GetArtistById([FromRoute] int artistId)
        {
            var result = artistService.GetArtistById(artistId);
            var resultDto = mapper.Map<ArtistResultArtistDto>(result);
            return Ok(resultDto);
        }

        [HttpGet]
        public IActionResult GetArtists([FromQuery] ArtistGetArtistsDto artistFilter)
        {
            var artist = mapper.Map<Artist>(artistFilter);
            var result = artistService.GetArtists(artist);
            var resultDto = mapper.Map<IEnumerable<ArtistResultArtistDto>>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPost]
        public IActionResult PostArtist([FromBody] ArtistInsertArtistDto insertArtist)
        {
            var artist = mapper.Map<Artist>(insertArtist);
            var result = artistService.InsertArtist(artist);
            var resultDto = mapper.Map<ArtistResultArtistDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPut]
        public IActionResult PutArtist([FromBody] ArtistUpdateArtistDto updateArtist)
        {
            var artist = mapper.Map<Artist>(updateArtist);
            var result = artistService.UpdateArtist(artist);
            var resultDto = mapper.Map<ArtistResultArtistDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpDelete("{artistId}")]
        public IActionResult DeleteArtist([FromRoute] int artistId)
        {
            artistService.DeleteArtist(artistId);
            return Ok();
        }

    }
}
