using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Country;
using ArenaGestor.BusinessInterface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArenaGestor.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class CountrysController : ControllerBase, ICountrysAppService
    {
        private readonly ICountrysService countryService;
        private readonly IMapper mapper;

        public CountrysController(ICountrysService countryService, IMapper mapper)
        {
            this.countryService = countryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCountrys()
        {
            var result = countryService.GetCountrys();
            var resultDto = mapper.Map<IEnumerable<CountryResultDto>>(result);
            return Ok(resultDto);
        }

    }
}
