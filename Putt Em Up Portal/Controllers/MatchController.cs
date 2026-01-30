using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.DTOs;
using Putt_Em_Up_Portal.Models;
using Putt_Em_Up_Portal.Testing;
using System.Dynamic;
using System.Linq;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        [HttpGet(Name = "Search")]
        public ActionResult<MatchPreview> Search([FromQuery] MatchSearchParams searchParams)
        {


            List<MatchPreview> matches = new List<MatchPreview>();

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
            matches = matches.Where((match) => { if (match.MatchPerformances.Length < 2) return false; return match.MatchPerformances[0].Player.PlayerID == searchParams.PlayerID || match.MatchPerformances[1].Player.PlayerID == searchParams.PlayerID; }).ToList<MatchPreview>();
            if (matches.Count <= 0) return NotFound($"Match {searchParams.Mode} {searchParams.StartDate.ToString("MM/dd/yyyy")} could not be found.");
            if (searchParams.PageNumber != null && searchParams.PageSize != null)
                matches = matches.Skip((int)(searchParams.PageNumber) * (int)(searchParams.PageSize) - (int)(searchParams.PageSize)).Take((int)(searchParams.PageSize)).ToList<MatchPreview>() ;
            return Ok(matches);
        }

        private List<MatchPreview> FindMatchesAfterDate(DateTime startDate)
        {

            List<MatchPreview> matchPreviews = new List<MatchPreview>();
            LocalStorage<Match>.GetSampleList().ToList().ForEach((Match m) => { if (m.StartDate.Date >= startDate.Date && !m.Cancelled) { MatchPreview mp = CreateMatchPreview(m); if (mp != null) matchPreviews.Add(mp); } });
            return matchPreviews;



        }

        private MatchPreview CreateMatchPreview(Match match)
        {


            List<MatchPerformance> listMP = LocalStorage<MatchPerformance>.GetSampleList().Where(mp =>
            {
                return match.MatchID == mp.MatchID;

            }).ToList();

            if (listMP.Count < 2) return null;

            MatchPerformance mp1 = listMP[0];
            MatchPerformance mp2 = listMP[1];

            List<Player> playerList = LocalStorage<Player>.GetSampleList().Where((Player player) => { return (player.PlayerID == mp1.PlayerID || player.PlayerID == mp2.PlayerID); }).ToList();

            
            Player p1 = LocalStorage<Player>.GetSampleList().FirstOrDefault((Player player) => {
                return (player.PlayerID == mp1.PlayerID);
            });
            Player p2 = LocalStorage<Player>.GetSampleList().FirstOrDefault((Player player) => {
                return (player.PlayerID == mp2.PlayerID);
            });
            if (p1 == null || p2 == null) return null;

            return new(match, p1, p2, mp1, mp2);










        }
        private List<MatchPreview> FindMatchesDuringDate(DateTime startDate)
        {
            List<MatchPreview> matchPreviews = new List<MatchPreview>();
            LocalStorage<Match>.GetSampleList().ToList().ForEach((Match m) => { if (m.StartDate.Date.Equals(startDate.Date) && !m.Cancelled) { MatchPreview mp = CreateMatchPreview(m); if (mp != null) matchPreviews.Add(mp); } });
            return matchPreviews;


        }

        private List<MatchPreview> FindMatchesBeforeDate(DateTime startDate)
        {
            List<MatchPreview> matchPreviews = new List<MatchPreview>();
            LocalStorage<Match>.GetSampleList().ToList().ForEach((Match m) => { if (m.StartDate.Date <= startDate.Date && !m.Cancelled) { MatchPreview mp = CreateMatchPreview(m); if (mp != null) matchPreviews.Add(mp); } });
            return matchPreviews;
        }




        [HttpGet("{id}")]

        public ActionResult<MatchPreview> Get(long id)
        {


            Match? match = LocalStorage<Match>.GetSampleList().FirstOrDefault((Match m) => { return m.MatchID == id &&!m.Cancelled; });
            if (match == null) return NotFound($"Match with id {id} not found.");
            MatchPreview? matchPreview = CreateMatchPreview(match);
            if (matchPreview == null) {return NotFound($"Match with id {id} not found."); }
            return Ok(matchPreview);
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
