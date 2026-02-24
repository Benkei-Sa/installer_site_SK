namespace installer_site_SK.Models.Admin;

public class OrderListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }

    public string WorkerEmail { get; set; } = "";
    public string WorkerId { get; set; } = "";
}