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
    public class SpinnersController : ControllerBase
    {

        private readonly ISpinnersRepository _spinnersRepository;
        public SpinnersController(ISpinnersRepository spinnersRepository)
        {
            _spinnersRepository = spinnersRepository;
        }

        //[HttpPost]
        //[Route("draws/{drawsId}/spin/{officerId}")]
        //public IActionResult Post(string drawsId, string officerId, SpinnersModel spinnersModel)
        //{
        //    dynamic response = _spinnersRepository.InsertSpinners(drawsId, officerId, spinnersModel);
        //    return StatusCode((int)response.statusCode, response);
        //}

        //[HttpGet]
        //[Route("draws/{drawsId}/spin/{spinId=0}")]
        //public IActionResult Get(string drawsId,string spinId, string include, [FromQuery] Pagination pageInfo)
        //{
        //    dynamic response = _spinnersRepository.GetSpinners(drawsId, spinId, include, pageInfo);
        //    return StatusCode((int)response.statusCode, response);
        //}

        //[HttpPut]
        //[Route("draws/{drawsId}/spin/{officerId}")]
        //public IActionResult Put(string drawsId, string officerId, SpinnersModel spinnersModel)
        //{
        //    dynamic response = _spinnersRepository.UpdateSpinners(drawsId, officerId, spinnersModel);
        //    return StatusCode((int)response.statusCode, response);
        //}

        //[HttpDelete]
        //[Route("spin/{spinId}")]
        //public IActionResult Delete(string spinId)
        //{
        //    dynamic response = _spinnersRepository.DeleteSpinners(spinId);
        //    return StatusCode((int)response.statusCode, response);
        //}
    }
}
