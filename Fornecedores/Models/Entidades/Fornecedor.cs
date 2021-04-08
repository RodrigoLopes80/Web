
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
        public class Fornecedor
    {
        [Required]
        [Display(Name = "Id",Description ="Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Nome Fantasia",Description = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo Razão Social é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Razão Social", Description = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório")]
        [MaxLength(30)]
        [Display(Name = "Categoria", Description = "Categoria")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        [MaxLength(18)]
        [Display(Name = "CNPJ", Description = "CNPJ")]
        [UIHint("cnpj")]
        [ValidaCNPJ(ErrorMessage  = "O valor é inválido para CNPJ")]
        public string CNPJ { get; set; }

        [Display(Name = "Data do Cadastro", Description = "Data do Cadastro")]
        [Required(ErrorMessage = "O campo Data do Cadastro é obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataCadastro { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        [Display(Name = "Endereço", Description = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        [MaxLength(30)]
        [Display(Name = "Cidade", Description = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        [MaxLength(30)]
        [Display(Name = "Estado", Description = "Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo Responsável é obrigatório.")]
        [MaxLength(50)]        
        [Display(Name = "Responsável", Description = "Responsável")]
        public string Responsavel { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [MaxLength(14)]
        [UIHint("tel")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$",ErrorMessage = "Tipo de telefone não é válido.")]

        [Display(Name = "Telefone", Description = "Telefone")]
        public string Telefone { get; set; }    

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [MaxLength(60)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Este E-mail não é válido.")]
        [Display(Name = "E-mail", Description = "E-mail")]
        public string Email { get; set; }

        public bool Exclusao { get; set; }
     
    } 

}
