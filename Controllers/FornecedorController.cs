using AutoMapper;
using ManyMinds.Data;
using ManyMindsApi.Data.Dto.Fornecedor;
using ManyMindsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManyMindsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FornecedorController : ControllerBase
{
    private readonly ManyMindsApiContext context;
    private readonly IMapper mapper;

    public FornecedorController(ManyMindsApiContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<PesquisarFornecedorDto> Todos()
    {
        return this.mapper.Map<List<PesquisarFornecedorDto>>(this.context.Fornecedores.ToList());
    }

    [HttpPost]
    public void GeraFornecedor()
    {
        var for1 = new Fornecedor(true);
        var for2 = new Fornecedor(true);
        var for3 = new Fornecedor(true);
        this.context.Fornecedores.AddRange(for1, for2, for3);
        this.context.SaveChanges();

    }
}
