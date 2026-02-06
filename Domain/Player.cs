 

namespace Domain
{
    public class Player
    {
        public long PlayerID { get; set; }
        public string Username { get; set; }
        
        public string Password { get; set; }

        

        public bool AccountDeleted { get; set; }

        public string DisplayName { get; set; } 

        public string Description { get; set; }

        public string AvatarFilePath { get; set; }


    }
}
