using PrizesService.Models;
using PrizesService.Models.ResponseModel;

namespace PrizesService.Abstraction
{
    public interface ICandidatesRepository
    {
        dynamic InsertCandidates(string drawId, CandidatesModel candidatesModel);
        dynamic GetCandidates(string drawId, string candidateId, Pagination pageInfo);
        dynamic UpdateCandidates(string drawsId, CandidatesModel candidatesModel);
        dynamic DeleteCandidates(string candidatesId);
    }
}
