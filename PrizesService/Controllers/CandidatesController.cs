﻿using System;
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
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesRepository _candidatesRepository;
        public CandidatesController(ICandidatesRepository candidatesRepository)
        {
            _candidatesRepository = candidatesRepository;
        }

        [HttpPost]
        [Route("draws/{drawId}/candidates")]
        public IActionResult Post(string drawsId, CandidatesModel candidatesModel)
        {
            dynamic response = _candidatesRepository.InsertCandidates(drawsId, candidatesModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpGet]
        [Route("draws/{drawId}/candidates/{candidateId?}")]
        public IActionResult Get(string drawId, string candidateId, [FromQuery] Pagination pageInfo)
        {
            dynamic response = _candidatesRepository.GetCandidates(drawId, candidateId, pageInfo);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPut]
        [Route("draws/{drawId}/candidates")]
        public IActionResult Put(string drawsId, CandidatesModel candidatesModel)
        {
            dynamic response = _candidatesRepository.UpdateCandidates(drawsId, candidatesModel);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpDelete]
        [Route("candidates/{candidatesId}")]
        public IActionResult Delete(string candidatesId)
        {
            dynamic response = _candidatesRepository.DeleteCandidates(candidatesId);
            return StatusCode((int)response.statusCode, response);
        }
    }
}
