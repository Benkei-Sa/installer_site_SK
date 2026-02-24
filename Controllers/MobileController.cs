using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace installer_site_SK.Controllers;

[Authorize(Roles = "Worker,Admin,SuperAdmin")]
[Route("m")]
public class MobileController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => RedirectToAction(nameof(Orders));

    [HttpGet("library")]
    public IActionResult Library() => View();

    [HttpGet("orders")]
    public IActionResult Orders() => View();

    [HttpGet("calendar")]
    public IActionResult Calendar() => View();
}