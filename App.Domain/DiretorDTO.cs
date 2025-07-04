using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class DiretorDTO : UsuarioDTO
    {
        [Required]
        public EscolaDTO escola;

        public DiretorDTO() { }
    }
}
