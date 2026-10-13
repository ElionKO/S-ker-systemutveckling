using System.ComponentModel.DataAnnotations;

namespace JensenOnline.Api.DTOs;

public class CreateOrderDto
{
    [Required]
    public int ProductId { get; set; }
}
