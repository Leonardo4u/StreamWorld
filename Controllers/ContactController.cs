using Microsoft.AspNetCore.Mvc;
using StreamWorld.Data;
using StreamWorld.Models;

namespace StreamWorld.Controllers;

public class ContactController : Controller
{
    private readonly ApplicationDbContext _db;

    public ContactController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send(MensagemContato model)
    {
        if (!ModelState.IsValid) return View("Index", model);
        _db.MensagensContato.Add(model);
        await _db.SaveChangesAsync();
        ViewData["Success"] = true;
        return View("Index");
    }
}
