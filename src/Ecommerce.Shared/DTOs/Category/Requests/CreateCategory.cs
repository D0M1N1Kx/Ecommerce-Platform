using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Category.Requests;

public class CreateCategory
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
}