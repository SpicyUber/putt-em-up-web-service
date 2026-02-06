using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Commands
{
    public class EditMatchCommand: IRequest<Domain.Match>
    {
        public long Id { get; set; }

        public bool Cancelled { get; set; }
    }
}
