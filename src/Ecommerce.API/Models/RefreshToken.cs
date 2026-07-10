using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models;

[Table("refresh_tokens")]
public class RefreshToken
{
    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("user_id", TypeName = "uuid")]
    public Guid UserId { get; set; }
    
    [Required]
    [Column("token_hash")]
    public string TokenHash { get; set; } = string.Empty;
    
    [Required]
    [Column("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [Required] 
    [Column("revoked")] 
    public bool Revoked { get; set; } = false;
    
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}