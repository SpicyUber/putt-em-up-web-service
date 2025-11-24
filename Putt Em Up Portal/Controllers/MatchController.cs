using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.Models;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        [HttpGet(Name ="GetMatch")]
      public IEnumerable<Match> Get(int id)
        {
           return Match.GetSampleList().Where((Match m) => { return m.MatchID == id; }).ToList<Match>();

        }

    }
}
