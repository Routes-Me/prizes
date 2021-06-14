using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.DataAccess.Abstraction
{
    public interface ICandidatesDataAccessRepository
    {
        dynamic DeleteCandidates(string candidatesId);
        dynamic GetCandidates(string drawsId, string candidatesId, Pagination pageInfo);
        dynamic InsertCandidates(string drawId, CandidatesModel candidatesModel);
        dynamic UpdateCandidates(string drawsId, CandidatesModel candidatesModel);
    }
}
