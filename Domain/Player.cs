

using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Player : IdentityUser<long>
    {
         
       

        

        public bool AccountDeleted { get; set; }

        public string DisplayName { get; set; } 

        public string Description { get; set; }

        public string AvatarFilePath { get; set; }


    }
}
