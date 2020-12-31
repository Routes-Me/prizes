using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Abstraction
{
    public interface ISpinnersRepository
    {
        dynamic InsertSpins(string drawsId, PostSpinsModel spinsModel);
        dynamic GetSpins(string drawsId, string spinsId, string include, Pagination pageInfo);
        dynamic UpdateSpins(string drawsId, PostSpinsModel spinsModel);
        dynamic DeleteSpins(string spinsId);
    }
}
