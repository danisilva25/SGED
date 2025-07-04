using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class EmailDTO
    {
        [Required]
        public int idEmail { get; set; }
        [Required]
        [StringLength(70, ErrorMessage = "O campo email deve ter no máximo 70 caracteres")]
        public String descricaoEmail { get; set; }
        [Required]
        public EscolaDTO escola { get; set; }
        [Required]
        public UsuarioDTO usuario { get; set; }

        public EmailDTO() { }
    }
}
