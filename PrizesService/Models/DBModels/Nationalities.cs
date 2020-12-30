using System;
using System.Collections.Generic;

namespace PrizesService.Models.DBModels
{
    public partial class Nationalities
    {
        public Nationalities()
        {
            CandidatesNationalities = new HashSet<CandidatesNationalities>();
        }

        public int NationalityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CandidatesNationalities> CandidatesNationalities { get; set; }
    }
}
