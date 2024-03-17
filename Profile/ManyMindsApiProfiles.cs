using AutoMapper;
using ManyMinds.Models;
using ManyMindsApi.Controllers;
using ManyMindsApi.Data.Dto.Login;
using ManyMindsApi.Data.Dto.PedidoCompra;
using ManyMindsApi.Data.Dto.Produto;
using ManyMindsApi.Data.Dto.Usuario;
using ManyMindsApi.Models;

namespace ManyMindsApi.Profiles
{
    public class ManyMindsApiProfiles : Profile
    {
        public ManyMindsApiProfiles()
        {
            CreateMap<AdicionarProdutoDto, Produto>();
            CreateMap<AtualizarProdutoDto, Produto>();
            CreateMap<Produto, AtualizarProdutoDto>();
            CreateMap<Produto, PesquisarProdutoDto>();
            CreateMap<Produto, ReativarProdutoDto>();
            CreateMap<ReativarProdutoDto, Produto>();

        }
    }

    public class PedidoCompraProfile : Profile
    {
        public PedidoCompraProfile()
        {
            CreateMap<AdicionarPedidoCompraDto, PedidoCompra>()
                .ForMember(dest => dest.Itens, opt => opt.Ignore()) 
            .ForMember(dest => dest.Fornecedor, opt => opt.Ignore());
            CreateMap<AtualizarPedidoCompraDto, PedidoCompra>();
            CreateMap<PedidoCompra, AtualizarPedidoCompraDto>();
            CreateMap<PedidoCompra, PesquisarPedidoCompraDto>();
        }
    }

    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<AdicionarUsuarioDto, Usuario>();
            CreateMap<LoginDto, Usuario>();
        }
    }
}
