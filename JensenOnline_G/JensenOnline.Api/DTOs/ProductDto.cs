using System.ComponentModel.DataAnnotations;

namespace JensenOnline.Api.DTOs;

public class ProductDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [MaxLength(500)]
    public string Description { get; set; } = "";
}
