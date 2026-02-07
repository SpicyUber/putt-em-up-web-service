using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries
{
    public class GetAllRecentMessagesQuery: IRequest<List<Domain.Message>>
    {
        public long PlayerID { get; set; }
    }
}
