using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        //var categoryClassModels = EnumHelper.ConvertToModel<Enums.Category>();
        //var categoryEntities = categoryClassModels.Select(r => new Category
        //{
        //    Id = (Enums.Category)r.Id,
        //    Name = r.Name
        //}).ToList();

        //builder.HasData(categoryEntities);
    }
}
