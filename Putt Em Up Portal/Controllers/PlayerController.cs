

using Application.DTOs;
using Application.Player.Commands;
using Application.Player.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.Testing;
using System.Threading.Tasks;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlayerController(IMediator mediator) {
            this.mediator = mediator;
        }
        
        [HttpGet("profiles/{username}")]
         public async Task<ActionResult<Profile>> GetProfile(string username)
        {

           Profile p = await mediator.Send(new FindProfileByUsernameQuery { Username = username });
            if (p != null) return Ok(p); else return NotFound("Profile with this username not found.");
            
        }



        [HttpGet("profiles/search")]

        public async Task<ActionResult<LeaderboardPage>> Search([FromQuery]SearchPlayersQuery playerParameters)
        {
            LeaderboardPage lp = await mediator.Send(playerParameters);
            return Ok(lp);

        }



        [HttpPut("profiles/{username}")]
        public async Task<ActionResult<Profile>> PutProfile(string username, [FromBody] ProfileEditParams profile)
        {
            if(User?.Identity?.Name!=username.ToUpper())return Unauthorized();

            Profile p = await mediator.Send(new EditProfileCommand() { Profile = profile, Username = username });

                if(p != null) return Ok(p);
                  else return NotFound("Profile with this username not found.");

        }

       

        [HttpGet("accounts/{id}")]

        public async Task<ActionResult<Account>> GetAccount(long id){
            Account account = await mediator.Send(new FindAccountByIdQuery() { Id = id });


            if(account==null)return NotFound();
            return Ok(account);

            }

        [HttpPut("accounts/{id}")]
        public async Task<ActionResult<Account>> PutAccount(long id ,[FromBody]AccountEditParams accountParams)
        {
            /* Player? p = LocalStorage<Player>.GetSampleList().FirstOrDefault(p => { return id == p.PlayerID && !p.AccountDeleted; });
             if (p == null) return NotFound();
             if (accountParams.Username != null) p.Username = accountParams.Username;
             if (accountParams.Password != null) p.Password = accountParams.Password;






             return Ok(new Account(p));*/
            Account a = await mediator.Send(new EditAccountCommand() { Id = id, AccountParams = accountParams });
            if(a==null) return NotFound();
            return Ok(a);
        }

        [HttpDelete("accounts/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteAccount(long id) {

           bool success =  await mediator.Send(new DeleteAccountCommand() { Id = id});
            return (success)?NoContent(): NotFound("Could not find Id.");
            }


        [HttpPost("register")]

        public async Task<ActionResult<LoginAnswer>> Register([FromBody]RegisterCommand registerRequest)
        {
            /* if (LocalStorage<Player>.GetSampleList().Any(p => { return p.Username == registerRequest.Username; }))
                 return BadRequest("Username must be unique.");
             Player p = new Player();
             p.Username = registerRequest.Username;
             p.Password = registerRequest.Password;

             p.DisplayName = p.Username;
             p.Description = $"Hi, I'm {p.DisplayName}";
             p.AccountDeleted = false;
             p.AvatarFilePath = "";
             p.PlayerID = LocalStorage<Player>.GetSampleList().Max(p => p.PlayerID)+1;
             LocalStorage<Player>.AddToSampleList(p);
             return Ok(new LoginAnswer(p));
             */
            LoginAnswer answer = await mediator.Send(registerRequest);
            if(answer==null) return BadRequest("Username must be unique.");
            return Ok(answer);
        }
         
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginCommand loginRequest)
        {
           LoginAnswer answer= await mediator.Send(loginRequest);
            if (answer == null) return NotFound("Username and / or Password are incorrect.");

            return Ok(answer);
        }


       
    }
}
