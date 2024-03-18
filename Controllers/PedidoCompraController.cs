using AutoMapper;
using ManyMinds.Controllers;
using ManyMinds.Data;
using ManyMinds.Models;
using ManyMindsApi.Data.Dto.Fornecedor;
using ManyMindsApi.Data.Dto.PedidoCompra;
using ManyMindsApi.Data.Dto.Produto;
using ManyMindsApi.Models;
using ManyMindsApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Xml;

namespace ManyMindsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoCompraController : ControllerBase
{
    private readonly ManyMindsApiContext context;
    private readonly IMapper mapper;
    private readonly ISystemLogService logService;

    public PedidoCompraController(ManyMindsApiContext context, IMapper mapper, ISystemLogService logService)
    {
        this.context = context;
        this.mapper = mapper;
        this.logService = logService;
    }

    // Endpoint para adicionar um novo pedido de compra
    [HttpPost]
    public IActionResult AdicionaPedidoCompra([FromBody] AdicionarPedidoCompraDto dto)
    {
        PedidoCompra pedidoCompra = this.mapper.Map<PedidoCompra>(dto);

        if (!dto.ProdutosParaCompra.Any())
            return BadRequest("Adicione pelo menos um Código de produto e sua Quantidade desejada");

        pedidoCompra.Itens.AddRange(GeraLstItens(dto));

        pedidoCompra.Fornecedor = BuscaFornecedor(dto.Fornecedor); 

        this.context.pedidosCompra.Add(pedidoCompra);
        this.context.SaveChanges();

        logService.AdicionaLog(
            "Adicionar",
            pedidoCompra.Id.ToString(),
            pedidoCompra.Id,
            DateTime.Now,
            "Pedido de Compra " + pedidoCompra.Id + ", Fornecedor: " + pedidoCompra.Fornecedor.Id + " adicionado com sucesso com " + pedidoCompra.Itens.Count() + " itens");

        return CreatedAtAction(nameof(FindPedidoCompraById), new { id = pedidoCompra.Id }, pedidoCompra);
    }

    // Endpoint para encontrar um pedido de compra pelo ID
    [HttpGet("{id}")]
    public IActionResult FindPedidoCompraById(int id)
    {
        var pedidoCompra = this.context.pedidosCompra
            .Include(pedido => pedido.Itens) 
            .ThenInclude(item => item.Produto) 
            .FirstOrDefault(pedidoCompra => pedidoCompra.Id == id);

        if (pedidoCompra == null)
            return NotFound("Pedido de Compra com o código " + id + " não encontrado!");

        var pedidoCompraDto = this.mapper.Map<PesquisarPedidoCompraDto>(pedidoCompra);
        
        return Ok(pedidoCompraDto);
    }

