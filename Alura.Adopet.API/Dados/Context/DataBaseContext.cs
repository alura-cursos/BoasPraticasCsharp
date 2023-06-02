using Alura.Adopet.API.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Alura.Adopet.API.Dados.Context
{
    public class DataBaseContext: DbContext
    {     
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options) {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }   
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pet> Pets { get; set; }

    }
}
