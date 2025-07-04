using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class Turma_DisciplinaDTO
    {
        [Required]
        public int idTurma_Disciplina { get; set; }
        [Required]
        public TurmaDTO turma { get; set; }
        [Required]
        public DisciplinaDTO disciplina { get; set; }

        public Turma_DisciplinaDTO() { }
    }
}
