using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models;

[Table("discounts")]
public class Discount
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Column("discount_percentage",  TypeName = "decimal(5,2)")]
    public decimal DiscountPercentage { get; set; }

    [Required] 
    [Column("is_active")] 
    public bool IsActive { get; set; } = true;
    
    [Column("valid_until")]
    public DateTime ValidUntil { get; set; }
}