using Domain;

namespace Putt_Em_Up_Portal.DTOs
{
    public class ProfileEditParams 
    {

       
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string AvatarInBase64 { get; set; }
    }
}
