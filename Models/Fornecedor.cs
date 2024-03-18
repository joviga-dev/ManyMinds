using System.ComponentModel.DataAnnotations;

namespace ManyMindsApi.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        public bool Status { get; set; }

        public Fornecedor(bool Status) {
            this.Status = Status;
        }
    }
}