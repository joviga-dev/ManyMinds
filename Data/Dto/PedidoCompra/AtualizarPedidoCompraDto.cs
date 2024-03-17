using ManyMindsApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.PedidoCompra
{
    public class AtualizarPedidoCompraDto
    {
        public bool Status { get; set; }

        public String Obs { get; set; }

    }
}