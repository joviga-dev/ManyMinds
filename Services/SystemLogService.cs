using ManyMinds.Data;
using ManyMindsApi.Models;

namespace ManyMindsApi.Services;

public interface ISystemLogService
{
    void AdicionaLog(string Tipo, string EntidadeAfetada,int EntidadeId, DateTime DateTime, string Detalhes);
}

public class SystemLogService : ISystemLogService
{
    private readonly ManyMindsApiContext context;

    public SystemLogService(ManyMindsApiContext context)
    {
        this.context = context;
    }

    public void AdicionaLog(string Tipo, string EntidadeAfetada, int EntidadeId, DateTime DateTime, string Detalhes)
    {
        this.context.Logs.Add(new SystemLog(Tipo, EntidadeAfetada, EntidadeId, DateTime, Detalhes));
        this.context.SaveChanges();
    }
}