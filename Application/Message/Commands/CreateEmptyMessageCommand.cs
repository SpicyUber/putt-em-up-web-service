using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Commands
{
    public class CreateEmptyMessageCommand : IRequest<Domain.Message>
    {
        public long FromPlayerID { get; set; }
        public long ToPlayerID { get; set; }
    }
}
