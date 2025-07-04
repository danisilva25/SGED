using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class UsuarioDTO
    {
        [Required]
        public int idUsuario { get; set; }
        [StringLength(100, ErrorMessage = "O nome deve conter no máximo 100 caracteres")]
        [Required]
        public String nomeUsuario { get; set; }
        [Required]
        public Int16 idade {get; set;}
        [Required]
        public String salt { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "O campo login deve conter no máximo 30 caracteres")]
        public String loginUsuario { get; set; }
        [Required]
        public String senhaUsuario { get; set; }
        [Required]
        public Int16 tipoUsuario { get; set; }
        public UsuarioDTO() { }

        public UsuarioDTO(int id, String nome)
        {
            this.idUsuario = id;
            this.nomeUsuario = nome;
        }
    }
}
