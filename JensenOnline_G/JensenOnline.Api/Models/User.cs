using System.ComponentModel.DataAnnotations;

namespace JensenOnline.Api.Models;

public class User
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; } = "";

    [Required]
    public string PasswordHash { get; set; } = "";

    [Required]
    public string Role { get; set; } = "Customer";

    public List<Order> Orders { get; set; } = new();
}
