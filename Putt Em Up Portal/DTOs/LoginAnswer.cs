using Putt_Em_Up_Portal.Models;

namespace Putt_Em_Up_Portal.DTOs
{
    public class LoginAnswer
    {
        public long PlayerID {get;set;}
        public string Username {get;set; }
        
        public string Token {get;set;}

        public  LoginAnswer(Player p) { 
        PlayerID= p.PlayerID;
        Username = p.Username;    
        
        }


    }
}
