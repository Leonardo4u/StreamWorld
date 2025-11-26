using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamWorld.Models;

[Table("Genres")]
public class Genero
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Producao> Producoes { get; set; } = new List<Producao>();
}
