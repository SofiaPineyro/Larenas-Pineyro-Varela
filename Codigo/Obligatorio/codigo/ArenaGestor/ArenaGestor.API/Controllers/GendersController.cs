using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Gender;
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
    public class GendersController : ControllerBase, IGendersAppService
    {
        private readonly IGendersService genderService;
        private readonly IMapper mapper;

        public GendersController(IGendersService genderService, IMapper mapper)
        {
            this.genderService = genderService;
            this.mapper = mapper;
        }

        [HttpGet("{genderId}")]
        public IActionResult GetGenderById([FromRoute] int genderId)
        {
            var result = genderService.GetGenderById(genderId);
            var resultDto = mapper.Map<GenderResultGenderDto>(result);
            return Ok(resultDto);
        }

        [HttpGet]
        public IActionResult GetGenders([FromQuery] GenderGetGendersDto genderFilter)
        {
            var gender = mapper.Map<Gender>(genderFilter);
            var result = genderService.GetGenders(gender);
            var resultDto = mapper.Map<IEnumerable<GenderResultGenderDto>>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPost]
        public IActionResult PostGender([FromBody] GenderInsertGenderDto insertGender)
        {
            var gender = mapper.Map<Gender>(insertGender);
            var result = genderService.InsertGender(gender);
            var resultDto = mapper.Map<GenderResultGenderDto>(result);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPut]
        public IActionResult PutGender([FromBody] GenderUpdateGenderDto updateGender)
        {
            var gender = mapper.Map<Gender>(updateGender);
            var result = genderService.UpdateGender(gender);
            var resultDto = mapper.Map<GenderResultGenderDto>(result);
            return Ok(resultDto);

        }

        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpDelete("{genderId}")]
        public IActionResult DeleteGender([FromRoute] int genderId)
        {
            genderService.DeleteGender(genderId);
            return Ok();
        }

    }
}
