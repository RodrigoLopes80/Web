
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fornecedores.Models.Entidades
{
    [Table("Fornecedor")]
        public class Usuario
    {
        [Required]
        [Display(Name = "Id",Description ="Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Nome",Description = "Nome")]
        public string Nome { get; set; }

        [MaxLength(11)]
        [Display(Name = "Senha", Description = "Senha")]
        public string Senha { get; set; }
     
    } 

}
