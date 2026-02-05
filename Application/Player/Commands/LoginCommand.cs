using Application.DTOs;
using MediatR;


namespace Application.Player.Commands
{
    public class LoginCommand : IRequest<LoginAnswer>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
