using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Queries
{
    public class SearchPlayersQuery : IRequest<LeaderboardPage>
    {
        public string? UsernameStartsWith { get; set; }
        public bool DescendingRanking { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
