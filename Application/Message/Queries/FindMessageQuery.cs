using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries
{
    public class FindMessageQuery : IRequest<Domain.Message>
    {
        public DateTime SentTimestamp { get; set; }
        public long FromPlayerID { get; set; }
        public long ToPlayerID { get; set; }
    }
}
