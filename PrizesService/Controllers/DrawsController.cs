using Microsoft.AspNetCore.Mvc;
using PrizesService.Abstraction;
using PrizesService.Models;
using PrizesService.Models.ResponseModel;

namespace PrizesService.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("v{version:apiVersion}/")]
    public class DrawsController : ControllerBase
    {
        private readonly IDrawsRepository _drawsRepository;
        public DrawsController(IDrawsRepository drawsRepository)
        {
            _drawsRepository = drawsRepository;
        }

        [HttpPost]
        [Route("draws")]
        public IActionResult Post(DrawsModel drawsModel)
        {
            dynamic response = _drawsRepository.InsertDraws(drawsModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpGet]
        [Route("draws/{drawId?}")]
        public IActionResult Get(string drawId, [FromQuery] Pagination pageInfo)
        {
            dynamic response = _drawsRepository.GetDraws(drawId, pageInfo);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPut]
        [Route("draws")]
        public IActionResult Put(DrawsModel drawsModel)
        {
            dynamic response = _drawsRepository.UpdateDraws(drawsModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpDelete]
        [Route("draws/{drawsId}")]
        public IActionResult Delete(string drawsId)
        {
            dynamic response = _drawsRepository.DeleteDraws(drawsId);
            return StatusCode((int)response.statusCode, response);
        }
    }
}
