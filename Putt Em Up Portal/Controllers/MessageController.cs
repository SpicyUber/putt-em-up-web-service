using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.DTOs;
using Putt_Em_Up_Portal.Hubs;
using Domain;
using Putt_Em_Up_Portal.Testing;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MessageController : ControllerBase
    {


        
        [HttpGet]
        [Route("messages")]
        public ActionResult<Message> Get([FromQuery]long fromPlayerID, [FromQuery]long toPlayerID, [FromQuery]DateTime sentTimestamp )
        {
           Message? message= LocalStorage<Message>.GetSampleList().FirstOrDefault((m) => { return m.FromPlayerID == fromPlayerID && m.SentTimestamp == sentTimestamp && m.ToPlayerID == toPlayerID; });
            if (message == null) { return NotFound(); }
            
            return Ok(message);
        }

        [HttpGet]
        [Route("chats")]
        public ActionResult<List<Message>> GetChat([FromQuery] long firstPlayerID, [FromQuery] long secondPlayerID)
        {
            List<Message> messages = LocalStorage<Message>.GetSampleList().Where<Message>((Message m) => { return (m.FromPlayerID == firstPlayerID && secondPlayerID == m.ToPlayerID || m.FromPlayerID == secondPlayerID && firstPlayerID == m.ToPlayerID); }).OrderBy((Message m1) => m1.SentTimestamp).ToList();
            if (messages.Count == 0) { return NotFound(); }

            return Ok(messages);
        }

        [HttpGet]
        [Route("messages/recent")]
        public ActionResult<List<Message>> GetAllChat([FromQuery] long playerID)
        {
            List<Message> messages = LocalStorage<Message>.GetSampleList().Where<Message>((Message m) => { return (m.FromPlayerID == playerID || m.ToPlayerID == playerID )&& m.SentTimestamp.AddDays(7.0)>DateTime.Now; }).OrderByDescending((Message m1) => m1.SentTimestamp).ToList();
            if (messages.Count == 0) { return NotFound(); }

            return Ok(messages);
        }

        [Route("messages")]
        [HttpDelete]

      public ActionResult<Message> Delete(long fromPlayerID, long toPlayerID, DateTime sentTimestamp) {
            List < Message > list = LocalStorage<Message>.GetSampleList().ToList();
            int num = list.RemoveAll(m => { return m.FromPlayerID == fromPlayerID && m.SentTimestamp == sentTimestamp && m.ToPlayerID == toPlayerID; });
            LocalStorage<Message>.SetSampleList(list);
            if (num == 0) { return NotFound(); } else { 
                  return NoContent(); }
        }

        [HttpPost]
        [Route("messages")]
        public ActionResult<Message> Post([FromBody]MessagePostParams messageParams) {
            Message message = new() { FromPlayerID = messageParams.FromPlayerID, ToPlayerID = messageParams.ToPlayerID, Content = "Message is being processed...", Reported = false, SentTimestamp = DateTime.Now };
            LocalStorage<Message>.AddToSampleList(message);
           
            return Ok(message); }

        [HttpPut]
        [Route("messages")]
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
