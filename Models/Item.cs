using ManyMinds.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManyMindsApi.Models;


public class Item
{
    [Key]
    public int Id { get; set; }
    [Required]
    public virtual Produto Produto { get; set; }
    [Required]
    public int Quantidade { get; set; }

    [Required]
    public decimal ValorTotal { get; set; }

    [Required]
    [JsonIgnore]
    public PedidoCompra PedidoCompra { get; set; }

    public Item(Produto produto, int quantidade)
    {
        this.Produto = produto;
        this.Quantidade = quantidade;
        this.ValorTotal = produto.valorUnitario * quantidade;
    }

    public Item() { }
}
