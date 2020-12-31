using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Abstraction
{
    public interface ICandidatesRepository
    {
        dynamic InsertCandidates(string drawsId, CandidatesModel candidatesModel);
        dynamic GetCandidates(string drawsId, string candidatesId, Pagination pageInfo);
        dynamic UpdateCandidates(string drawsId, CandidatesModel candidatesModel);
        dynamic DeleteCandidates(string candidatesId);
    }
}
