using Microsoft.AspNetCore.Mvc;
using PrizesService.Abstraction;
using PrizesService.Models;
using PrizesService.Models.ResponseModel;

namespace PrizesService.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("v{version:apiVersion}/")]
    public class SpinnersController : ControllerBase
    {

        private readonly ISpinnersRepository _spinnersRepository;
        public SpinnersController(ISpinnersRepository spinnersRepository)
        {
            _spinnersRepository = spinnersRepository;
        }

        [HttpPost]
        [Route("draws/{drawsId}/spins")]
        public IActionResult Post(string drawsId, PostSpinsModel spinsModel)
        {
            dynamic response = _spinnersRepository.InsertSpins(drawsId, spinsModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpGet]
        [Route("draws/{drawsId}/spins/{spinsId=0}")]
        public IActionResult Get(string drawsId, string spinsId, string include, [FromQuery] Pagination pageInfo)
        {
            dynamic response = _spinnersRepository.GetSpins(drawsId, spinsId, include, pageInfo);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPut]
        [Route("draws/{drawsId}/spins")]
        public IActionResult Put(string drawsId, PostSpinsModel spinsModel)
        {
            dynamic response = _spinnersRepository.UpdateSpins(drawsId, spinsModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpDelete]
        [Route("spins/{spinsId}")]
        public IActionResult Delete(string spinsId)
        {
            dynamic response = _spinnersRepository.DeleteSpins(spinsId);
            return StatusCode((int)response.statusCode, response);
        }
    }
}
