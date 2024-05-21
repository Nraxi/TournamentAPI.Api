using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Data.Repositories
{
     class GameRepository : IGameRepository
    {
        private readonly TournamentAPIApiContext _context;

        public GameRepository(TournamentAPIApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public async Task<Game?> GetAsync(int id)
        {
            return await _context.Game.FindAsync(id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Game.AnyAsync(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Game.Add(game);
        }

        public void Update(Game game)
        {
            _context.Game.Update(game);
        }

        public void Remove(Game game)
        {
            _context.Game.Remove(game);
        }
    }
}
