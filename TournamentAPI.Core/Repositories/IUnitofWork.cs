﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Core.Repositories
{
    public interface IUnitofWork : IDisposable
    {
        ITournamentRepository TournamentRepository { get; }
        IGameRepository GameRepository { get; }

        Task CompleteAsync();

    }
}