using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models;

[Table("cart_items")]
public class CartItem
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [Column("cart_id", TypeName = "uuid")]
    public Guid CartId { get; set; }
    
    [Required]
    [Column("product_id", TypeName = "uuid")]
    public Guid ProductId { get; set; }

    [Required]
    [Column("quantity")]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}