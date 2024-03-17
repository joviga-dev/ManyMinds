using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.Produto
{
    public class AdicionarProdutoDto
    {
        [Key]
        [Required(ErrorMessage = "O código do produto é obrigatório")]
        public int codigo { get; set; }
        [Required(ErrorMessage = "O Nome do produto é obrigatório")]
        [StringLength(30, ErrorMessage = "O tamanho do nome não pode exceder 30 caracteres")]
        public string nome { get; set; }
        [Required(ErrorMessage = "O Valor do produto é obrigatório")]
        public decimal valorUnitario { get; set; }
        [Required(ErrorMessage = "O Status do produto é obrigatório")]
        public bool status { get; set; }
    }
}
