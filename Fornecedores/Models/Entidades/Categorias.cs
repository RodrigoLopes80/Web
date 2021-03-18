
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
        public class Categorias
    {
        [Required]
        [Display(Name = "Id",Description ="Código")]
        public int Id { get; set; }


        [MaxLength(50)]
        [Display(Name = "Nome",Description = "Categorias")]
        public string Nome { get; set; }

    }

}
