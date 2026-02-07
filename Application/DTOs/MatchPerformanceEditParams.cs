using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MatchPerformanceEditParams
    {
        public bool WonMatch { get; set; }

        public int FinalScore { get; set; }

        public int MMRDelta { get; set; }
    }
}
