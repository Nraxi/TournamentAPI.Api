﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class TournamentAPIApiContext : DbContext
    {
        public TournamentAPIApiContext (DbContextOptions<TournamentAPIApiContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentAPI.Core.Entities.Tournament> Tournament { get; set; } = default!;
        public DbSet<TournamentAPI.Core.Entities.Game> Game { get; set; } = default!;
    }
}
    