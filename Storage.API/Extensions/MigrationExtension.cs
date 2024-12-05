using Microsoft.EntityFrameworkCore;
using Storage.DataAccess.Persistence.Migrations;

namespace Equipment_Storage_Service.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        context.Database.Migrate();
    }
}