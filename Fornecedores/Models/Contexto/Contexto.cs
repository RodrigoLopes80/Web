using Fornecedores.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fornecedores.Models.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto>option) : base(option)
        {
            Database.GetConnectionString();
        }

        public DbSet<Fornecedor> Fornecedor { get; set; }   
    }
}
