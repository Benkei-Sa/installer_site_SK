using System.ComponentModel.DataAnnotations;

namespace installer_site_SK.Models.Admin;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Введите email.")]
    [EmailAddress(ErrorMessage = "Введите корректный email.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль.")]
    [DataType(DataType.Password)]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Пароль должен быть не короче 8 символов и содержать: 1 заглавную букву, 1 цифру и 1 спецсимвол."
    )]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Выберите роль.")]
    public string Role { get; set; } = "Worker";
}