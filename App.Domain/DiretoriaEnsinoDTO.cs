using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class DiretoriaEnsinoDTO
    {
        [Required]
        public int idDiretoriaEnsino { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "A descrição deve conter no máximo 40 caracteres")]
        public String descricaoDiretoriaEnsino { get; set; }
        [StringLength(8, ErrorMessage = "O campo cep deve conter 8 caracteres")]
        [Required]
        public String cep { get; set; }

        public DiretoriaEnsinoDTO(int id, String descricao, String cepDiretoria) 
        {
            this.idDiretoriaEnsino = id;
            this.descricaoDiretoriaEnsino = descricao;
            this.cep = cepDiretoria;
        }
        public DiretoriaEnsinoDTO() { }
    }
}
