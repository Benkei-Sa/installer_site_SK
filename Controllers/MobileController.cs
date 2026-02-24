using installer_site_SK.Data;
using installer_site_SK.Models.Mobile;
using installer_site_SK.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace installer_site_SK.Controllers;

[Authorize(Roles = "Worker,Admin,SuperAdmin")]
[Route("m")]
public class MobileController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public MobileController(AppDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet("")]
    public IActionResult Index() => RedirectToAction(nameof(Orders));

    [HttpGet("library")]
    public IActionResult Library() => View();

    [HttpGet("orders")]
    public async Task<IActionResult> Orders()
    {
        var user = await _userManager.GetUserAsync(User);

        var orders = await _db.Orders
            .Where(o => o.AssignedToUserId == user!.Id)
            .OrderByDescending(o => o.CreatedAtUtc)
            .ToListAsync();

        return View(orders);
    }


    [HttpGet("orders/{id:int}")]
    public async Task<IActionResult> OrderDetails(int id)
    {
        var user = await _userManager.GetUserAsync(User);

        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.AssignedToUserId == user!.Id);
        if (order == null) return NotFound();

        var vm = new OrderDetailsViewModel
        {
            Id = order.Id,
            Title = order.Title,
            Description = order.Description,
            Status = order.Status,
            WorkerComment = order.WorkerComment
        };

        return View(vm);
    }

    [HttpPost("orders/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OrderDetails(int id, OrderDetailsViewModel vm)
    {
        var user = await _userManager.GetUserAsync(User);

        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.AssignedToUserId == user!.Id);
        if (order == null) return NotFound();

        if (!ModelState.IsValid)
        {
            vm.Title = order.Title;
            vm.Description = order.Description;
            return View(vm);
        }

        order.Status = vm.Status;
        order.WorkerComment = vm.WorkerComment;
        order.UpdatedAtUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(OrderDetails), new { id });
    }


    [HttpGet("calendar")]
    public IActionResult Calendar() => View();


}