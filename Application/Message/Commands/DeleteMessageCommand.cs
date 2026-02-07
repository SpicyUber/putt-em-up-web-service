using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Commands
{
    public class DeleteMessageCommand :IRequest<bool>
    {
        public DateTime SentTimestamp { get; set; }
        public long FromPlayerID { get; set; }
        public long ToPlayerID { get; set; }
    }
}
