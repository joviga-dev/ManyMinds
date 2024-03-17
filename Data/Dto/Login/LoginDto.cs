using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.Login;

public class LoginDto
{
    [Required]
    [StringLength(10)]
    public string Login { get; set; }
    [Required]
    [StringLength(8)]
    public string Senha { get; set; }
}
