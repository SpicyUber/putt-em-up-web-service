using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.DTOs;
using Domain;
using Putt_Em_Up_Portal.Testing;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/performances")]
    [ApiController]
    public class MatchPerformanceController : ControllerBase
    {

        [HttpGet]
        public ActionResult<MatchPerformance> Get(long playerID, long matchID) {
        
        MatchPerformance? mp = LocalStorage<MatchPerformance>.GetSampleList().FirstOrDefault((mp) => mp.MatchID == matchID && mp.PlayerID==playerID);
            if (mp != null) return Ok(mp); else return NotFound();
        }

        [HttpPost]
        public ActionResult<MatchPerformance> Post([FromBody] MatchPerformancePostParams mpParams)
        {
            MatchPerformance mp = new() { MatchID=mpParams.MatchID, PlayerID=mpParams.PlayerID, FinalScore=0, MMRDelta=0, WonMatch=false};
            LocalStorage<MatchPerformance>.AddToSampleList(mp);
            return Ok(mp);
        }

        [HttpPut]

        public ActionResult<MatchPerformance> Put(long playerID, long matchID,[FromBody] MatchPerformanceEditParams mpParams)
        {
            MatchPerformance? mp = LocalStorage<MatchPerformance>.GetSampleList().FirstOrDefault((mp) => mp.MatchID == matchID && mp.PlayerID == playerID);
            if(mp==null) return NotFound();

            mp.MMRDelta = mpParams.MMRDelta;
            mp.WonMatch = mpParams.WonMatch;
            mp.FinalScore = mpParams.FinalScore;
           
            return Ok(mp);



        }

        [HttpDelete]

        public ActionResult Delete(long playerID, long matchID)
        {
            List<MatchPerformance> list = LocalStorage<MatchPerformance>.GetSampleList().ToList();
            int num = list.RemoveAll(m => { return m.PlayerID == playerID && m.MatchID == matchID; });
            LocalStorage<MatchPerformance>.SetSampleList(list);
            if (num == 0) { return NotFound(); } else return NoContent();


        }
    }
}
