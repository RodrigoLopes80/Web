using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fornecedores.Models
{
      public enum Categoria
      {
        [Display(Name = "Alimentação")]
        Alimentação,
        [Display(Name = "Automotivo")]
        Automotivo,
        [Display(Name = "Eletrônicos")]
        Eletrônicos,
        [Display(Name = "Games")]
        Games,
        [Display(Name = "Vestuário")]
        Vestuário,     
      }
}
