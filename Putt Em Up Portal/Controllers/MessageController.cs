using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.DTOs;
using Putt_Em_Up_Portal.Models;
using Putt_Em_Up_Portal.Testing;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet]
       public ActionResult<Message> Get([FromQuery]long fromPlayerID, [FromQuery]long toPlayerID, [FromQuery]DateTime sentTimestamp )
        {
           Message? message= LocalStorage<Message>.GetSampleList().FirstOrDefault((m) => { return m.FromPlayerID == fromPlayerID && m.SentTimestamp == sentTimestamp && m.ToPlayerID == toPlayerID; });
            if (message == null) { return NotFound(); }
            return Ok(message);
        }

        [HttpDelete]

      public ActionResult<Message> Delete(long fromPlayerID, long toPlayerID, DateTime sentTimestamp) {
            List < Message > list = LocalStorage<Message>.GetSampleList().ToList();
            int num = list.RemoveAll(m => { return m.FromPlayerID == fromPlayerID && m.SentTimestamp == sentTimestamp && m.ToPlayerID == toPlayerID; });
            LocalStorage<Message>.SetSampleList(list);
            if (num == 0) { return NotFound(); } else return NoContent();
        }

        [HttpPost]

       public ActionResult<Message> Post([FromBody]MessagePostParams messageParams) {
            Message message = new() { FromPlayerID = messageParams.FromPlayerID, ToPlayerID = messageParams.ToPlayerID, Content = "Message is being processed...", Reported = false, SentTimestamp = DateTime.Now };
            LocalStorage<Message>.AddToSampleList(message);
            return Ok(message); }

        [HttpPut]

        public ActionResult<Message> Put(long fromPlayerID, long toPlayerID, DateTime sentTimestamp,[FromBody]MessageEditParams messageParams)
        {

            Message? message = LocalStorage<Message>.GetSampleList().FirstOrDefault((m) => { return m.FromPlayerID == fromPlayerID && m.SentTimestamp == sentTimestamp && m.ToPlayerID == toPlayerID; });
            if (message == null) { return NotFound(); }
            if(messageParams.Content!=null) message.Content = messageParams.Content;
            if (messageParams.Reported != null) message.Reported = (bool)messageParams.Reported;
            return Ok(message);


        }
    }
}
