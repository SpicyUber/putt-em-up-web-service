using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.DTOs;
using Putt_Em_Up_Portal.Models;
using Putt_Em_Up_Portal.Testing;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        [HttpGet(Name = "Search")]
        public ActionResult<Match> Search([FromQuery] MatchSearchParams searchParams)
        {


            List<Match> matches = new List<Match>();

            switch (searchParams.Mode)
            {

                case SearchMode.BeforeIncludingDate:
                    matches = FindMatchesBeforeDate(searchParams.StartDate);
                    break;
                case SearchMode.AfterIncludingDate:
                    matches = FindMatchesAfterDate(searchParams.StartDate);
                    break;
                case SearchMode.DuringDate:
                    matches = FindMatchesDuringDate(searchParams.StartDate);
                    break;
            }

            if (matches.Count <= 0) return NotFound($"Match {searchParams.Mode} {searchParams.StartDate.ToString("MM/dd/yyyy")} could not be found.");

            return Ok(matches);
        }

        private List<Match> FindMatchesAfterDate(DateTime startDate)
        {
            return LocalStorage<Match>.GetSampleList().Where<Match>((Match m) => { return m.StartDate.Date >= startDate.Date; }).ToList();
        }

        private List<Match> FindMatchesDuringDate(DateTime startDate)
        {
            return LocalStorage<Match>.GetSampleList().Where<Match>((Match m) => { return m.StartDate.Date.Equals(startDate.Date); }).ToList();
        }

        private List<Match> FindMatchesBeforeDate(DateTime startDate)
        {
            return LocalStorage<Match>.GetSampleList().Where<Match>((Match m) => { return m.StartDate.Date <= startDate.Date; }).ToList();
        }




        [HttpGet("{id}")]

        public ActionResult<Match> Get(long id)
        {
            Match? match = LocalStorage<Match>.GetSampleList().FirstOrDefault((Match m) => { return m.MatchID == id; });
            if (match == null) {return NotFound($"Match with id {id} not found."); }
            return Ok(match);
        }

        [HttpPost()]

        public ActionResult<Match> Post()
        {
            Match match = null;

            if (LocalStorage<Match>.GetSampleList().Count() == 0) 
            { 
                match = new() { MatchID = 0, Cancelled = true, StartDate = DateTime.Now.Date }; 
            }
            else
            {
                match = new() { MatchID = LocalStorage<Match>.GetSampleList().Max((Match m) => { return m.MatchID; }) + 1, Cancelled = true, StartDate = DateTime.Now };
            }
            LocalStorage<Match>.AddToSampleList(match);
            
            return Ok(match);
        }

        [HttpDelete("{id}")]

        public ActionResult<Match> Delete(long id) {

            List<Match> list = LocalStorage<Match>.GetSampleList().ToList<Match>();
            int num = list.RemoveAll((Match m) => { return m.MatchID == id; });
            LocalStorage<Match>.SetSampleList(list);
            if (num == 0) { return NotFound($"Match with id {id} not found."); }
            return NoContent();

        }

        [HttpPut("{id}")]
        public ActionResult<Match> Put(long id, [FromBody] bool cancelled)
        {
            Match? match = LocalStorage<Match>.GetSampleList().FirstOrDefault((Match m) => { return m.MatchID == id; });
            if (match == null) { return NotFound($"Match with id {id} not found."); }
            match.Cancelled = cancelled;
            return Ok(match);

        }

        






    }
}
