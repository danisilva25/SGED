using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class AlunoDTO : UsuarioDTO
    {
        [Required]
        [StringLength(14, ErrorMessage = "O RA deve conter no máximo 14 caracteres")]
        public String ra { get; set; }
        [Required]
        public TurmaDTO turma { get; set; }
        [Required]
        public EscolaDTO escola { get; set; }

        public AlunoDTO() { }

        public AlunoDTO(int id, String nome)
        {
            this.idUsuario = id;
            this.nomeUsuario = nome;
        }
    }
}
