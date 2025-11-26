using Microsoft.EntityFrameworkCore;
using StreamWorld.Models;

namespace StreamWorld.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Producao> Producoes => Set<Producao>();
    public DbSet<Genero> Generos => Set<Genero>();
    public DbSet<Artista> Artistas => Set<Artista>();
    public DbSet<ProducaoArtista> ProducaoArtistas => Set<ProducaoArtista>();
    public DbSet<MensagemContato> MensagensContato => Set<MensagemContato>();

    protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Chave primária composta para junção (ProducaoArtista)
        modelBuilder.Entity<ProducaoArtista>().HasKey(pa => new { pa.ProducaoId, pa.ArtistaId });
    }
}
