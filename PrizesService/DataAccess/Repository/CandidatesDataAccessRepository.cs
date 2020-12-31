using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PrizesService.Abstraction;
using PrizesService.DataAccess.Abstraction;
using PrizesService.Helper;
using PrizesService.Models;
using PrizesService.Models.DBModels;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrizesService.DataAccess.Repository
{
    public class CandidatesDataAccessRepository : ICandidatesDataAccessRepository
    {
        private readonly prizesserviceContext _context;
        private readonly IObfuscationRepository _obfuscationRepository;
        public CandidatesDataAccessRepository(prizesserviceContext context, IObfuscationRepository obfuscationRepository)
        {
            _context = context;
            _obfuscationRepository = obfuscationRepository;
        }
        public dynamic DeleteCandidates(string candidatesId)
        {
            int candidatesIdDecrypted = _obfuscationRepository.IdDecryption(candidatesId);
            var candidates = _context.Candidates
                .Include(x => x.CandidatesNationalities)
                .Include(x => x.DrawWinners)
                .Include(x => x.DrawsCandidates)
                .Where(x => x.CandidateId == candidatesIdDecrypted).FirstOrDefault();

            if (candidates == null)
                Common.ThrowException(CommonMessage.CandidateNotFound, StatusCodes.Status404NotFound);

            if (candidates.CandidatesNationalities != null)
            {
                _context.CandidatesNationalities.RemoveRange(candidates.CandidatesNationalities);
                _context.SaveChanges();
            }

            if (candidates.DrawsCandidates != null)
            {
                _context.DrawsCandidates.RemoveRange(candidates.DrawsCandidates);
                _context.SaveChanges();
            }

            if (candidates.DrawWinners != null)
            {
                _context.DrawWinners.RemoveRange(candidates.DrawWinners);
                _context.SaveChanges();
            }
            _context.Candidates.Remove(candidates);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.CandidateDelete, false);
        }

        public dynamic GetCandidates(string drawsId, string candidatesId, Pagination pageInfo)
        {
            int totalCount = 0;
            CandidateResponse response = new CandidateResponse();
            List<CandidatesGetModel> candidatesList = new List<CandidatesGetModel>();
            int drawsIdDecrypted = _obfuscationRepository.IdDecryption(drawsId);
            var draws = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (draws == null)
                Common.ThrowException(CommonMessage.DrawsNotFound, StatusCodes.Status404NotFound);

            int candidatesIdDecrypted = _obfuscationRepository.IdDecryption(candidatesId);
            if (candidatesIdDecrypted == 0)
            {
                candidatesList = (from candidates in _context.Candidates
                                  join drawsCandidates in _context.DrawsCandidates on candidates.CandidateId equals drawsCandidates.CandidateId
                                  where drawsCandidates.DrawId == drawsIdDecrypted
                                  select new CandidatesGetModel()
                                  {
                                      CandidateId = _obfuscationRepository.IdEncryption(candidates.CandidateId),
                                      Name = candidates.Name,
                                      Email = candidates.Email,
                                      DateOfBirth = candidates.DateOfBirth,
                                      PhoneNumber = candidates.PhoneNumber,
                                      CreatedAt = candidates.CreatedAt
                                  }).AsEnumerable().OrderBy(a => a.CandidateId).Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();

                totalCount = (from candidates in _context.Candidates
                              join drawsCandidates in _context.DrawsCandidates on candidates.CandidateId equals drawsCandidates.CandidateId
                              where drawsCandidates.DrawId == drawsIdDecrypted
                              select new CandidatesGetModel() { }).ToList().Count;

            }
            else
            {
                candidatesList = (from candidates in _context.Candidates
                                  join drawsCandidates in _context.DrawsCandidates on candidates.CandidateId equals drawsCandidates.CandidateId
                                  where drawsCandidates.DrawId == drawsIdDecrypted && candidates.CandidateId == candidatesIdDecrypted
                                  select new CandidatesGetModel()
                                  {
                                      CandidateId = _obfuscationRepository.IdEncryption(candidates.CandidateId),
                                      Name = candidates.Name,
                                      Email = candidates.Email,
                                      DateOfBirth = candidates.DateOfBirth,
                                      PhoneNumber = candidates.PhoneNumber,
                                      CreatedAt = candidates.CreatedAt
                                  }).AsEnumerable().OrderBy(a => a.CandidateId).Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();

                totalCount = (from candidates in _context.Candidates
                              join drawsCandidates in _context.DrawsCandidates on candidates.CandidateId equals drawsCandidates.CandidateId
                              where drawsCandidates.DrawId == drawsIdDecrypted && candidates.CandidateId == candidatesIdDecrypted
                              select new CandidatesGetModel() { }).ToList().Count;
            }

            var page = new Pagination
            {
                offset = pageInfo.offset,
                limit = pageInfo.limit,
                total = totalCount
            };

            response.status = true;
            response.message = CommonMessage.CandidateRetrived;
            response.pagination = page;
            response.data = candidatesList;
            response.statusCode = StatusCodes.Status200OK;
            return response;
        }


        public dynamic InsertCandidates(string drawsId, CandidatesModel candidatesModel)
        {
            int drawsIdDecrypted = _obfuscationRepository.IdDecryption(drawsId);
            var draws = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (draws == null)
                Common.ThrowException(CommonMessage.DrawsNotFound, StatusCodes.Status404NotFound);

            int nationalityIdDecrypted = _obfuscationRepository.IdDecryption(candidatesModel.NationalityId);
            var nationalities = _context.Nationalities.Where(x => x.NationalityId == nationalityIdDecrypted).FirstOrDefault();
            if (nationalities == null)
                Common.ThrowException(CommonMessage.NationalitiesNotFound, StatusCodes.Status404NotFound);

            Candidates candidates = new Candidates();
            candidates.Name = candidatesModel.Name;
            candidates.Email = candidatesModel.Email;
            candidates.DateOfBirth = candidatesModel.DateOfBirth;
            candidates.PhoneNumber = candidatesModel.PhoneNumber;
            candidates.CreatedAt = DateTime.Now;
            _context.Candidates.Add(candidates);
            _context.SaveChanges();

            DrawsCandidates drawsCandidates = new DrawsCandidates();
            drawsCandidates.DrawId = drawsIdDecrypted;
            drawsCandidates.CandidateId = candidates.CandidateId;
            drawsCandidates.CreatedAt = DateTime.Now;
            _context.DrawsCandidates.Add(drawsCandidates);
            _context.SaveChanges();

            CandidatesNationalities candidatesNationalities = new CandidatesNationalities();
            candidatesNationalities.CandidateId = candidates.CandidateId;
            candidatesNationalities.NationalityId = nationalityIdDecrypted;
            _context.CandidatesNationalities.Add(candidatesNationalities);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.CandidateInsert, true);
        }

        public dynamic UpdateCandidates(string drawsId, CandidatesModel candidatesModel)
        {
            int drawsIdDecrypted = _obfuscationRepository.IdDecryption(drawsId);
            var draws = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (draws == null)
                Common.ThrowException(CommonMessage.DrawsNotFound, StatusCodes.Status404NotFound);

            int candidatesIdDecrypted = _obfuscationRepository.IdDecryption(candidatesModel.CandidateId);
            var candidates = _context.Candidates.Where(x => x.CandidateId == candidatesIdDecrypted).FirstOrDefault();
            if (candidates == null)
                Common.ThrowException(CommonMessage.CandidateNotFound, StatusCodes.Status404NotFound);

            int nationalityIdDecrypted = _obfuscationRepository.IdDecryption(candidatesModel.NationalityId);
            var nationalities = _context.Nationalities.Where(x => x.NationalityId == nationalityIdDecrypted).FirstOrDefault();
            if (nationalities == null)
                Common.ThrowException(CommonMessage.NationalitiesNotFound, StatusCodes.Status404NotFound);

            var drawsCandidatesData = _context.DrawsCandidates.Where(x => x.CandidateId == candidatesIdDecrypted && x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (drawsCandidatesData == null)
            {
                DrawsCandidates drawsCandidates = new DrawsCandidates();
                drawsCandidates.DrawId = drawsIdDecrypted;
                drawsCandidates.CandidateId = candidatesIdDecrypted;
                _context.DrawsCandidates.Add(drawsCandidates);
                _context.SaveChanges();
            }

            var candidatesNationalitiesData = _context.CandidatesNationalities.Where(x => x.CandidateId == candidatesIdDecrypted).FirstOrDefault();
            if (candidatesNationalitiesData != null)
            {
                candidatesNationalitiesData.NationalityId = nationalityIdDecrypted;
                _context.CandidatesNationalities.Update(candidatesNationalitiesData);
                _context.SaveChanges();
            }
            else
            {
                CandidatesNationalities CandidatesNationalities = new CandidatesNationalities();
                CandidatesNationalities.CandidateId = candidatesIdDecrypted;
                CandidatesNationalities.NationalityId = nationalityIdDecrypted;
                _context.CandidatesNationalities.Add(CandidatesNationalities);
                _context.SaveChanges();
            }

            candidates.Name = candidatesModel.Name;
            candidates.Email = candidatesModel.Email;
            candidates.DateOfBirth = candidatesModel.DateOfBirth;
            candidates.PhoneNumber = candidatesModel.PhoneNumber;
            _context.Candidates.Update(candidates);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.CandidateUpdate, false);
        }
    }
}
