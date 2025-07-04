using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class ProfessorDTO : UsuarioDTO
    {
        [Required]
        [StringLength(11, ErrorMessage = "O cpf deve conter 11 caracteres")]
        public String cpf { get; set; }
        [StringLength(9, ErrorMessage = "O rg deve conter no máximo 9 caracteres")]
        [Required]
        public String rg { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "A matrícula deve conter no máximo 15 caracteres")]
        public String matricula { get; set; }
        [Required]
        public Char categoria { get; set; }
        [Required]
        [Range(0, 32, ErrorMessage = "A hora aula deve estar entre 0 a 32")]
        public Int16 horaAula { get; set; }
        [Required]
        public Boolean disponivel { get; set; }
        [Required]
        public DiretoriaEnsinoDTO diretoriaEnsino { get; set; }

        public ProfessorDTO() { }

        public ProfessorDTO(int id, String nome)
        {
            this.idUsuario = id;
            this.nomeUsuario = nome;
        }
    }
}
