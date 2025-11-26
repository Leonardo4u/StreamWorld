using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamWorld.Models;

[Table("Productions")]
public class Producao
{
    public int Id { get; set; }


    [Required]
    [StringLength(250)]
    public string Titulo { get; set; } = string.Empty;

    [Display(Name = "Link da Capa")]
    [StringLength(1000)]
    public string? LinkCapa { get; set; }

    [StringLength(2000)]
    public string? Sinopse { get; set; }


    [DataType(DataType.Date)]
    public DateTime? DataLancamento { get; set; }

    [Display(Name = "Ano de Criação")]
    public int? AnoCriacao { get; set; }

    [StringLength(2000)]
    public string? Resumo { get; set; }

    [Required]
    [StringLength(20)]
    public string Tipo { get; set; } = "Filme"; // Filme or Serie

    // Relação com Genero
    public int? GeneroId { get; set; }
    public Genero? Genero { get; set; }

    // Relação com Diretor (um Artista)
    [Display(Name = "Diretor")]
    public int? DiretorId { get; set; }
    public Artista? Diretor { get; set; }

    public ICollection<ProducaoArtista> ProducaoArtistas { get; set; } = new List<ProducaoArtista>();
}
