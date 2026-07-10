using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Auth.Requests;

public class RegisterRequest
{
    [Required] 
    [MaxLength(50)] 
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
}