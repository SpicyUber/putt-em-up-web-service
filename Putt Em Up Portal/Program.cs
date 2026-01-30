
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Identity;
using Putt_Em_Up_Portal.Hubs;
using Putt_Em_Up_Portal.Middleware;
using Putt_Em_Up_Portal.Models;
using Putt_Em_Up_Portal.Testing;
using Putt_Em_Up_Portal.Validators;
using SharpGrip.FluentValidation.AutoValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text.Json.Serialization;

namespace Putt_Em_Up_Portal
{
    public class Program
    {

        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
            builder.Services.AddValidatorsFromAssemblyContaining<MatchSearchParamsValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ProfileEditParamsValidator>();
            builder.Services.AddFluentValidationAutoValidation();
           

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            var app = builder.Build();

         app.UseCors(config => { config.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();
            app.MapHub<MessageHub>("/messageHub");
            InitLocalStorage();

            app.Run();
        }

       private static void InitLocalStorage()
        {
            List <Match> matches = new();
            matches.Add(new Match() { MatchID = 0, Cancelled = false, StartDate = new(2012, 3, 3) });
            matches.Add(new Match() { MatchID = 1, Cancelled = false, StartDate = new(2012, 3, 3,13,20,20) });
            matches.Add(new Match() { MatchID = 2, Cancelled = false, StartDate = new(2022, 7, 2,12,22,33) });
            matches.Add(new Match() { MatchID = 3, Cancelled = false, StartDate = new(2023, 7, 2, 12, 22, 33) });
            matches.Add(new Match() { MatchID = 4, Cancelled = false, StartDate = new(2023, 3, 2, 12, 22, 33) });
            LocalStorage<Match>.SetSampleList(matches);
            List<Player> players = new();
            players.Add(new() {PlayerID=0,Username="ila",Password="admin!",MatchmakingRanking=0,AccountDeleted=false,DisplayName="Aleksandar",Description="My Description.",AvatarFilePath="default.png" });
            players.Add(new() { PlayerID = 1, Username = "magnus", Password = "123456", MatchmakingRanking = 200, AccountDeleted = false, DisplayName = "Magnus", Description = "I am very good at this game.", AvatarFilePath = "screaming.png" });
            players.Add(new() { PlayerID = 2, Username = "bob", Password = "123abc", MatchmakingRanking = -200, AccountDeleted = false, DisplayName = "Bob", Description = "I am very bad at this game.", AvatarFilePath = "default.png" });
            LocalStorage<Player>.SetSampleList(players);
            List<MatchPerformance> mps = new();
            mps.Add(new() { PlayerID =0, MatchID=0, WonMatch=false, MMRDelta=-100, FinalScore=0 });
            mps.Add(new() { PlayerID = 1, MatchID=0, WonMatch = true, MMRDelta = 100, FinalScore = 3 });
            mps.Add(new() { PlayerID = 2, MatchID = 1, WonMatch = false, MMRDelta = -100, FinalScore = 0 });
            mps.Add(new() { PlayerID = 1, MatchID = 1, WonMatch = true, MMRDelta = 100, FinalScore = 3 });
            mps.Add(new() { PlayerID = 2, MatchID = 2, WonMatch = false, MMRDelta = -100, FinalScore = 0 });
            mps.Add(new() { PlayerID = 0, MatchID = 2, WonMatch = true, MMRDelta = 100, FinalScore = 3 });
            mps.Add(new() { PlayerID = 0, MatchID = 3, WonMatch = true, MMRDelta = 100, FinalScore = 3 });
            mps.Add(new() { PlayerID = 1, MatchID = 3, WonMatch = false, MMRDelta = -100, FinalScore = 0 });
            mps.Add(new() { PlayerID = 0, MatchID = 4, WonMatch = false, MMRDelta = -100, FinalScore = 0 });
            mps.Add(new() { PlayerID = 1, MatchID = 4, WonMatch = true, MMRDelta = 100, FinalScore = 3 });
            LocalStorage<MatchPerformance>.SetSampleList(mps);
            List<Message> messages = new();
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2012,3,3, 1, 33, 12), Reported = false, Content = "ez" });
            messages.Add(new Message() { FromPlayerID = 0, ToPlayerID = 1, SentTimestamp = new(2012,3,3, 1, 34, 22), Reported = false, Content = ":(" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2022, 3, 3, 1, 33, 12), Reported = false, Content = "it's been 10 years, just reminding u it was ez" });
            messages.Add(new Message() { FromPlayerID = 0, ToPlayerID = 1, SentTimestamp = new(2022, 3, 3, 1, 34, 22), Reported = false, Content = "How do I block you??" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 1, 33, 12), Reported = false, Content = "hey man just letting you know im sorry for being mean to u, hope u forgive me" });
            messages.Add(new Message() { FromPlayerID = 0, ToPlayerID = 1, SentTimestamp = new(2026, 1, 29, 1, 34, 22), Reported = false, Content = "really?" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 2, 33, 12), Reported = false, Content = "nope, ez" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 3, 33, 12), Reported = false, Content = "hahahaha" });
            messages.Add(new Message() { FromPlayerID = 0, ToPlayerID = 1, SentTimestamp = new(2026, 1, 29, 3, 34, 22), Reported = false, Content = "Dimitrica Tucovica 23, 4. sprat, stan 21" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 3, 35, 12), Reported = false, Content = "..." });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 3, 35, 24), Reported = false, Content = "i apologize" });
            messages.Add(new Message() { FromPlayerID = 0, ToPlayerID = 1, SentTimestamp = new(2026, 1, 29, 4, 34, 22), Reported = false, Content = "Too late now :)" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 4, 35, 12), Reported = false, Content = "pls no" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 4, 35, 25), Reported = false, Content = "pls no man" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 4, 35, 26), Reported = false, Content = "its just a game dude" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 0, SentTimestamp = new(2026, 1, 29, 4, 35, 28), Reported = false, Content = "dude?" });
            messages.Add(new Message() { FromPlayerID = 0, ToPlayerID = 1, SentTimestamp = new(2026, 1, 29, 4, 54, 22), Reported = false, Content = "Look outside bro :D" });
            messages.Add(new Message() { FromPlayerID = 0, ToPlayerID = 2, SentTimestamp = new(2022,7,2, 10, 11, 31), Reported = false, Content = "friendlies today?" });
            messages.Add(new Message() { FromPlayerID = 2, ToPlayerID = 0, SentTimestamp = new(2022, 7, 2, 10, 11, 31), Reported = false, Content = "maybe" });
            messages.Add(new Message() { FromPlayerID = 1, ToPlayerID = 2, SentTimestamp = new(2012, 3, 3, 14, 33, 12), Reported = false, Content = "ez" });
            messages.Add(new Message() { FromPlayerID = 2, ToPlayerID = 1, SentTimestamp = new(2012, 3, 3, 14, 34, 22), Reported = true, Content = "wow" });
            LocalStorage<Message>.SetSampleList(messages);
        }
    }
}
