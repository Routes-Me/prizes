using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Models.ResponseModel
{
    public class CandidatesModel
    {
        public string CandidateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string NationalityId { get; set; }
    }

    public class CandidatesGetModel
    {
        public string CandidateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
