using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Core.Entities
{
    // [Keyless]
    [PrimaryKey(nameof(Id))]
    public class Tournament
    {
        public int Id { get; set; } 
        public string ?Title { get; set; } 

        public DateTime StartDate { get; set; } 
        public ICollection<Game> ?Games { get; set; } 
    }
}
