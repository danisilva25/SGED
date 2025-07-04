using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class DisciplinaDTO
    {
        [Required]
        public int idDisciplina { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "A descrição deve conter no máximo 20 caracteres")]
        public String descricaoDisciplina { get; set; }

        public DisciplinaDTO() { }

        public DisciplinaDTO(int id, String descricao)
        {
            this.idDisciplina = id;
            this.descricaoDisciplina = descricao;
        }
    }
}
