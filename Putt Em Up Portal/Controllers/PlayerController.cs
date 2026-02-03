
 
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.DTOs;
using Domain;
using Putt_Em_Up_Portal.Testing;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        
        [HttpGet("profiles/{username}")]
         public ActionResult<Profile> GetProfile(string username)
        {

            Player? p = LocalStorage<Player>.GetSampleList().FirstOrDefault((p) => { return p.Username == username && !p.AccountDeleted; });
            if (p != null) return Ok(new Profile(p)); else return NotFound();
            
        }



        [HttpGet("profiles/search")]

        public ActionResult<LeaderboardPage> Search([FromQuery]PlayerSearchParams playerParameters)
        {
            List<Profile> list = new();
            int totalPages = 0;
            IEnumerable<Player> players = LocalStorage<Player>.GetSampleList().Where<Player>((Player p) => { return (playerParameters.UsernameStartsWith == null || p.DisplayName.ToLower().StartsWith(playerParameters.UsernameStartsWith.ToLower())); });

            if (playerParameters.DescendingRanking) players = players.OrderByDescending((p) => { return p.MatchmakingRanking; });
            else players = players.OrderBy((p) => { return p.MatchmakingRanking; });

            if (playerParameters.PageSize == null || playerParameters.PageNumber == null)
            {

                foreach (Player p in players)
                    list.Add(new Profile(p));

                return Ok(new LeaderboardPage(list.ToArray(), totalPages));
            }
            
            for (int i = 1; i < (int)playerParameters.PageNumber; i++)
               players= players.Skip((int)playerParameters.PageSize);

            players = players.Take((int)playerParameters.PageSize);

            foreach (Player p in players)
                list.Add(new Profile(p));

            return Ok(new LeaderboardPage(list.ToArray(), (int)Math.Ceiling(list.Count / (float)playerParameters.PageSize)));


        }



        [HttpPut("profiles/{username}")]
        public ActionResult<Profile> PutProfile(string username, [FromBody] ProfileEditParams profile)
        {

            Player? p = LocalStorage<Player>.GetSampleList().FirstOrDefault((p) => { return (p.Username == username && !p.AccountDeleted); });
            if (p != null) {
            p.DisplayName = profile.DisplayName;
            p.Description = profile.Description;
            p.AvatarFilePath = CreateAvatar(profile.AvatarInBase64,p.Username);

                
                return Ok(new Profile(p)); 
            
            } else return NotFound();

        }

       

        [HttpGet("accounts/{id}")]

        public ActionResult<Account> GetAccount(long id){
            Player? p = LocalStorage<Player>.GetSampleList().FirstOrDefault(p => { return id==p.PlayerID && !p.AccountDeleted; });
            if (p == null) return NotFound();


            return Ok(new Account(p));

            }

        [HttpPut("accounts/{id}")]
        public ActionResult<Account> PutAccount(long id ,[FromBody]AccountEditParams accountParams)
        {
            Player? p = LocalStorage<Player>.GetSampleList().FirstOrDefault(p => { return id == p.PlayerID && !p.AccountDeleted; });
            if (p == null) return NotFound();
            if (accountParams.Username != null) p.Username = accountParams.Username;
            if (accountParams.Password != null) p.Password = accountParams.Password;

            




            return Ok(new Account(p));
        }

        [HttpDelete("accounts/{id}")]

        public ActionResult DeleteAccount(long id) {

            Player? p = LocalStorage<Player>.GetSampleList().FirstOrDefault((p) => { return (p.PlayerID == id && !p.AccountDeleted); });
            if (p != null)
            { p.AccountDeleted = true; return NoContent(); }
            else return NotFound();

            }


        [HttpPost("register")]

        public ActionResult<LoginAnswer> Register([FromBody]LoginParams registerRequest)
        {
            if (LocalStorage<Player>.GetSampleList().Any(p => { return p.Username == registerRequest.Username; }))
                return BadRequest("Username must be unique.");
            Player p = new Player();
            p.Username = registerRequest.Username;
            p.Password = registerRequest.Password;
            p.MatchmakingRanking = 0;
            p.DisplayName = p.Username;
            p.Description = $"Hi, I'm {p.DisplayName}";
            p.AccountDeleted = false;
            p.AvatarFilePath = "";
            p.PlayerID = LocalStorage<Player>.GetSampleList().Max(p => p.PlayerID)+1;
            LocalStorage<Player>.AddToSampleList(p);
            return Ok(new LoginAnswer(p));
            

        }
         
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginParams loginRequest)
        {
            Player? p  = LocalStorage<Player>.GetSampleList().FirstOrDefault(p => { return p.Username == loginRequest.Username && p.Password == loginRequest.Password && !p.AccountDeleted; }) ;
            if (p == null) return NotFound();


            return Ok(new LoginAnswer(p));
        }


        private string CreateAvatar(string imageInBase64, string username)
        {
            Guid imgGuid = Guid.NewGuid();
            try { 
                
            BinaryWriter bw = new (System.IO.File.Create($"." + Path.DirectorySeparatorChar + "ProfilePictures" + Path.DirectorySeparatorChar + username + imgGuid+ ".png"));
            bw.Write(Convert.FromBase64String(imageInBase64));
                bw.Close();
            }catch(Exception e) { return ""; }

            return username + imgGuid;
        }
    }
}
