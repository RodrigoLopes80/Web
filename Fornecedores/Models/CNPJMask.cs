using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fornecedores.Models
{
    public static class CNPJMask
    {
        public static string FormatToCnpj(this string value)
        {
           if (value.Length == 14)
                return String.Format("{0:00.000.000/0000-00}", long.Parse(value));
            else
                return value;
        }
    }
}
