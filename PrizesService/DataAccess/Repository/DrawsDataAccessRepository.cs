using Microsoft.AspNetCore.Http;
using PrizesService.DataAccess.Abstraction;
using PrizesService.Helper;
using PrizesService.Models;
using PrizesService.Models.DBModels;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using RoutesSecurity;

namespace PrizesService.DataAccess.Repository
{
    public class DrawsDataAccessRepository : IDrawsDataAccessRepository
    {
        private readonly prizesserviceContext _context;
        public DrawsDataAccessRepository(prizesserviceContext context)
        {
            _context = context;
        }
        public dynamic DeleteDraws(string drawsId)
        {
            int drawsIdDecrypted = Obfuscation.Decode(drawsId);
            var draws = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (draws == null)
                Common.ThrowException(CommonMessage.DrawsNotFound, StatusCodes.Status404NotFound);

            _context.Draws.Remove(draws);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.DrawsDelete, false);
        }

        public dynamic GetDraws(string drawId, Pagination pageInfo)
        {
            int totalCount = 0;
            DrawsResponse response = new DrawsResponse();
            List<DrawsModel> drawsList = new List<DrawsModel>();

            if (string.IsNullOrEmpty(drawId))
            {
                drawsList = (from draws in _context.Draws
                             select new DrawsModel()
                             {
                                 DrawId = Obfuscation.Encode(draws.DrawId),
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
                int drawsIdDecrypted = Obfuscation.Decode(drawId);
                drawsList = (from draws in _context.Draws
                             where draws.DrawId == drawsIdDecrypted
                             select new DrawsModel()
                             {
                                 DrawId = Obfuscation.Encode(draws.DrawId),
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
            int drawsIdDecrypted = Obfuscation.Decode(drawsModel.DrawId);
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
