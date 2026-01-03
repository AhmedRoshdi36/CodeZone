
using System.ComponentModel.DataAnnotations;

namespace CodeZone.DAL.Entities;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string SKU { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    // Navigation property
    public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
