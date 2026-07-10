using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models;

[Table("products")]
public class Product
{
    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("name")]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    
    [Column("description", TypeName = "text")]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Column("price", TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Required] 
    [Column("stock")] 
    public int Stock { get; set; } = 0;
    
    [Required]
    [Column("sku")]
    [MaxLength(50)]
    public string Sku { get; set; } = string.Empty;
    
    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }
    
    [Column("discount_id")]
    public int DiscountId { get; set; }

    public Category Category { get; set; } = null!;
    public Discount? Discount { get; set; }
}