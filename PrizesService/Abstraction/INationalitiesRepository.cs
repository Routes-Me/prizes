using PrizesService.Models;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Abstraction
{
    public interface INationalitiesRepository
    {
        dynamic InsertNationalities(NationalitiesModel nationalitiesModel);
        dynamic GetNationalities(string nationalitiesId, Pagination pageInfo);
        dynamic UpdateNationalities(NationalitiesModel nationalitiesModel);
        dynamic DeleteNationalities(string nationalitiesId);
    }
}
