using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Data.Dto.SystemLog
{
    public class PesquisaSystemLogDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string EntidadeAfetada { get; set; }
        public int EntidadeId { get; set; }
        public DateTime DateTime { get; set; }
        public string Detalhes { get; set; }
    }
}
