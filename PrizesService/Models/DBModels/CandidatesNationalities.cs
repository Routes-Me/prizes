using System;
using System.Collections.Generic;

namespace PrizesService.Models.DBModels
{
    public partial class CandidatesNationalities
    {
        public int CandidateId { get; set; }
        public int NationalityId { get; set; }

        public virtual Candidates Candidate { get; set; }
    }
}
