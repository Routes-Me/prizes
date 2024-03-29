﻿using System;
using System.Collections.Generic;

namespace PrizesService.Models.DBModels
{
    public partial class Draws
    {
        public Draws()
        {
            DrawsCandidates = new HashSet<DrawsCandidates>();
            Spins = new HashSet<Spins>();
        }

        public int DrawId { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<DrawsCandidates> DrawsCandidates { get; set; }
        public virtual ICollection<Spins> Spins { get; set; }
    }
}
