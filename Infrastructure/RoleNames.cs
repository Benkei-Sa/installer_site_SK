namespace installer_site_SK.Infrastructure;

public static class RoleNames
{
    public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Admin";
    public const string Worker = "Worker";

    public static string ToRu(string role) => role switch
    {
        SuperAdmin => "СуперАдмин",
        Admin => "Админ",
        Worker => "Установщик",
        _ => role
    };
}