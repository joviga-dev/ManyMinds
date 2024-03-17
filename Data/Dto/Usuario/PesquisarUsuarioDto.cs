using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.Usuario;

public class PesquisarUsuarioDto
{
    public string Login { get; set; }
    public string Senha { get; set; }
}
