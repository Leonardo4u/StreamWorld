using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamWorld.Data;
using StreamWorld.Models;

namespace StreamWorld.Controllers.Admin;

[Authorize(Policy = "AdminOnly")]
public class ProductionsController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductionsController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _db.Producoes.Include(p => p.Genero).OrderBy(p => p.Titulo).ToListAsync();
        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Generos = await _db.Generos.OrderBy(g => g.Nome).ToListAsync();
        ViewBag.Artistas = await _db.Artistas.OrderBy(a => a.Nome).ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Producao producao, int[]? selectedArtists)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Generos = await _db.Generos.OrderBy(g => g.Nome).ToListAsync();
            ViewBag.Artistas = await _db.Artistas.OrderBy(a => a.Nome).ToListAsync();
            return View(producao);
        }

        _db.Producoes.Add(producao);
        await _db.SaveChangesAsync();

        if (selectedArtists != null && selectedArtists.Length > 0)
        {
            foreach (var aid in selectedArtists)
            {
                _db.ProducaoArtistas.Add(new ProducaoArtista { ProducaoId = producao.Id, ArtistaId = aid });
            }
            await _db.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var p = await _db.Producoes.Include(x => x.ProducaoArtistas).FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return NotFound();
        ViewBag.Generos = await _db.Generos.OrderBy(g => g.Nome).ToListAsync();
        ViewBag.Artistas = await _db.Artistas.OrderBy(a => a.Nome).ToListAsync();
        return View(p);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Producao producao, int[]? selectedArtists)
    {
        if (id != producao.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            ViewBag.Generos = await _db.Generos.OrderBy(g => g.Nome).ToListAsync();
            ViewBag.Artistas = await _db.Artistas.OrderBy(a => a.Nome).ToListAsync();
            return View(producao);
        }

        _db.Entry(producao).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        // atualizar artistas
        var existing = _db.ProducaoArtistas.Where(pa => pa.ProducaoId == id);
        _db.ProducaoArtistas.RemoveRange(existing);
        if (selectedArtists != null && selectedArtists.Length > 0)
        {
            foreach (var aid in selectedArtists)
            {
                _db.ProducaoArtistas.Add(new ProducaoArtista { ProducaoId = id, ArtistaId = aid });
            }
        }
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var p = await _db.Producoes.Include(p => p.Genero).FirstOrDefaultAsync(p => p.Id == id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var p = await _db.Producoes.FindAsync(id);
        if (p != null)
        {
            var pas = _db.ProducaoArtistas.Where(pa => pa.ProducaoId == id);
            _db.ProducaoArtistas.RemoveRange(pas);
            _db.Producoes.Remove(p);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
