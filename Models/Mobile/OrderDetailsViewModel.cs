using System.ComponentModel.DataAnnotations;

namespace installer_site_SK.Models.Mobile;

public class OrderDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }

    [Required]
    public string Status { get; set; } = "New";

    [MaxLength(2000)]
    public string? WorkerComment { get; set; }
}