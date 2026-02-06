using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Putt_Em_Up_Portal.Testing;
using System.Dynamic;
using System.Linq;
using Application.DTOs;
using System.Threading.Tasks;
using MediatR;
using Application.Match.Queries;
using Application.Match.Commands;

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
        public async Task<ActionResult<MatchPreviewPage>> Search([FromQuery] SearchMatchesQuery searchParams)
        {

            MatchPreviewPage matchPage = await mediator.Send(searchParams);
            return Ok(matchPage);
          
        }

     




        [HttpGet("{id}")]

        public async Task<ActionResult<MatchPreview>> Get(long id)
        {


            MatchPreview? matchPreview = await mediator.Send(new FindMatchByIdQuery() { Id=id});
            if (matchPreview == null) return NotFound($"Match with id {id} not found.");
             
            return Ok(matchPreview);
        }

        [HttpPost()]

        public async Task<ActionResult<Match>> Post()
        {
            Match match =await mediator.Send(new CreateEmptyMatchCommand());
            return Ok(match);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Match>> Delete(long id) {

            bool success = await mediator.Send(new DeleteMatchCommand() { Id = id });
            return (success) ? NoContent() : NotFound("Could not find Id.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Match>> Put(long id, [FromBody] bool cancelled)
        {

            Match match = await mediator.Send(new EditMatchCommand() { Id = id, Cancelled=cancelled });
            return (match!=null) ? Ok(match) : NotFound("Could not find Id.");

        }

        






    }
}
