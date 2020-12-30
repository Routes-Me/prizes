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
    public class NationalitiesRepository : INationalitiesRepository
    {
        private INationalitiesDataAccessRepository _nationalitiesDataAccessRepository;
        public NationalitiesRepository(INationalitiesDataAccessRepository nationalitiesDataAccessRepository)
        {
            _nationalitiesDataAccessRepository = nationalitiesDataAccessRepository;
        }

        public dynamic DeleteNationalities(string nationalitiesId)
        {
            try
            {
                return _nationalitiesDataAccessRepository.DeleteNationalities(nationalitiesId);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic GetNationalities(string nationalitiesId, Pagination pageInfo)
        {
            try
            {
                return _nationalitiesDataAccessRepository.GetNationalities(nationalitiesId, pageInfo);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic InsertNationalities(NationalitiesModel nationalitiesModel)
        {
            try
            {
                return _nationalitiesDataAccessRepository.InsertNationalities(nationalitiesModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic UpdateNationalities(NationalitiesModel nationalitiesModel)
        {
            try
            {
                return _nationalitiesDataAccessRepository.UpdateNationalities(nationalitiesModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }
    }
}
