using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.Usuario;

public class AdicionarUsuarioDto
{
    [Required]
    [StringLength(10)]
    public string Login { get; set; }
    [Required]
    [StringLength(8)]
    public string Senha { get; set; }
}
