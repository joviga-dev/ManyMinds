using AutoMapper;
using ManyMinds.Data;
using ManyMinds.Models;
using ManyMindsApi.Data.Dto.Produto;
using ManyMindsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SystemLogController : ControllerBase
{
    private readonly ManyMindsApiContext context;
    private readonly IMapper mapper;

    public SystemLogController(ManyMindsApiContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

}
