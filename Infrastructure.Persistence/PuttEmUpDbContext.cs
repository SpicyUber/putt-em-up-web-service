using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Persistence
{
    public class PuttEmUpDbContext : IdentityDbContext<Player,IdentityRole<long>,long>
    {
        public DbSet<Player> Players {get;set;}
        public DbSet<Match> Matches { get;set;}
        public DbSet<MatchPerformance> MatchPerformances { get;set;}

        public DbSet<Message> Messages { get;set;}

        private readonly IPasswordHasher<Player> hasher;
        public PuttEmUpDbContext(DbContextOptions options, IPasswordHasher<Player> hasher) : base(options) { this.hasher = hasher; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Player>().HasKey((Player p) => p.Id);

          //  modelBuilder.Entity<Player>().HasIndex((Player p) => p.Username).IsUnique();

            


            modelBuilder.Entity<MatchPerformance>().HasKey((MatchPerformance p) => new { p.PlayerID, p.MatchID });

            modelBuilder.Entity<MatchPerformance>().HasOne<Player>().WithMany().HasForeignKey((MatchPerformance mp) => mp.PlayerID);
            
            modelBuilder.Entity<MatchPerformance>().HasOne<Match>().WithMany().HasForeignKey((MatchPerformance mp) => mp.MatchID);


            modelBuilder.Entity<Match>().HasKey((Match m) => m.MatchID);


            modelBuilder.Entity<Message>().HasOne<Player>().WithMany().HasForeignKey((Message m)=>m.FromPlayerID).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>().HasOne<Player>().WithMany().HasForeignKey((Message m) => m.ToPlayerID).OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<Message>().HasKey((Message m) => new {m.FromPlayerID , m.ToPlayerID, m.SentTimestamp });
            modelBuilder.Entity<IdentityRole<long>>().HasData(new IdentityRole<long>() { Id = 1, Name = "user", NormalizedName = "USER" }, new IdentityRole<long>() {Id=2 , Name="admin", NormalizedName="ADMIN" });
            Player p1 = new Player()
            {
                Id = 1,
                UserName = "admin",
                


                AccountDeleted = false,
                DisplayName = "Aleksandar",
                NormalizedUserName = "ADMIN",
                Description = "My Description.",
                AvatarFilePath = ""
               
            }; 
            p1.PasswordHash = hasher.HashPassword(p1, "admin!");

            Player p2 = new Player()
            {
                Id = 2,
                UserName = "magnus",
                NormalizedUserName = "MAGNUS",


                AccountDeleted = false,
                DisplayName = "Magnus",
                Description = "I am very good at this game.",
                AvatarFilePath = "magnus"
            }; 
            p2.PasswordHash = hasher.HashPassword(p2, "admin!");

            Player p3 = new Player
            {
                Id = 3,
                UserName = "bob",


                AccountDeleted = false,
                DisplayName = "Bob",
                NormalizedUserName = "BOB",
                Description = "I am very bad at this game.",
                AvatarFilePath = ""
            };
             p3.PasswordHash = hasher.HashPassword(p3, "admin!");
            modelBuilder.Entity<Player>().HasData(
               p1,p2,p3
            );

            modelBuilder.Entity<IdentityUserRole<long>>().HasData(
           new IdentityUserRole<long>
           {
               RoleId = 2,
               UserId = 1
           },
            new IdentityUserRole<long>
            {
                RoleId = 1,
                UserId = 2
            },
             new IdentityUserRole<long>
             {
                 RoleId = 1,
                 UserId = 3
             }
       );


            modelBuilder.Entity<Match>().HasData(
                
                new Match { MatchID = 1, Cancelled = false, StartDate = new DateTime(2012, 3, 3, 13, 20, 20) },
                new Match { MatchID = 2, Cancelled = false, StartDate = new DateTime(2022, 7, 2, 12, 22, 33) },
                new Match { MatchID = 3, Cancelled = false, StartDate = new DateTime(2023, 7, 2, 12, 22, 33) },
                new Match { MatchID = 4, Cancelled = false, StartDate = new DateTime(2023, 3, 2, 12, 22, 33) },
                new Match { MatchID = 5, Cancelled = false, StartDate = new DateTime(2024, 12, 11, 12, 22, 33) }
            );

            
            modelBuilder.Entity<MatchPerformance>().HasData(
                 
                new MatchPerformance { PlayerID = 3, MatchID = 1, WonMatch = false, MMRDelta = -100, FinalScore = 0 },
                new MatchPerformance { PlayerID = 2, MatchID = 1, WonMatch = true, MMRDelta = 100, FinalScore = 3 },
                new MatchPerformance { PlayerID = 3, MatchID = 2, WonMatch = false, MMRDelta = -100, FinalScore = 0 },
                new MatchPerformance { PlayerID = 1, MatchID = 2, WonMatch = true, MMRDelta = 100, FinalScore = 3 },
                new MatchPerformance { PlayerID = 1, MatchID = 3, WonMatch = true, MMRDelta = 100, FinalScore = 3 },
                new MatchPerformance { PlayerID = 2, MatchID = 3, WonMatch = false, MMRDelta = -100, FinalScore = 0 },
                new MatchPerformance { PlayerID = 1, MatchID = 4, WonMatch = false, MMRDelta = -100, FinalScore = 0 },
                new MatchPerformance { PlayerID = 2, MatchID = 4, WonMatch = true, MMRDelta = 100, FinalScore = 3 },
                new MatchPerformance { PlayerID = 2, MatchID = 5, WonMatch = true, MMRDelta = 100, FinalScore = 3 },
                new MatchPerformance { PlayerID = 3, MatchID = 5, WonMatch = false, MMRDelta = -100, FinalScore = 0 }
            );

            
            modelBuilder.Entity<Message>().HasData(
                new Message { FromPlayerID = 1, ToPlayerID = 2, SentTimestamp = new DateTime(2012, 3, 3, 1, 33, 12), Reported = false, Content = "ez" },
                new Message { FromPlayerID = 2, ToPlayerID = 1, SentTimestamp = new DateTime(2012, 3, 3, 1, 34, 22), Reported = false, Content = ":(" },
                new Message { FromPlayerID = 1, ToPlayerID = 3, SentTimestamp = new DateTime(2022, 3, 3, 1, 33, 12), Reported = false, Content = "it's been 10 years, lets play?" },
                new Message { FromPlayerID = 2, ToPlayerID = 1, SentTimestamp = new DateTime(2022, 3, 3, 1, 34, 22), Reported = false, Content = "How do I block you??" },
                new Message { FromPlayerID = 2, ToPlayerID = 3, SentTimestamp = new DateTime(2022, 3, 3, 1, 33, 13), Reported = false, Content = "hi" },
                new Message { FromPlayerID = 1, ToPlayerID = 2, SentTimestamp = new DateTime(2022, 3, 3, 1, 34, 23), Reported = false, Content = "hello" }
            );


        }
       
        
    }
}
