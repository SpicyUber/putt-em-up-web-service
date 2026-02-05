
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
 
using Domain;
using Putt_Em_Up_Portal.Testing;
using System.Diagnostics;

namespace Putt_Em_Up_Portal.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string fromPlayerID, string toPlayerID, string content)
        {

            long from = long.Parse(fromPlayerID);
            long to = long.Parse(toPlayerID);

            Debug.WriteLine($"{fromPlayerID},{toPlayerID},{content}");
            Message message = new() { FromPlayerID = from, ToPlayerID = to, Content = content, Reported = false, SentTimestamp = DateTime.Now };
            LocalStorage<Message>.AddToSampleList(message);

            await Clients.All.SendAsync("RefreshChat");


        }
    }
}
