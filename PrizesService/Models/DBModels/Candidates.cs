using System;
using System.Collections.Generic;

namespace PrizesService.Models.DBModels
{
    public partial class Candidates
    {
        public Candidates()
        {
            CandidatesNationalities = new HashSet<CandidatesNationalities>();
            DrawWinners = new HashSet<DrawWinners>();
            DrawsCandidates = new HashSet<DrawsCandidates>();
        }

        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<CandidatesNationalities> CandidatesNationalities { get; set; }
        public virtual ICollection<DrawWinners> DrawWinners { get; set; }
        public virtual ICollection<DrawsCandidates> DrawsCandidates { get; set; }
    }
}
