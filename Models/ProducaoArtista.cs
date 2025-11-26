using System.ComponentModel.DataAnnotations.Schema;

namespace StreamWorld.Models;

[Table("ProductionArtists")]
public class ProducaoArtista
{
    public int ProducaoId { get; set; }
    public Producao? Producao { get; set; }

    public int ArtistaId { get; set; }
    public Artista? Artista { get; set; }
}
