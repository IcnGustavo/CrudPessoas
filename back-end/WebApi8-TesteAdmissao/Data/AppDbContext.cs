using Microsoft.EntityFrameworkCore;
using WebApi8_TesteAdmissao.Models;

namespace WebApi8_TesteAdmissao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<PessoaModel> Pessoas { get; set; }

        public DbSet<EnderecoModel> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PessoaModel>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");
        }


    }
}
