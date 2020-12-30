using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrizesService.Abstraction;
using PrizesService.Models;
using PrizesService.Models.ResponseModel;

namespace PrizesService.Controllers
{
    [Route("api")]
    [ApiController]
    public class NationalitiesController : ControllerBase
    {
        private readonly INationalitiesRepository  _nationalitiesRepository;
        public NationalitiesController(INationalitiesRepository nationalitiesRepository)
        {
            _nationalitiesRepository = nationalitiesRepository;
        }

        [HttpPost]
        [Route("nationalities")]
        public IActionResult Post(NationalitiesModel nationalitiesModel)
        {
            dynamic response = _nationalitiesRepository.InsertNationalities(nationalitiesModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpGet]
        [Route("nationalities/{nationalitiesId=0}")]
        public IActionResult Get( string nationalitiesId, [FromQuery] Pagination pageInfo)
        {
            dynamic response = _nationalitiesRepository.GetNationalities(nationalitiesId, pageInfo);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPut]
        [Route("nationalities")]
        public IActionResult Put(NationalitiesModel nationalitiesModel)
        {
            dynamic response = _nationalitiesRepository.UpdateNationalities(nationalitiesModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpDelete]
        [Route("nationalities/{nationalitiesId}")]
        public IActionResult Delete(string nationalitiesId)
        {
            dynamic response = _nationalitiesRepository.DeleteNationalities(nationalitiesId);
            return StatusCode((int)response.statusCode, response);
        }
    }
}
