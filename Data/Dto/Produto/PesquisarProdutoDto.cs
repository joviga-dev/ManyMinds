using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.Produto;

public class PesquisarProdutoDto
{
    public int codigo { get; set; }
    public string nome { get; set; }
    public decimal valorUnitario { get; set; }
    public bool status { get; set; }
}
