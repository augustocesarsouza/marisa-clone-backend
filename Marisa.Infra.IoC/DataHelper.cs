using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Marisa.Infra.IoC
{
    public class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            // Obter o DbContext
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            // Verificar se há migrações pendentes
            var pendingMigrations = await dbContextSvc.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                // Se houver migrações pendentes, aplicar as migrações
                var logger = svcProvider.GetRequiredService<ILogger<DataHelper>>();
                logger.LogInformation($"Há {pendingMigrations.Count()} migrações pendentes. Aplicando migrações...");
                await dbContextSvc.Database.MigrateAsync();
                logger.LogInformation("Migrações aplicadas com sucesso.");
            }
            else
            {
                // Caso não haja migrações pendentes
                var logger = svcProvider.GetRequiredService<ILogger<DataHelper>>();
                logger.LogInformation("Não há migrações pendentes.");
            }
        }
    }
}
