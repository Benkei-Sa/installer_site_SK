namespace installer_site_SK.Models.Admin;

public class UserListItemViewModel
{
    public string Email { get; set; } = "";
    public List<string> Roles { get; set; } = new();
}