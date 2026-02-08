using Application.DTOs;
using Application.MatchPerformance.Commands;
using Application.MatchPerformance.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.Testing;
using System.Threading.Tasks;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/performances")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class MatchPerformanceController : ControllerBase
    {
        private readonly IMediator mediator;

        public MatchPerformanceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<MatchPerformance>> Get([FromQuery] FindMatchPerformanceQuery findMatchPerformanceRequest) {
        
        MatchPerformance? mp = await mediator.Send(findMatchPerformanceRequest);
            if (mp != null) return Ok(mp); else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<MatchPerformance>> Post([FromBody] CreateEmptyMatchPerformanceCommand createMatchPerformanceParams)
        {
           MatchPerformance matchPerformance = await mediator.Send(createMatchPerformanceParams);

            if (matchPerformance != null) return Ok(matchPerformance);
            return NotFound("Match/Player Id does not exist or match has reached matchPerformance quota.");
        }

        [HttpPut]

        public async Task<ActionResult<MatchPerformance>> Put(long playerID, long matchID,[FromBody] MatchPerformanceEditParams mpParams)
        {
            MatchPerformance? mp = await mediator.Send(new EditMatchPerformanceCommand() { PlayerID=playerID,MatchID=matchID,EditParams=mpParams});
            if(mp==null) return NotFound("Could not find matchPerformance.");

            
           
            return Ok(mp);



        }

        [HttpDelete]

        public async Task<ActionResult> Delete([FromQuery] DeleteMatchPerformanceCommand deleteMatchPerformanceRequest)
        {
            bool success = await mediator.Send(deleteMatchPerformanceRequest);
            if (!success) { return NotFound("MatchPerformance was not found."); } else return NoContent();


        }
    }
}
