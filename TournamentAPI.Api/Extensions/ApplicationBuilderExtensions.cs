using TournamentAPI.Data.Data;

namespace TournamentAPI.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<TournamentAPIApiContext>();

                //await context.Database.EnsureDeletedAsync();
                //await context.Database.MigrateAsync();

                try
                {
                     SeedData.Initialize(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

        }
    }
}
