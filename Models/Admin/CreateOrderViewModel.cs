using System.ComponentModel.DataAnnotations;

namespace installer_site_SK.Models.Admin;

public class CreateOrderViewModel
{
    [Required(ErrorMessage = "Введите название заказа.")]
    [MaxLength(200)]
    public string Title { get; set; } = "";

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Выберите работника.")]
    public string AssignedToUserId { get; set; } = "";

    public List<(string Id, string Email)> Workers { get; set; } = new();
}