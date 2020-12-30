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
    public class DrawsRepository : IDrawsRepository
    {
        private IDrawsDataAccessRepository _drawsDataAccessRepository;
        public DrawsRepository(IDrawsDataAccessRepository drawsDataAccessRepository)
        {
            _drawsDataAccessRepository = drawsDataAccessRepository;
        }
        public dynamic DeleteDraws(string drawsId)
        {
            try
            {
                return _drawsDataAccessRepository.DeleteDraws(drawsId);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic GetDraws(string drawsId, Pagination pageInfo)
        {
            try
            {
                return _drawsDataAccessRepository.GetDraws(drawsId, pageInfo);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic InsertDraws(DrawsModel drawsModel)
        {
            try
            {
                return _drawsDataAccessRepository.InsertDraws(drawsModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }

        public dynamic UpdateDraws(DrawsModel drawsModel)
        {
            try
            {
                return _drawsDataAccessRepository.UpdateDraws(drawsModel);
            }
            catch (Exception ex)
            {
                return ReturnResponse.ExceptionResponse(ex);
            }
        }
    }
}
