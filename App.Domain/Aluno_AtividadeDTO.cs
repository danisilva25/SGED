using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class Aluno_AtividadeDTO
    {       
        public int idAluno_Disciplina { get; set; }

        [Required(ErrorMessage = "O campo nota é obrigatório")]
        [Range(0, 10, ErrorMessage = "O intervalo aceito é de 0 a 10")]
        public Int16 nota { get; set; }

        [Required]
        public AlunoDTO aluno { get; set; }

        [Required]
        public AtividadeDTO atividade { get; set; }

        public Aluno_AtividadeDTO() { }
    }
}
