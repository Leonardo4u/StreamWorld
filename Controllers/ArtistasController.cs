using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamWorld.Data;

namespace StreamWorld.Controllers;

public class ArtistasController : Controller
{
    private readonly ApplicationDbContext _db;

    public ArtistasController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string? nome, string? pais)
    {
        var query = _db.Artistas.AsQueryable();

        // Filtrar por nome se fornecido
        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query.Where(a => a.Nome.Contains(nome));
            ViewBag.NomeFiltro = nome;
        }

        // Filtrar por país se fornecido
        if (!string.IsNullOrWhiteSpace(pais))
        {
            query = query.Where(a => a.PaisOrigem != null && a.PaisOrigem.Contains(pais));
            ViewBag.PaisFiltro = pais;
        }

        var artistas = await query.OrderBy(a => a.Nome).ToListAsync();
        return View(artistas);
    }

    public async Task<IActionResult> Details(int id)
    {
        var artista = await _db.Artistas
            .Include(a => a.ProducaoArtistas)
                .ThenInclude(pa => pa.Producao)
                    .ThenInclude(p => p!.Genero)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (artista == null)
        {
            return NotFound();
        }

        // Buscar produções onde o artista é diretor
        var producoesComoDiretor = await _db.Producoes
            .Include(p => p.Genero)
            .Where(p => p.DiretorId == id)
            .ToListAsync();

        ViewBag.ProducoesComoDiretor = producoesComoDiretor;

        return View(artista);
    }
}
