using Domain;

namespace Application.DTOs
{
    public class LoginAnswer
    {
        public long PlayerID {get;set;}
        public string Username {get;set; }
        
        public string Token {get;set;}

        public bool IsAdmin {get;set;}

        public  LoginAnswer(Domain.Player p, string token, bool isAdmin)
        {
            PlayerID = p.Id;
            Username = p.UserName;
            Token = token;
            IsAdmin = isAdmin;
        }


    }
}
