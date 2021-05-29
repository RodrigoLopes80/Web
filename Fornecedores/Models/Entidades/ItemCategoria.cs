
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fornecedores.Models.Entidades
{
    [Table("ItemCategoria")]
    public class ItemCategoria
    {
    
        [Key]
        public int CatId { get; set; }

        public string CatNome { get; set; }

       
    }

}