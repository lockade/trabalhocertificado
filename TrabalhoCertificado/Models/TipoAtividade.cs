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
        [MaxLength(100,ErrorMessage = "Quantidade máxima de caracteres estourada, que é máximo de cem caracteres.")]
        [Required(ErrorMessage = "É necessário adicionar um nome ao tipo de atividade")]
        public string NomeAtividade { get; set; }

        [MaxLength(250, ErrorMessage = "Quantidade máxima de caracteres estourada, que é máximo de duzentos e cinquenta caracteres.")]
        [Display(Name = "Descrição da atividade")]
        public string DescAtividade { get; set; }

        public int idUsuario { get; set; }
    }
}
