using System.ComponentModel.DataAnnotations;

namespace JensenOnline.Api.Models;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required]
    public string ProductName { get; set; } = "";

    public decimal Price { get; set; }
}
