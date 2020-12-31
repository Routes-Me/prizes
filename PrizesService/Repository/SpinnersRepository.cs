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
    public class SpinnersRepository : ISpinnersRepository
    {
        private ISpinnersDataAccessRepository _spinnersDataAccessRepository;
        public SpinnersRepository(ISpinnersDataAccessRepository spinnersDataAccessRepository)
        {
            _spinnersDataAccessRepository = spinnersDataAccessRepository;
        }
        public dynamic DeleteSpins(string spinsId)
        {
            try
            {
                return _spinnersDataAccessRepository.DeleteSpins(spinsId);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic GetSpins(string drawsId, string spinsId, string include, Pagination pageInfo)
        {
            try
            {
                return _spinnersDataAccessRepository.GetSpins(drawsId, spinsId, include, pageInfo);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic InsertSpins(string drawsId, PostSpinsModel spinsModel)
        {
            try
            {
                return _spinnersDataAccessRepository.InsertSpins(drawsId, spinsModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic UpdateSpins(string drawsId, PostSpinsModel spinsModel)
        {
            try
            {
                return _spinnersDataAccessRepository.UpdateSpins(drawsId, spinsModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }
    }
}
