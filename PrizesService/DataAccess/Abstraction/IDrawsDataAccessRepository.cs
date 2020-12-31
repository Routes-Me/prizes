using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.DataAccess.Abstraction
{
    public interface IDrawsDataAccessRepository
    {
        dynamic DeleteDraws(string drawsId);
        dynamic GetDraws(string drawsId, Pagination pageInfo);
        dynamic InsertDraws(DrawsModel drawsModel);
        dynamic UpdateDraws(DrawsModel drawsModel);
    }
}
