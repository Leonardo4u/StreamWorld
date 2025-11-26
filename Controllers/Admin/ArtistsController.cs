using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamWorld.Data;
using StreamWorld.Models;

namespace StreamWorld.Controllers.Admin;

[Authorize(Policy = "AdminOnly")]
public class ArtistsController : Controller
{
    private readonly ApplicationDbContext _db;

    public ArtistsController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _db.Artistas.OrderBy(a => a.Nome).ToListAsync());
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Artista artista)
    {
        if (!ModelState.IsValid) return View(artista);
        _db.Artistas.Add(artista);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var artista = await _db.Artistas.FindAsync(id);
        if (artista == null) return NotFound();
        return View(artista);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Artista artista)
    {
        if (id != artista.Id) return BadRequest();
        if (!ModelState.IsValid) return View(artista);
        _db.Entry(artista).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var artista = await _db.Artistas.FindAsync(id);
        if (artista == null) return NotFound();
        return View(artista);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var artista = await _db.Artistas.FindAsync(id);
        if (artista != null)
        {
            _db.Artistas.Remove(artista);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
