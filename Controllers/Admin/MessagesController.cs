using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamWorld.Data;

namespace StreamWorld.Controllers.Admin;

[Authorize(Policy = "AdminOnly")]
public class MessagesController : Controller
{
    private readonly ApplicationDbContext _db;

    public MessagesController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var msgs = await _db.MensagensContato.OrderByDescending(m => m.EnviadoEm).ToListAsync();
        return View(msgs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var m = await _db.MensagensContato.FindAsync(id);
        if (m == null) return NotFound();
        return View(m);
    }
}
