using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoCertificado.Models
{
    public class TipoAtividade
    {
        [Display(Name = "Identificador")]
        public int ID { get; set; }

        [Display(Name = "Tipo nome da atividade")]
        [Required(ErrorMessage = "É necessário adicionar um nome ao tipo de atividade")]
        public string NomeAtividade { get; set; }

        [Display(Name = "Descrição da atividade")]
        public string DescAtividade { get; set; }

    }
}
