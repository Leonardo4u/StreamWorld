using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamWorld.Models;

[Table("ContactMessages")]
public class MensagemContato
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Mensagem { get; set; }

    public DateTime EnviadoEm { get; set; } = DateTime.UtcNow;
}
