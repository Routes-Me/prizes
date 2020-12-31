using System;
using System.Collections.Generic;

namespace PrizesService.Models.DBModels
{
    public partial class DrawsCandidates
    {
        public int DrawId { get; set; }
        public int CandidateId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Candidates Candidate { get; set; }
        public virtual Draws Draw { get; set; }
    }
}
