using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoCertificado.Models
{
    public class Atividade
    {
        [Display(Name = "Identificador")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Nome obrigatório")]
        [Display(Name ="Nome da atividade")]
        public string nome { get; set; }

        [Display(Name = "Tipo de atividade (Curso, Projeto, etc.)")]
        [Required(ErrorMessage = "É necessário definir o tipo de projeto")]
        TipoAtividade atividade { get; set; }
        public string descricao { get; set; }

        [Display(Name = "Data de início")]
        [Required(ErrorMessage = "É necessário adicionar a data de início.")]
        [DataType(DataType.Date)]
        public DateTime dataInicio { get; set; }

        [Display(Name = "Data de finalização")]
        [Required(ErrorMessage = "É necessário adicionar a data de finalização.")]
        [DataType(DataType.Date)]
        public DateTime dataFim { get; set; }

        [Display(Name = "Data de validade")]
        [DataType(DataType.Date)]
        public DateTime? DataValidade { get; set; }

        [Display(Name = "Anexo de arquivos e/ou imagens")]
        public string anexo { get; set; }

    }
}
