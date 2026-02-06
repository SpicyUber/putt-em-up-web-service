using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Commands
{
    public class RegisterCommand : IRequest<LoginAnswer>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
