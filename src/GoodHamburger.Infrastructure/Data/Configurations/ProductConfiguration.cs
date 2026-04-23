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
            new Product(new Guid("11111111-1111-1111-1111-111111111111"), "X Burger", 5.00m, ProductCategory.Sandwich),
            new Product(new Guid("22222222-2222-2222-2222-222222222222"), "X Egg", 4.50m, ProductCategory.Sandwich),
            new Product(new Guid("33333333-3333-3333-3333-333333333333"), "X Bacon", 7.00m, ProductCategory.Sandwich),
            new Product(new Guid("44444444-4444-4444-4444-444444444444"), "Batata frita", 2.00m, ProductCategory.Fries),
            new Product(new Guid("55555555-5555-5555-5555-555555555555"), "Refrigerante", 2.50m, ProductCategory.Soda)
        );
    }
}