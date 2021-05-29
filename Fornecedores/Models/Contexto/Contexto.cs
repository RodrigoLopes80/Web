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
        public Contexto(DbContextOptions<Contexto> option) : base(option)
        {
            Database.GetConnectionString();
        }
              
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<ItemCategoria> ItemCategorias { get; set; }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UserFornecedor> UserFornecedor { get; set; }


        //condição de soft delete no tela


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            {
                modelBuilder.Entity<Usuario>()
                   .HasQueryFilter(cad => EF.Property<bool>(cad, "Exclusao") == false);
           
            }

            {
                modelBuilder.Entity<UserFornecedor>()
                   .HasKey(x => new { x.UserId, x.Id });
            }

            {
                modelBuilder.Entity<Fornecedor>()
                   .HasQueryFilter(cad => EF.Property<bool>(cad, "Exclusao") == false);
            }
        }

        //atualização da coluna Exclusao para true ou false
        public async Task<int> SaveChangesAsync()
        {

            {
                UpdateSoftDeleteStatuses();
                return await base.SaveChangesAsync();
            }
        }


        //metodo para soft delete
        private void UpdateSoftDeleteStatuses()
        {
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["Exclusao"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["Exclusao"] = true;
                            break;
                    }
                }
            }
        }

    }
}

