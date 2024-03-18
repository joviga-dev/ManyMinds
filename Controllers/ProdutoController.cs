using AutoMapper;
using ManyMinds.Data;
using ManyMinds.Models;
using ManyMindsApi.Controllers;
using ManyMindsApi.Data.Dto.Produto;
using ManyMindsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManyMinds.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly ManyMindsApiContext context;
    private readonly IMapper mapper;
    private readonly ISystemLogService logService;

    public ProdutoController(ManyMindsApiContext context, IMapper mapper, ISystemLogService logService)
    {
        this.context = context;
        this.mapper = mapper;
        this.logService = logService;
    }

    // Adiciona um novo produto
    [HttpPost]
    public IActionResult AdicionaProduto([FromBody] AdicionarProdutoDto dto)
    {
        Produto produto = this.mapper.Map<Produto>(dto);

        this.context.produtos.Add(produto);
        this.context.SaveChanges();

        logService.AdicionaLog(
            "Adicionar",
            produto.nome,
            produto.codigo,
            DateTime.Now,
            "Produto " + produto.nome + " adicionado com sucesso");

        return CreatedAtAction(nameof(FindProdutoByCodigo),
            new { codigo = produto.codigo },
            produto);
    }

    // Atualiza um produto existente
    [HttpPut("{codigo}")]
    public IActionResult AtualizaProduto(int codigo, [FromBody] AtualizarProdutoDto dto)
    {
        var produtoParaAtualizar = this.BuscaProduto(codigo);

        if (!produtoParaAtualizar.status)
            return BadRequest("Não é permitido alterar itens inativados");


        this.mapper.Map(dto, produtoParaAtualizar);
        this.context.SaveChanges();

        logService.AdicionaLog(
            "Atualizar",
            produtoParaAtualizar.nome,
            produtoParaAtualizar.codigo,
            DateTime.Now,
            "Produto " + produtoParaAtualizar.nome + " atualizado com sucesso");

        return Ok("Produto " + codigo + " atualizado com sucesso");
    }

    // Atualiza parcialmente um produto existente
    [HttpPatch("{codigo}")]
    public IActionResult AtualizaProdutoParcial(int codigo, JsonPatchDocument<AtualizarProdutoDto> patch)
    {
        var produto = this.BuscaProduto(codigo);

        if (!produto.status)
            return BadRequest("Não é permitido alterar itens inativados");

        var produtoParaAtualizar = this.mapper.Map<AtualizarProdutoDto>(produto);

        patch.ApplyTo(produtoParaAtualizar, ModelState);

        if (!TryValidateModel(produtoParaAtualizar)) return ValidationProblem(ModelState);

        this.mapper.Map(produtoParaAtualizar, produto);
        this.context.SaveChanges();

        return Ok("Dados do Produto " + codigo + " atualizados com sucesso");
    }

    // Reativa Produto
    [HttpPatch("{codigo}/reativar")]
    public IActionResult ReativaProduto(int codigo)
    {
        var patch = new JsonPatchDocument<ReativarProdutoDto>();
        var produto = this.BuscaProduto(codigo);

        if (!produto.status)
            produto.status = true;

        var produtoParaReativar = this.mapper.Map<ReativarProdutoDto>(produto);

        patch.ApplyTo(produtoParaReativar, ModelState);

        if (!TryValidateModel(produtoParaReativar)) return ValidationProblem(ModelState);

        this.mapper.Map(produtoParaReativar, produto);
        this.context.SaveChanges();

        return Ok("Dados do Produto " + codigo + " atualizados com sucesso");
    }

    // Retorna todos os produtos
    [HttpGet("todos")]
    public IEnumerable<PesquisarProdutoDto> PesquisaProdutos([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return this.mapper.Map<List<PesquisarProdutoDto>>(this.context.produtos.Skip(skip).Take(take));
    }

    // Retorna um produto pelo código
    [HttpGet("{codigo}")]
    public IActionResult FindProdutoByCodigo(int codigo)
    {
        var produto = this.BuscaProduto(codigo);

        var produtoDto = this.mapper.Map<PesquisarProdutoDto>(produto);

        return Ok(produtoDto);
    }

    // Inativa um produto pelo código
    [HttpDelete("{codigo}")]
    public IActionResult InativaProduto(int codigo)
    {
        var produtoParaAtualizar = BuscaProduto(codigo);

        produtoParaAtualizar.status = false;
        this.context.SaveChanges();


        logService.AdicionaLog(
            "Inativar",
            produtoParaAtualizar.nome,
            produtoParaAtualizar.codigo,
            DateTime.Now,
            "Produto " + produtoParaAtualizar.nome + " inativado com sucesso");

        return Ok("Produto " + codigo + " inativado com sucesso");
    }

    private Produto BuscaProduto(int codigo)
    {
        var produto = this.context.produtos
            .FirstOrDefault(produto => produto.codigo == codigo);

        if (produto == null)
            throw new BadHttpRequestException("Produto com o código " + codigo + " não encontrado!");

        return produto;
    }
}
