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
    public class SpinnersDataAccessRepository : ISpinnersDataAccessRepository
    {
        private readonly prizesserviceContext _context;
        private readonly IObfuscationRepository _obfuscationRepository;
        public SpinnersDataAccessRepository(prizesserviceContext context, IObfuscationRepository obfuscationRepository)
        {
            _context = context;
            _obfuscationRepository = obfuscationRepository;
        }
        public dynamic DeleteSpins(string spinsId)
        {
            throw new NotImplementedException();
        }

        public dynamic GetSpins(string spinsId, string drawsId, string include, Pagination pageInfo)
        {

            throw new NotImplementedException();
        }

        public dynamic InsertSpins(string drawsId, PostSpinsModel spinsModel)
        {
            int drawsIdDecrypted = _obfuscationRepository.IdDecryption(drawsId);
            int officerIdDecrypted = _obfuscationRepository.IdDecryption(spinsModel.OfficerId);
            var draws = _context.Draws.Where(x => x.DrawId == drawsIdDecrypted).FirstOrDefault();
            if (draws == null)
                Common.ThrowException(CommonMessage.DrawsNotFound, StatusCodes.Status404NotFound);

            var winnersOfSameDraw = (from drawsCandidates in _context.DrawsCandidates
                                     join drawWinner in _context.DrawWinners on drawsCandidates.CandidateId equals drawWinner.CandidateId
                                     where drawsCandidates.DrawId == drawsIdDecrypted
                                     select drawsCandidates).ToList();

            var filteredCandidates = _context.DrawsCandidates.Where(c => !winnersOfSameDraw.Select(b => b.CandidateId).Contains(c.CandidateId)).ToList();
            var random = new Random();
            int index = random.Next(filteredCandidates.Count);
            var winners = filteredCandidates[index];

            Spins spins = new Spins();
            spins.OfficerId = officerIdDecrypted;
            spins.DrawId = drawsIdDecrypted;
            spins.CreatedAt = DateTime.Now;
            _context.Spins.Add(spins);
            _context.SaveChanges();

            DrawWinners drawWinners = new DrawWinners();
            drawWinners.CandidateId = winners.CandidateId;
            drawWinners.SpinId = spins.SpinId;
            drawWinners.CreatedAt = DateTime.Now;
            _context.DrawWinners.Add(drawWinners);
            _context.SaveChanges();
            return ReturnResponse.SuccessResponse(CommonMessage.SpinInsert, true);
        }

        public dynamic UpdateSpins(string drawsId, PostSpinsModel spinsModel)
        {
            throw new NotImplementedException();
        }
    }
}
