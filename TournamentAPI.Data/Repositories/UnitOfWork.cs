using System;
using System.Threading.Tasks;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;


namespace TournamentAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentAPIApiContext _context;
        public ITournamentRepository TournamentRepository { get; }
        public IGameRepository GameRepository { get; }

        public UnitOfWork(TournamentAPIApiContext context, ITournamentRepository tournamentRepository, IGameRepository gameRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            TournamentRepository = tournamentRepository ?? throw new ArgumentNullException(nameof(tournamentRepository));
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
