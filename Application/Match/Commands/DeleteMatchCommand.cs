using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Commands
{
    public class DeleteMatchCommand : IRequest<bool>
    {
        public long Id { get; set; }

    }
}
