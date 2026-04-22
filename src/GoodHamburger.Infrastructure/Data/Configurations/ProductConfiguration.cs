using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

        builder.HasData(
            new Product(Guid.NewGuid(), "X Burger", 5.00m, ProductCategory.Sandwich),
            new Product(Guid.NewGuid(), "X Egg", 4.50m, ProductCategory.Sandwich),
            new Product(Guid.NewGuid(), "X Bacon", 7.00m, ProductCategory.Sandwich),
            new Product(Guid.NewGuid(), "Batata frita", 2.00m, ProductCategory.Fries),
            new Product(Guid.NewGuid(), "Refrigerante", 2.50m, ProductCategory.Soda)
        );
    }
}