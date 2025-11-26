using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamWorld.Models;

[Table("Artists")]
public class Artista
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    [Display(Name = "Tipo")]
    public string Tipo { get; set; } = "Artista"; // "Artista" ou "Diretor"

    [Display(Name = "Data de Nascimento")]
    public DateTime? DataNascimento { get; set; }

    [StringLength(100)]
    [Display(Name = "País de Origem")]
    public string? PaisOrigem { get; set; }

    [StringLength(500)]
    [Display(Name = "Link da Foto")]
    public string? LinkFoto { get; set; }

    public ICollection<ProducaoArtista> ProducaoArtistas { get; set; } = new List<ProducaoArtista>();
}
