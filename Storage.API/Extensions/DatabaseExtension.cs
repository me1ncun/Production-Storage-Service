using Storage.Application.Helpers;
using Storage.DataAccess.Persistence.Migrations;

namespace Equipment_Storage_Service.Extensions;

public static class DatabaseExtension
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DatabaseContext>();
        
            context.Database.EnsureCreated();
        
            DbInitializer.Initialize(context);
        }
    }
}