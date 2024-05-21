using System;
using System.Linq;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public static class SeedData
    {
        public static void Initialize(TournamentAPIApiContext context)
        {
            if (!context.Tournament.Any()) // Ändra från Tournaments till Tournament här
            {
                // Skapa några turneringar med tillhörande matcher
                var tournaments = new[]
                {
                    new Tournament
                    {
                        Title = "Turnering 1",
                        StartDate = DateTime.Now.AddDays(7),
                        Games = new[]
                        {
                            new Game { Title = "Match 1" },
                            new Game { Title = "Match 2" },
                            new Game { Title = "Match 3" }
                        }
                    },
                    new Tournament
                    {
                        Title = "Turnering 2",
                        StartDate = DateTime.Now.AddDays(14),
                        Games = new[]
                        {
                            new Game { Title = "Match 4" },
                            new Game { Title = "Match 5" }
                        }
                    }
                    
                };

                context.Tournament.AddRange(tournaments); 
                context.SaveChanges();
            }
        }
    }
}
