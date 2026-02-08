using Application.DTOs;
using Application.Message.Commands;
using Application.Message.Queries;
using Domain;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Putt_Em_Up_Portal.Hubs;
using Putt_Em_Up_Portal.Testing;
using System.Threading.Tasks;

namespace Putt_Em_Up_Portal.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMediator mediator;

        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("messages")]
        public async Task<ActionResult<Message>> Get([FromQuery]FindMessageQuery findMessageRequest )
        {
            Message message = await mediator.Send(findMessageRequest);
            if (message == null) { return NotFound(); }
            
            return Ok(message);
        }

        [HttpGet]
        [Route("chats")]
        public async Task<ActionResult<List<Message>>> GetChat([FromQuery] GetChatQuery getChatRequest)
        {
            List<Message> messages = await mediator.Send(getChatRequest);

            if (messages.Count == 0) { return NotFound(); }

            return Ok(messages);
        }

        [HttpGet]
        [Route("messages/recent")]
        public async Task<ActionResult<List<Message>>> GetAllChat([FromQuery] GetAllRecentMessagesQuery getAllRecentMessagesRequest)
        {
            List<Message> messages = await mediator.Send(getAllRecentMessagesRequest);
               if (messages.Count == 0) { return NotFound(); }

            return Ok(messages);
        }

        [Route("messages")]
        [Authorize(Roles = "admin")]
        [HttpDelete]

      public async Task<ActionResult> Delete(DeleteMessageCommand deleteMessageRequest) {

            bool success = await mediator.Send(deleteMessageRequest);
             
            if (!success) { return NotFound(); }
            
            return NoContent(); 
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("messages")]
        public async Task<ActionResult<Message>> Post([FromBody]CreateEmptyMessageCommand createMessageRequest) {
            Message message = await mediator.Send(createMessageRequest);
               
           if(message == null) { return BadRequest("One of the IDs is not valid."); }
            return Ok(message); }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("messages")]
        public async Task<ActionResult<Message>> Put(long fromPlayerID, long toPlayerID, DateTime sentTimestamp,[FromBody]MessageEditParams messageParams)
        {

            Message message = await mediator.Send(new EditMessageCommand() { FromPlayerID = fromPlayerID, ToPlayerID = toPlayerID, EditParams = messageParams });
            if (message == null) { return NotFound("Message was not found. Possible invalid IDs and/or timestamp"); }
            return Ok(message);


        }
    }
}
