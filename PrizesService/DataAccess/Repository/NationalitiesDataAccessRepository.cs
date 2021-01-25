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
    public class NationalitiesDataAccessRepository : INationalitiesDataAccessRepository
    {
        private readonly prizesserviceContext _context;
        public NationalitiesDataAccessRepository(prizesserviceContext context)
        {
            _context = context;
        }
        public dynamic DeleteNationalities(string nationalitiesId)
        {
            int nationalitiesIdDecrypted = Obfuscation.Decode(nationalitiesId);
            var nationalities = _context.Nationalities.Where(x => x.NationalityId == nationalitiesIdDecrypted).FirstOrDefault();
            if (nationalities == null)
                Common.ThrowException(CommonMessage.NationalitiesNotFound, StatusCodes.Status404NotFound);

            _context.Nationalities.Remove(nationalities);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.NationalitiesDelete, false);
        }

        public dynamic GetNationalities(string nationalityId, Pagination pageInfo)
        {
            int totalCount = 0;
            NationalitiesResponse response = new NationalitiesResponse();
            List<NationalitiesModel> nationalitiesList = new List<NationalitiesModel>();
            if (string.IsNullOrEmpty(nationalityId))
            {
                nationalitiesList = (from nationalities in _context.Nationalities
                                     select new NationalitiesModel()
                                     {
                                         NationalityId = Obfuscation.Encode(nationalities.NationalityId),
                                         Name = nationalities.Name,
                                     }).AsEnumerable().OrderBy(a => a.NationalityId).Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();

                totalCount = _context.Nationalities.Count();
            }
            else
            {
                int nationalitiesIdDecrypted = Obfuscation.Decode(nationalityId);
                nationalitiesList = (from nationalities in _context.Nationalities
                                     where nationalities.NationalityId == nationalitiesIdDecrypted
                                     select new NationalitiesModel()
                                     {
                                         NationalityId = Obfuscation.Encode(nationalities.NationalityId),
                                         Name = nationalities.Name,
                                     }).AsEnumerable().OrderBy(a => a.NationalityId).Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();

                totalCount = _context.Nationalities.Where(x => x.NationalityId == nationalitiesIdDecrypted).Count();
            }

            var page = new Pagination
            {
                offset = pageInfo.offset,
                limit = pageInfo.limit,
                total = totalCount
            };

            response.status = true;
            response.message = CommonMessage.NationalitiesRetrived;
            response.pagination = page;
            response.data = nationalitiesList;
            response.statusCode = StatusCodes.Status200OK;
            return response;
        }

        public dynamic InsertNationalities(NationalitiesModel nationalitiesModel)
        {
            Nationalities nationalities = new Nationalities();
            nationalities.Name = nationalitiesModel.Name;
            _context.Nationalities.Add(nationalities);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.NationalitiesInsert, true);
        }

        public dynamic UpdateNationalities(NationalitiesModel nationalitiesModel)
        {
            int nationalityIdDecrypted = Obfuscation.Decode(nationalitiesModel.NationalityId);
            var nationalities = _context.Nationalities.Where(x => x.NationalityId == nationalityIdDecrypted).FirstOrDefault();
            if (nationalities == null)
                Common.ThrowException(CommonMessage.NationalitiesNotFound, StatusCodes.Status404NotFound);

            nationalities.Name = nationalitiesModel.Name;
            _context.Nationalities.Update(nationalities);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.NationalitiesUpdate, false);
        }
    }
}
