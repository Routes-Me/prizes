using Microsoft.AspNetCore.Http;
using PrizesService.Abstraction;
using PrizesService.DataAccess.Abstraction;
using PrizesService.Helper;
using PrizesService.Models;
using PrizesService.Models.DBModels;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.DataAccess.Repository
{
    public class DrawsDataAccessRepository : IDrawsDataAccessRepository
    {
        private readonly prizesserviceContext _context;
        private readonly IObfuscationRepository _obfuscationRepository;
        public DrawsDataAccessRepository(prizesserviceContext context, IObfuscationRepository obfuscationRepository)
        {
            _context = context;
            _obfuscationRepository = obfuscationRepository;
        }
        public dynamic DeleteDraws(string drawsId)
        {
            throw new NotImplementedException();
        }

        public dynamic GetDraws(string drawsId, Pagination pageInfo)
        {
            throw new NotImplementedException();
        }

        public dynamic InsertDraws(DrawsModel drawsModel)
        {
            Draws draws = new Draws();
            draws.StartAt = drawsModel.StartAt;
            draws.EndAt = drawsModel.EndAt;
            draws.Name = drawsModel.Name;
            draws.Status = drawsModel.Status;
            draws.CreatedAt = DateTime.Now;
            _context.Draws.Add(draws);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.DrawsInsert, true);
        }

        public dynamic UpdateDraws(DrawsModel drawsModel)
        {
            int drawsIdDecrypted = _obfuscationRepository.IdDecryption(drawsModel.DrawId);
            var draws = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (draws == null)
                Common.ThrowException(CommonMessage.DrawsNotFound, StatusCodes.Status404NotFound);

            draws.StartAt = drawsModel.StartAt;
            draws.EndAt = drawsModel.EndAt;
            draws.Name = drawsModel.Name;
            draws.Status = drawsModel.Status;
            _context.Draws.Update(draws);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.DrawsUpdate, false);
        }
    }
}
