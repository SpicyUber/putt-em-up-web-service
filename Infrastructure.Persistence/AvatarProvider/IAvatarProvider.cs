using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.AvatarProvider
{
    public interface IAvatarProvider
    {
        string CreateAvatar(string imageInBase64, string username );
        byte[] GetAvatar(string avatarFilePath);
    }
}
