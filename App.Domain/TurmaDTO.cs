using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class TurmaDTO
    {
        [Required]
        public int idTurma {get; set;}
        [Required]
        [StringLength(20, ErrorMessage = "A descrição deve conter no máximo 20 caracteres")]
        public String descricaoTurma { get; set; }

        public TurmaDTO() { }

        public TurmaDTO(int id, String descricao)
        {
            this.idTurma = id;
            this.descricaoTurma = descricao;
        }
    }
}
