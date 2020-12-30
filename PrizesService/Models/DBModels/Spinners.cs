using System;
using System.Collections.Generic;

namespace PrizesService.Models.DBModels
{
    public partial class Spinners
    {
        public Spinners()
        {
            DrawWinners = new HashSet<DrawWinners>();
        }

        public int SpinId { get; set; }
        public int? OfficerId { get; set; }
        public int? DrawId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Draws Draw { get; set; }
        public virtual ICollection<DrawWinners> DrawWinners { get; set; }
    }
}
