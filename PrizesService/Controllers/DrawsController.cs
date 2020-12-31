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
        [Route("draws/{drawsId=0}")]
        public IActionResult Get(string drawsId, [FromQuery] Pagination pageInfo)
        {
            dynamic response = _drawsRepository.GetDraws(drawsId, pageInfo);
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
