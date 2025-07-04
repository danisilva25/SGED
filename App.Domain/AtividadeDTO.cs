using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class AtividadeDTO
    {
        [Required]
        public int idAtividade { get; set; }
        [Required]
        [StringLength(70, ErrorMessage = "O campo descrição deve conter no máximo 70 caracteres")]
        public String descricaoAtividade { get; set; }
        [Required]
        public DateTime dataInicioAtividade { get; set; }
        public DateTime? dataFinalAtividade { get; set; }
        [Required]
        public DisciplinaDTO disciplina { get; set; }

        public AtividadeDTO() { }

        public AtividadeDTO(int id, String descricao)
        {
            this.idAtividade = id;
            this.descricaoAtividade = descricao;
        }
    }
}
