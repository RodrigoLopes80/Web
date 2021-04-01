
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fornecedores.Models.Entidades
{
    [Table("Usuario")]
        public class Usuario
    {
        [Key]

        public int UserId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength (10)]
        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [MaxLength(60)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Este E-mail não é válido.")]
        [Display(Name = "E-mail", Description = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MaxLength(10)]
        public string Senha { get; set; }

        public bool Exclusao { get; set; }


        public IEnumerable<Usuario> GetUsuarios()
        {
            return new List<Usuario>() {
              new Usuario { UserId = 101, Nome = "Rodrigo Pires Lopes",
                                 Login = "Rlopes", Email = "drigo.plopes@gmail.com",
                                 Senha = "123456" }
                               };
        }
    }

}
