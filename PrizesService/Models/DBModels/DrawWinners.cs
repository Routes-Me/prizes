using System;
using System.Collections.Generic;

namespace PrizesService.Models.DBModels
{
    public partial class DrawWinners
    {
        public int DrawWinnerId { get; set; }
        public int? CandidateId { get; set; }
        public int? SpinId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Candidates Candidate { get; set; }
        public virtual Spins Spin { get; set; }
    }
}
