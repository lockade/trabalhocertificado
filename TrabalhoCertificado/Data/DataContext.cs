using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoCertificado.Models;

namespace TrabalhoCertificado.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //public DbSet<Site> TBSite { get; set; }
        public DbSet<Usuario> TBUsuario { get; set; }
        public DbSet<Atividade> TBAtividades { get; set; }
        public DbSet<TipoAtividade> TBTiposAtividades { get; set; }
        public DbSet<RecuperarSenhaLinks> TBRecuperarSenhaLinks { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(b => b.Email)
                .IsUnique();

            modelBuilder.Entity<RecuperarSenhaLinks>()
                .HasKey(b => b.IDEncry);
        }

    }
}
