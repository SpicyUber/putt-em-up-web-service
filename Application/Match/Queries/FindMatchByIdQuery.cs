using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Queries
{
    public class FindMatchByIdQuery : IRequest<MatchPreview>
    {
        public long Id { get; set; }
    }
}
