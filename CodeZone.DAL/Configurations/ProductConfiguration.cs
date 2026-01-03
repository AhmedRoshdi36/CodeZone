using CodeZone.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeZone.DAL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.Description)
            .HasMaxLength(1000);
        
        builder.HasIndex(p => p.SKU).IsUnique();
            //i  know In memorydb doesnt enforce unique constraints
        
        // Configure relationship with StockTransactions
        builder.HasMany(p => p.StockTransactions)
            .WithOne(st => st.Product)
            .HasForeignKey(st => st.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

