using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.Produto
{
    public class ReativarProdutoDto
    {
        [Required]
        public bool status{ get; set; }
    }
}
