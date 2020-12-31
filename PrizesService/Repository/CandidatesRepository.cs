using PrizesService.Abstraction;
using PrizesService.DataAccess.Abstraction;
using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Repository
{
    public class CandidatesRepository : ICandidatesRepository
    {
        private ICandidatesDataAccessRepository _candidatesDataAccessRepository;
        public CandidatesRepository(ICandidatesDataAccessRepository candidatesDataAccessRepository)
        {
            _candidatesDataAccessRepository = candidatesDataAccessRepository;
        }
        public dynamic DeleteCandidates(string candidatesId)
        {
            try
            {
                return _candidatesDataAccessRepository.DeleteCandidates(candidatesId);
            }
            catch(Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic GetCandidates(string drawsId, string candidatesId, Pagination pageInfo)
        {
            try
            {
                return _candidatesDataAccessRepository.GetCandidates(drawsId, candidatesId, pageInfo);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic InsertCandidates(string drawsId, CandidatesModel candidatesModel)
        {
            try
            {
                return _candidatesDataAccessRepository.InsertCandidates(drawsId, candidatesModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic UpdateCandidates(string drawsId, CandidatesModel candidatesModel)
        {
            try
            {
                return _candidatesDataAccessRepository.UpdateCandidates(drawsId, candidatesModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }
    }
}
