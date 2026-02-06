using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Commands
{
    public class EditAccountCommand : IRequest<Account>
    {
        public long Id { get; set; }
        public AccountEditParams AccountParams { get; set; }
    }
}
