using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ProjetoFinalPaola.Models
{
    [Table("Colaborador")]
    public class Colaborador
    {
        [Column("ColaboradorId")]
        [Display(Name = "Código do Colaborador")]
        public int ColaboradorId { get; set; }

        [Column("ColaboradorNome")]
        [Display(Name = "Nome do Colaborador")]
        public string ColaboradorNome { get; set; } = string.Empty;

        [Column("ColaboradorCpf")]
        [Display(Name = "Cpf")]
        public string ColaboradorCpf { get; set; } = string.Empty;

        [Column("ColaboradorSexo")]
        [Display(Name = "Sexo")]
        public string ColaboradorSexo { get; set; } = string.Empty;

        [Column("ColaboradorTelefone")]
        [Display(Name = "Telefone")]
        public string ColaboradorTelefone { get; set; } = string.Empty;

        [ForeignKey("CidadeId")]
        public int CidadeId { get; set; }
        public Cidade? Cidade { get; set; }

        [ForeignKey("TipoColaboradorId")]
        public int TipoColaboradorId { get; set; }

        [Display(Name = "Tipo do Colaborador")]
        public TipoColaborador? TipoColaborador { get; set; }

    }
}
