using Application.DTOs;
using MediatR;
 

namespace Application.Player.Queries
{
    public class FindProfileByUsernameQuery: IRequest<Profile>
    {
        public string Username { get; set; }
    }
}
