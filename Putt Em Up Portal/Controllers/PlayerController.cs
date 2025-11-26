
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.DTOs;
using Putt_Em_Up_Portal.Models;
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

        [HttpPut("profiles/{username}")]
        public ActionResult<Profile> PutProfile(string username, [FromBody] ProfileEditParams profile)
        {

            Player? p = LocalStorage<Player>.GetSampleList().FirstOrDefault((p) => { return (p.Username == username && !p.AccountDeleted); });
            if (p != null) {
            p.DisplayName = profile.DisplayName;
            p.Description = profile.Description;
            p.AvatarFilePath = profile.AvatarFilePath;

                
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
            p.AvatarFilePath = "default.png";
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
    }
}
