using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Fornecedores.Models.Entidades
{
    public class EmailModel
    {
        [Required(ErrorMessage = "O campo Destino é obrigatório.")]
        [Display(Name = "Destino", Description = "Destino")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Este E-mail não é válido.")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "O campo Assunto é obrigatório.")]
        [Display(Name = "Assunto", Description = "Assunto")]
        
        public string Assunto { get; set; }
        [Required(ErrorMessage = "O campo Mensagem é obrigatório.")]
        [Display(Name = "Mensagem", Description = "Mensagem")]
        public string Mensagem { get; set; }
    }
}

