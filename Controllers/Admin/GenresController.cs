using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamWorld.Data;
using StreamWorld.Models;

namespace StreamWorld.Controllers.Admin;

[Authorize(Policy = "AdminOnly")]
public class GenresController : Controller
{
    private readonly ApplicationDbContext _db;

    public GenresController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _db.Generos.OrderBy(gen => gen.Nome).ToListAsync());
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genero genero)
    {
        if (!ModelState.IsValid) return View(genero);
        _db.Generos.Add(genero);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var genero = await _db.Generos.FindAsync(id);
        if (genero == null) return NotFound();
        return View(genero);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Genero genero)
    {
        if (id != genero.Id) return BadRequest();
        if (!ModelState.IsValid) return View(genero);
        _db.Entry(genero).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var genero = await _db.Generos.FindAsync(id);
        if (genero == null) return NotFound();
        return View(genero);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var genero = await _db.Generos.FindAsync(id);
        if (genero != null)
        {
            _db.Generos.Remove(genero);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
