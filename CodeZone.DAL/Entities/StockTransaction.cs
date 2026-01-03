
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZone.DAL.Entities;

public class StockTransaction
{
    public int Id { get; set; }
    
    [Required]
    public int WarehouseId { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    [NotEqualToZero(ErrorMessage = "Quantity can't be zero")]
    public int Quantity { get; set; }   // Positive to add, negative to remove

    

    // Navigation properties
    public Warehouse? Warehouse { get; set; }  

    public Product? Product { get; set; }   
}
