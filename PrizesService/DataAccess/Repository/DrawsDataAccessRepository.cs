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
            int drawsIdDecrypted = _obfuscationRepository.IdDecryption(drawsId);
            var draws = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (draws == null)
                Common.ThrowException(CommonMessage.DrawsNotFound, StatusCodes.Status404NotFound);

            _context.Draws.Remove(draws);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.DrawsDelete, false);
        }

        public dynamic GetDraws(string drawsId, Pagination pageInfo)
        {
            int totalCount = 0;
            DrawsResponse response = new DrawsResponse();
            List<DrawsModel> drawsList = new List<DrawsModel>();
            int drawsIdDecrypted = _obfuscationRepository.IdDecryption(drawsId);
            if (drawsIdDecrypted == 0)
            {
                drawsList = (from draws in _context.Draws
                             select new DrawsModel()
                             {
                                 DrawId = _obfuscationRepository.IdEncryption(draws.DrawId),
                                 StartAt = draws.StartAt,
                                 EndAt = draws.EndAt,
                                 Name = draws.Name,
                                 Status = draws.Status,
                                 CreatedAt = draws.CreatedAt
                             }).AsEnumerable().OrderBy(a => a.DrawId).Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();

                totalCount = _context.Draws.Count();
            }
            else
            {
                drawsList = (from draws in _context.Draws
                             where draws.DrawId == drawsIdDecrypted
                             select new DrawsModel()
                             {
                                 DrawId = _obfuscationRepository.IdEncryption(draws.DrawId),
                                 StartAt = draws.StartAt,
                                 EndAt = draws.EndAt,
                                 Name = draws.Name,
                                 Status = draws.Status,
                                 CreatedAt = draws.CreatedAt
                             }).AsEnumerable().OrderBy(a => a.DrawId).Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();

                totalCount = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).Count();
            }

            var page = new Pagination
            {
                offset = pageInfo.offset,
                limit = pageInfo.limit,
                total = totalCount
            };

            response.status = true;
            response.message = CommonMessage.DrawsRetrived;
            response.pagination = page;
            response.data = drawsList;
            response.statusCode = StatusCodes.Status200OK;
            return response;
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
