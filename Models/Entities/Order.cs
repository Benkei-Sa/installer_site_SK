using System.ComponentModel.DataAnnotations;

namespace installer_site_SK.Models.Entities;

public class Order
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = "";

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public string AssignedToUserId { get; set; } = "";

    // Три статуса для MVP
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "New";

    // Комментарий исполнителя
    [MaxLength(2000)]
    public string? WorkerComment { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}