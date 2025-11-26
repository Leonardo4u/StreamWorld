using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamWorld.Data;
using StreamWorld.Models;

namespace StreamWorld.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductsController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string? search, int? generoId, int? artistaId, int page = 1, int pageSize = 6, string sort = "title_asc")
    {
        // Consulta base para produções
        var q = _db.Producoes
            .Include(p => p.Genero)
            .Include(p => p.Diretor)
            .Include(p => p.ProducaoArtistas)!.ThenInclude(pa => pa.Artista)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(p => p.Titulo.Contains(search) 
                || (p.Diretor != null && p.Diretor.Nome.Contains(search))
                || p.ProducaoArtistas!.Any(pa => pa.Artista!.Nome.Contains(search)));

        if (generoId.HasValue)
            q = q.Where(p => p.GeneroId == generoId.Value);

        if (artistaId.HasValue)
            q = q.Where(p => p.DiretorId == artistaId.Value 
                || p.ProducaoArtistas!.Any(pa => pa.ArtistaId == artistaId.Value));

        // Organização
        q = sort switch
        {
            "title_desc" => q.OrderByDescending(p => p.Titulo),
            "date_asc" => q.OrderBy(p => p.DataLancamento),
            "date_desc" => q.OrderByDescending(p => p.DataLancamento),
            _ => q.OrderBy(p => p.Titulo),
        };

        var totalCount = await q.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        ViewBag.Generos = await _db.Generos.OrderBy(g => g.Nome).ToListAsync();
        ViewBag.Artistas = await _db.Artistas.OrderBy(a => a.Nome).ToListAsync();
        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = totalPages;
        ViewBag.Sort = sort;
        ViewBag.Search = search;
        ViewBag.GeneroId = generoId;
        ViewBag.ArtistaId = artistaId;

        return View(items);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Produto produto)
    {
        if (!ModelState.IsValid)
            return View(produto);

        _db.Produtos.Add(produto);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var producao = await _db.Producoes
            .Include(p => p.Genero)
            .Include(p => p.Diretor)
            .Include(p => p.ProducaoArtistas)!.ThenInclude(pa => pa.Artista)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (producao == null) return NotFound();
        return View(producao);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var produto = await _db.Produtos.FindAsync(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Produto produto)
    {
        if (id != produto.Id) return BadRequest();
        if (!ModelState.IsValid) return View(produto);

        _db.Entry(produto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _db.Produtos.FindAsync(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _db.Produtos.FindAsync(id);
        if (produto != null)
        {
            _db.Produtos.Remove(produto);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
