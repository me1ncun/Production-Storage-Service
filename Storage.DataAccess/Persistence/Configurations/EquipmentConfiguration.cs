using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Core.Enitities;

namespace Storage.DataAccess.Persistence.Configurations;

public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("Equipment");
        
        builder.HasKey(e => e.Code); 

        builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(e => e.Area)
                .IsRequired();
    }
}