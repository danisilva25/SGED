using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class Turma_EscolaDTO
    {
        [Required]
        public int idTurma_Escola { get; set; }
        [Required]
        public TurmaDTO turma { get; set; }
        [Required]
        public EscolaDTO escola { get; set; }

        public Turma_EscolaDTO() { }
    }
}
