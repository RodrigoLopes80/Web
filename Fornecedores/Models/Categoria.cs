using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fornecedores.Models
{
      public enum Categoria
      {
      [Display(Name = "Games")]
      Games,
      [Display(Name = "Eletrônicos")]
      Eletronicos,
      [Display(Name = "Vestuario")]
      Vestuario,
      [Display(Name = "Automotivo")]
      Automotivo,      
      [Display(Name = "Alimentação")]
      Alimentacao,
      }
}
