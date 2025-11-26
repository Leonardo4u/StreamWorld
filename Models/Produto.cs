using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamWorld.Models;

[Table("Products")]
public class Produto
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Descricao { get; set; }

    [DataType(DataType.Currency)]
    public decimal Preco { get; set; }

    [StringLength(100)]
    public string? Categoria { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataLancamento { get; set; }
}
