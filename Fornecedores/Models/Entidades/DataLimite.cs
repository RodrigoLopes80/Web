using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fornecedores.Models.Entidades
{
  
        public class DataLimite : ValidationAttribute
        {
            public DataLimite()
            {

            }

            public override bool IsValid(object value)
            {
                var dt = (DateTime)value;
                if (dt < DateTime.Now)
                {
                    return true;
                }
                return false;
            }
        }
    }