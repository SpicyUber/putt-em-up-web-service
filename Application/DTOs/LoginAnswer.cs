using Domain;

namespace Application.DTOs
{
    public class LoginAnswer
    {
        public long PlayerID {get;set;}
        public string Username {get;set; }
        
        public string Token {get;set;}

        public  LoginAnswer(Domain.Player p) { 
        PlayerID= p.PlayerID;
        Username = p.Username;    
        
        }


    }
}
