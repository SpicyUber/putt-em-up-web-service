using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries
{
    public class GetChatQuery : IRequest<List<Domain.Message>>
    {
        public long FirstPlayerID { get; set; }
        public long SecondPlayerID { get; set; }
    }
}
