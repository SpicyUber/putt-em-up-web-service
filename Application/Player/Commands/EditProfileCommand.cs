using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Commands
{
    public class EditProfileCommand : IRequest<Profile>
    {
        public string Username { get; set; }

        public ProfileEditParams Profile { get; set; }

    }
}
