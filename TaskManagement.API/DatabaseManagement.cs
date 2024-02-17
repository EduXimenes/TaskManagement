using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.API
{
    public static class DatabaseManagementService
    {
        public static void MigrationInit(WebApplication app)
        {
            using (var ServiceScope = app.Services.CreateScope())
            {
                var serviceDb = ServiceScope.ServiceProvider
                                            .GetService<TaskDbContext>();
                if (serviceDb == null)
                {
                    return;
                }
                serviceDb.Database.Migrate();
            }
        }
    }
}
