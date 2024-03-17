using ManyMindsApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.PedidoCompra
{
    public class PesquisarPedidoCompraDto
    {
        public int Id { get; set; }
        public List<Item> Itens { get; set; }

        public bool Status { get; set; }

        public String Obs { get; set; }

        public decimal ValorTotalPedido { 
            get {
                return Itens.Sum(item => item.ValorTotal);
                } 
             }
    }
}
