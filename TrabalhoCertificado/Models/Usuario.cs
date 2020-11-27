using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoCertificado.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Email obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        //[CAMPO IDENTITY]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Senha obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas não conhecidem")]
        [NotMapped]
        public string SenhaConfirmar { get; set; }

        //precisa colocar limite no nome ?
        [Required(ErrorMessage = "Nome obrigatório")]
        [MinLength(2, ErrorMessage = "Utilize ao menos 2 caracteres")]
        [MaxLength(250, ErrorMessage = "Utilize ao máximo 250 caracteres")]
        public string nome { get; set; }

        public string previlegios { get; set; } //admin ou usuario
        public bool ativado { get; set; }
    }
}
