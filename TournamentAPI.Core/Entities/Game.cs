using Microsoft.EntityFrameworkCore;

namespace TournamentAPI.Core.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Game
    {
        public int Id { get; set; }
       public string ?Title { get; set; }
       public DateTime Time { get; set; }
       public int TournamentId { get; set; }

    }
}
