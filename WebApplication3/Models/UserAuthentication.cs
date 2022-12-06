using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class UserAuthentication
{
    [Key]
    public string? UserName { get; set; }
    [Required]
    public string? Password { get; set; }
}