    // Endpoint para pesquisar todos os pedidos de compra
    [HttpGet("todos")]
    public IEnumerable<PesquisarPedidoCompraDto> PesquisaPedidosCompra([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return this.mapper.Map<List<PesquisarPedidoCompraDto>>(this.context.pedidosCompra
            .Include(pedido => pedido.Itens) 
            .ThenInclude(item => item.Produto) 
            .Skip(skip).Take(take)
            );
    }

    // Inativa um pedido de compra pelo ID
    [HttpDelete("{id}/finalizar")]
    public IActionResult FinalizaPedidoCompra(int id)
    {
        var pedidoCompraParaAtualizar = BuscaPedidoCompra(id);

        pedidoCompraParaAtualizar.status = false;
        this.context.SaveChanges();

        logService.AdicionaLog(
            "Finalizar",
            pedidoCompraParaAtualizar.Id.ToString(),
            pedidoCompraParaAtualizar.Id,
            DateTime.Now,
            "Pedido de Compra " + pedidoCompraParaAtualizar.Id + ", Fornecedor: " + pedidoCompraParaAtualizar.Fornecedor.Id + " finalizado com sucesso");

        return Ok("Pedido de compra " + id + " inativado com sucesso");
    }

    // Remove fisicamente um pedido de compra se o status for diferente de zero (finalizado)
    [HttpDelete("{id}/remover")]
    public IActionResult RemovePedidoCompra(int id)
    {
        var pedidoCompraParaRemover = this.context.pedidosCompra.Include(pedido => pedido.Fornecedor).FirstOrDefault(pedido => pedido.Id == id);

        if (!pedidoCompraParaRemover.status) return BadRequest("Não é permitido remover um pedido de compra finalizado.");

        this.context.pedidosCompra.Remove(pedidoCompraParaRemover);
        this.context.SaveChanges();

        logService.AdicionaLog(
            "Remover",
            pedidoCompraParaRemover.Id.ToString(),
            pedidoCompraParaRemover.Id,
            DateTime.Now,
            "Pedido de Compra " + pedidoCompraParaRemover.Id + ", Fornecedor: " + pedidoCompraParaRemover.Fornecedor.Id + " removido com sucesso");

        return Ok("Pedido de compra " + id + " removido com sucesso");

    }

    // Atualiza um pedido de compra existente
    [HttpPut("{id}")]
    public IActionResult AtualizaPedidoCompra(int id, [FromBody] AtualizarPedidoCompraDto dto)
    {
        var pedidoCompraParaAtualizar = BuscaPedidoCompra(id);

        if (!pedidoCompraParaAtualizar.status)
            return BadRequest("Não é permitido alterar pedidos inativados");

        this.mapper.Map(dto, pedidoCompraParaAtualizar);
        this.context.SaveChanges();

        logService.AdicionaLog(
            "Atualizar",
            pedidoCompraParaAtualizar.Id.ToString(),
            pedidoCompraParaAtualizar.Id,
            DateTime.Now,
            "Pedido de Compra " + pedidoCompraParaAtualizar.Id + ", Fornecedor: " + pedidoCompraParaAtualizar.Fornecedor.Id + " atualizado com sucesso");

        return Ok("Pedido de compra " + id + " atualizado com sucesso");
    }

    // Atualiza parcialmente um pedido de compra existente
    [HttpPatch("{id}")]
    public IActionResult AtualizaPedidoCompraParcial(int id, JsonPatchDocument<AtualizarPedidoCompraDto> patch)
    {
        var pedidoCompra = BuscaPedidoCompra(id);

        var pedidoCompraParaAtualizar = this.mapper.Map<AtualizarPedidoCompraDto>(pedidoCompra);

        patch.ApplyTo(pedidoCompraParaAtualizar, ModelState);

        if (!TryValidateModel(pedidoCompraParaAtualizar)) return ValidationProblem(ModelState);
        
        this.mapper.Map(pedidoCompraParaAtualizar, pedidoCompra);
        this.context.SaveChanges();

        return Ok("Dados do Pedido de compra " + id + " atualizados com sucesso");
    }


    // Gera itens de compra com base nos produtos listados no DTO
    private List<Item> GeraLstItens(AdicionarPedidoCompraDto dto)
    {
        var lista = new List<Item>();
        foreach (ProdutoParaCompra produtoListado in dto.ProdutosParaCompra)
        {
            var produto = this.context.produtos.FirstOrDefault(produto => produto.codigo == produtoListado.Codigo);

            if (produto == null)
                throw new BadHttpRequestException("Produto com o código " + produtoListado.Codigo + " não encontrado!");

            if (!produto.status)
                throw new BadHttpRequestException("Produto Inativo");

            Item item = new Item(produto, produtoListado.Quantidade);
            lista.Add(item);
        }
        return lista;
    }

    // Busca um fornecedor pelo ID
    private Fornecedor BuscaFornecedor(int Id)
    {
        var fornecedorDoPedido = this.context.Fornecedores.FirstOrDefault(fornecedor => fornecedor.Id == Id);

        if (fornecedorDoPedido == null)
            throw new BadHttpRequestException("Fornecedor com o código " + Id + " não encontrado!");

        if (!fornecedorDoPedido.Status)
            throw new BadHttpRequestException("Fornecedor Inativo");

        return fornecedorDoPedido;
    }

    // Verifica o Pedido de Compra, para evitar repetição
    private PedidoCompra BuscaPedidoCompra(int id) {

        var pedidoCompra = this.context.pedidosCompra
            .Include(pedido => pedido.Fornecedor)
            .FirstOrDefault(pedido => pedido.Id == id);

        if (pedidoCompra == null)
            throw new BadHttpRequestException("Pedido de Compra com o código " + id + " não encontrado!");

        return pedidoCompra;
    }
}
