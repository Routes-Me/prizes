using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Abstraction
{
    public interface IDrawsRepository
    {
        dynamic InsertDraws(DrawsModel drawsModel);
        dynamic GetDraws(string drawsId, Pagination pageInfo);
        dynamic UpdateDraws(DrawsModel drawsModel);
        dynamic DeleteDraws(string drawsId);
    }
}
