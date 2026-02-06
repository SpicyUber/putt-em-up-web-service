using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Queries
{
    public class SearchMatchesQuery : IRequest<MatchPreviewPage>
    {
        public long PlayerID { get; set; }
        public DateTime StartDate { get; set; }


        public SearchMode Mode { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

    }
}
