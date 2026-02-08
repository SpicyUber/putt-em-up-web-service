
using Application.Message.Commands;
 
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Putt_Em_Up_Portal.Testing;
using System.Diagnostics;
using System.Security.Claims;

namespace Putt_Em_Up_Portal.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMediator mediator;
        public MessageHub(IMediator mediator) { this.mediator = mediator; }

        [Authorize]
        public async Task SendMessage(long fromPlayerID, long toPlayerID, string content)
        {

            string tokenUserIdClaimString = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long tokenUserId = -1;
            if(!long.TryParse(tokenUserIdClaimString,out tokenUserId) || tokenUserId!=fromPlayerID) return;


            Debug.WriteLine($"{fromPlayerID},{toPlayerID},{content}");
            Message newMessage = await mediator.Send(new CreateEmptyMessageCommand() { FromPlayerID = fromPlayerID, ToPlayerID = toPlayerID });
            if (newMessage == null) return;

            Message editedMessage = await mediator.Send(new EditMessageCommand() { FromPlayerID = newMessage.FromPlayerID, ToPlayerID = newMessage.ToPlayerID, SentTimestamp = newMessage.SentTimestamp, EditParams = new() { Content = content, Reported = newMessage.Reported } });

            await Clients.All.SendAsync("RefreshChat");


        }
    }
}
