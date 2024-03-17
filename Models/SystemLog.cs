using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Models;

public class SystemLog
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Tipo { get; set; } 
    [Required]
    public string EntidadeAfetada { get; set; }
    [Required]
    public int EntidadeId {  get; set; }
    [Required]
    public DateTime DateTime { get; set; }
    [Required]
    [StringLength(150)]
    public string Detalhes { get; set; }

    public SystemLog(string tipo, string entidadeAfetada, int entidadeId, DateTime dateTime, string detalhes)
    {
        Tipo = tipo;
        EntidadeAfetada = entidadeAfetada;
        DateTime = dateTime;
        Detalhes = detalhes;
        EntidadeId = entidadeId;
    }
}
