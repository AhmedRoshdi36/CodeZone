using CodeZone.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeZone.DAL.Configurations;

public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> builder)
    {
        builder.HasKey(st => st.Id);
        
        builder.Property(st => st.Quantity).IsRequired();
        
     
        
        builder.HasOne(st => st.Warehouse)
            .WithMany()
            .HasForeignKey(st => st.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(st => st.Product)
            .WithMany(p => p.StockTransactions)
            .HasForeignKey(st => st.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Index for efficient queries 
        builder.HasIndex(st => new { st.WarehouseId, st.ProductId });
        //i nkow the in memory db dont infore indexes 
    }
}

