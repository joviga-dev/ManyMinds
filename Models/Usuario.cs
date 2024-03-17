using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Login { get; set; }
    [Required]
    [StringLength(8)]
    public string Senha{ get; set; }
    public int nTentativas { get; set; }
    public string? Ip { get; set; }
    public DateTime? DataBloqueio { get; set; }
    public TimeSpan? DuracaoBloqueio { get; set; }

}
