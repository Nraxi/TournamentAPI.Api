using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace TournamentAPI.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentAPIApiContext _context;

        public TournamentRepository(TournamentAPIApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _context.Tournament.ToListAsync();
        }

        public async Task<Tournament?> GetAsync(int id)
        {
            return await _context.Tournament.FindAsync(id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournament.AnyAsync(t => t.Id == id);
        }

        public void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
            _context.SaveChanges(); 
        }

        public void  Update(Tournament tournament)
        {
            _context.Tournament.Update(tournament);
             _context.SaveChangesAsync(); 
        }


        
        public void  Remove(Tournament tournament)
        {
            _context.Tournament.Remove(tournament);
             _context.SaveChangesAsync(); 
        }
    }
}
