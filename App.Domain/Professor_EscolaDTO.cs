using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class Professor_EscolaDTO
    {
        [Required]
        public int idProfessor_Escola { get; set; }
        [Required]
        public ProfessorDTO professor { get; set; }
        [Required]
        public EscolaDTO escola { get; set; }

        public Professor_EscolaDTO() { }
    }
}
