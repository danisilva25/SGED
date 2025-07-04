using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGED.Domain
{
    public class EscolaDTO
    {
        [Required]
        public int idEscola { get; set; }
        [Required]
        public String nomeEscola { get; set; }
        [StringLength(80, ErrorMessage = "O campo escola deve conter no máximo 80 caracteres")]
        [Required]
        public String telefone { get; set; }
        [StringLength(8, ErrorMessage = "O campo telefone deve conter 11 caracteres")]
        [Required]
        public String cep { get; set; }
        [StringLength(8, ErrorMessage = "O cep deve conter no máximo 8 caracteres")]
        [Required]
        public DiretoriaEnsinoDTO diretoriaEnsino { get; set; }
        [Required]
        public Double idebAnosFinais { get; set; }
        [Required]
        public Double idebEnsinoMedio { get; set; }
        public EscolaDTO() { }

        public EscolaDTO(Int32 id, String nome)
        {
            this.idEscola = id;
            this.nomeEscola = nome;
        }
    }
}
