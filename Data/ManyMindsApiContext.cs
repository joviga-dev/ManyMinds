using ManyMinds.Models;
using ManyMindsApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ManyMinds.Data;

public class ManyMindsApiContext : DbContext
{
    public DbSet<Produto> produtos { get; set; }
    public DbSet<PedidoCompra> pedidosCompra { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Item> Itens { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<SystemLog> Logs { get; set; }
    public ManyMindsApiContext(DbContextOptions<ManyMindsApiContext> opts) 
        : base(opts)
    {
            
    }
}
