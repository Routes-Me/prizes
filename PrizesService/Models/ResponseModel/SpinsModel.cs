using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Models.ResponseModel
{
    public class SpinsModel
    {
        public string SpinId { get; set; }
        public string OfficerId { get; set; }
        public string DrawId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class PostSpinsModel
    {
        public string SpinId { get; set; }
        public string OfficerId { get; set; }
    }
}
