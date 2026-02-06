using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.AvatarProvider
{
    public class AvatarProvider : IAvatarProvider
    {
        public string CreateAvatar(string imageInBase64, string username)
        {
            Guid imgGuid = Guid.NewGuid();
            try
            {

                BinaryWriter bw = new(System.IO.File.Create($".." + Path.DirectorySeparatorChar + "Infrastructure.Persistence" + Path.DirectorySeparatorChar + "ProfilePictures" + Path.DirectorySeparatorChar + username + imgGuid + ".png"));
                bw.Write(Convert.FromBase64String(imageInBase64));
                bw.Close();
            }
            catch (Exception e) { return ""; }

            return username + imgGuid;
        }

        public byte[] GetAvatar(string avatarFilePath)
        {
            byte[] result = null;
            try { result = File.ReadAllBytes($".." + Path.DirectorySeparatorChar+"Infrastructure.Persistence" + Path.DirectorySeparatorChar + "ProfilePictures" + Path.DirectorySeparatorChar + avatarFilePath + ".png"); }
            catch (Exception e) { result = null;  }
            return result;
        }
    }
}
