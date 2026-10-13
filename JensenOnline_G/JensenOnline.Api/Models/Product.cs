using System.ComponentModel.DataAnnotations;

namespace JensenOnline.Api.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [MaxLength(500)]
    public string Description { get; set; } = "";
}
