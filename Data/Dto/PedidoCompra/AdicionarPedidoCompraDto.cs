using ManyMindsApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.PedidoCompra
{
    public class ProdutoParaCompra
    {
        public int Codigo { get; set; }
        public int Quantidade { get; set; }
    }

    public class AdicionarPedidoCompraDto
    {
        [Required(ErrorMessage = "Pelo menos um produto e sua quantidade sáo obrigatórios")]
        public List<ProdutoParaCompra> ProdutosParaCompra;

        [Required]
        public bool Status { get; set; }

        [StringLength(150, ErrorMessage = "O tamanho do nome não pode exceder 150 caracteres")]
        public String Obs { get; set; }

        [Required]
        public int Fornecedor { get; set; }
    }
}
