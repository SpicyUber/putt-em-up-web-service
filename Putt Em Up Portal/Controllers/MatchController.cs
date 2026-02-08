using Application.DTOs;
using Application.Match.Commands;
using Application.Match.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.Testing;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMediator mediator;

        public MatchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "Search")]
        [Authorize]
        public async Task<ActionResult<MatchPreviewPage>> Search([FromQuery] SearchMatchesQuery searchParams)
        {
            if (User.IsInRole("admin")) { var r = new SearchMatchesIncludingCancelledQuery() {
                Mode = searchParams.Mode, PlayerID = searchParams.PlayerID, PageNumber = searchParams.PageNumber, PageSize = searchParams.PageSize, StartDate = searchParams.StartDate };
                MatchPreviewPage matchPage = await mediator.Send(r);
                return Ok(matchPage);
            }
            else { 

                MatchPreviewPage matchPage = await mediator.Send(searchParams);
            return Ok(matchPage);
            }


        }



       
         



        [HttpGet("{id}")]


        public async Task<ActionResult<MatchPreview>> Get(long id)
        {


            MatchPreview? matchPreview = await mediator.Send(new FindMatchByIdQuery() { Id=id});
            if (matchPreview == null) return NotFound($"Match with id {id} not found.");
             
            return Ok(matchPreview);
        }

        [HttpPost()]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Match>> Post()
        {
            Match match =await mediator.Send(new CreateEmptyMatchCommand());
            return Ok(match);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<Match>> Delete(long id) {

            bool success = await mediator.Send(new DeleteMatchCommand() { Id = id });
            return (success) ? NoContent() : NotFound("Could not find Id.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Match>> Put(long id, [FromBody] bool cancelled)
        {

            Match match = await mediator.Send(new EditMatchCommand() { Id = id, Cancelled=cancelled });
            return (match!=null) ? Ok(match) : NotFound("Could not find Id.");

        }

        






    }
}
