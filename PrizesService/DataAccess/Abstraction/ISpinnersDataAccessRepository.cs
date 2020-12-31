using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.DataAccess.Abstraction
{
    public interface ISpinnersDataAccessRepository
    {
        dynamic DeleteSpins(string spinsId);
        dynamic GetSpins(string drawsId, string spinsId, string include, Pagination pageInfo);
        dynamic InsertSpins(string drawsId, PostSpinsModel spinsModel);
        dynamic UpdateSpins(string drawsId, PostSpinsModel spinsModel);
    }
}
