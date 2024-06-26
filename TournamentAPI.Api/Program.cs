﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentAPI.Api.Extensions;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data;
using TournamentAPI.Data.Data;
using TournamentAPI.Data.Repositories;

namespace TournamentAPI.Api
{
    public class Program
    {
        public static void  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TournamentAPIApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentAPIApiContext") ?? throw new InvalidOperationException("Connection string 'TournamentAPIApiContext' not found.")));

            builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Add services to the container.

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
             app.SeedDataAsync();
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
