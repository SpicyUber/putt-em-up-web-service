using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Commands
{
    public class DeleteAccountCommand: IRequest<bool>
    {
        public long Id { get; set; }
    }
}
