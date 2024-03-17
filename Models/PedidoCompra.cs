using ManyMinds.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManyMindsApi.Models;

public class PedidoCompra
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Pelo menos um produto é obrigatório")]
    public List<Item> Itens { get; set; }

    [Required(ErrorMessage = "Pelo menos um fornecedor é obrigatório")]
    public Fornecedor Fornecedor { get; set; }

    [Required(ErrorMessage = "O status é obrigatório")]
    public bool status { get; set; }

    [MaxLength(150, ErrorMessage = "O tamanho do nome não pode exceder 150 caracteres")]
    public String Obs {  get; set; }

    public PedidoCompra() { 
        this.Itens = new List<Item>();
    }
}
