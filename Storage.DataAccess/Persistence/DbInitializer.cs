using Microsoft.EntityFrameworkCore;
using Storage.Core.Enitities;
using Storage.DataAccess.Persistence.Migrations;

namespace Storage.Application.Helpers;

public static class DbInitializer
{
    public static void Initialize(DatabaseContext context)
    {
        if (!context.Equipments.Any())
        {
            context.Equipments.AddRange(
                new Equipment()
                {
                    Name = "3D Printer",
                    Area = 20
                },
                new Equipment()
                {
                    Name = "Welding Robot",
                    Area = 15
                },
                new Equipment()
                {
                    Name = "Hydraulic Press",
                    Area = 30
                });

            context.SaveChanges();
        }

        if (!context.ProductionFacilities.Any())
        {
            context.ProductionFacilities.AddRange(
                new ProductionFacility()
                {
                    Name = "Tech Park",
                    StandartArea = 150
                },
                new ProductionFacility()
                {
                    Name = "Central Facility",
                    StandartArea = 230
                });
            
            context.SaveChanges();
        }
    }
}