using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models;

[Table("carts")]
public class Cart
{
    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("user_id", TypeName = "uuid")]
    public Guid UserId { get; set; }
    
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}