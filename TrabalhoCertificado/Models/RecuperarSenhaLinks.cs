using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoCertificado.Models
{
    
    public class RecuperarSenhaLinks
    {
        
        public string IDEncry { get; set; }

        public DateTime tempo { get; set; }

        
        [Required(ErrorMessage = "Email obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatório")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$", ErrorMessage = "Senha deve possuir no mínimo 8 caracteres, ao menos 1 letra maíuscula, 1 letra minúscula e 1 caractere especial")]
        [NotMapped]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Senha obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas não conhecidem")]
        [NotMapped]
        public string SenhaConfirmar { get; set; }
    }
}
