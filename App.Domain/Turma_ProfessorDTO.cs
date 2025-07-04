using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class Turma_ProfessorDTO
    {
        [Required]
        public int idTurma_Professor { get; set; }
        [Required]
        public TurmaDTO turma { get; set; }
        [Required]
        public ProfessorDTO professor { get; set; }

        public Turma_ProfessorDTO() { }
    }
}
