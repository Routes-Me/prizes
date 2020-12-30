using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.DataAccess.Abstraction
{
    public interface INationalitiesDataAccessRepository
    {
        dynamic DeleteNationalities(string nationalitiesId);
        dynamic GetNationalities(string nationalitiesId, Pagination pageInfo);
        dynamic InsertNationalities(NationalitiesModel nationalitiesModel);
        dynamic UpdateNationalities(NationalitiesModel nationalitiesModel);
    }
}
