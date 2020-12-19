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
        [MaxLength(100, ErrorMessage = "Quantidade máxima de caracteres estourada, que é máximo de cem caracteres.")]
        [Required(ErrorMessage = "Nome obrigatório")]
        [Display(Name ="Nome da atividade")]
        public string nome { get; set; }

        [Display(Name = "Tipo de atividade (Curso, Projeto, etc.)")]
        [Required(ErrorMessage = "É necessário definir o tipo de projeto")]
        public List<TipoAtividade> atividade { get; set; }
        [MaxLength(250, ErrorMessage = "Quantidade máxima de caracteres estourada, que é máximo de duzentos e cinquenta caracteres.")]
        [Display(Name = "Descrição da atividade")]
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
        [DataType(DataType.Upload)]
        public string anexo { get; set; }

    }
}
