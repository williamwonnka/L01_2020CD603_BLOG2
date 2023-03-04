using L01_2020CD603.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2020CD603.Data
{
    public class SQLServerConsumer: DbContext
    {
        public SQLServerConsumer(DbContextOptions<SQLServerConsumer> options) : base(options) 
        {
        
        }

        public DbSet<Usuarios> usuarios { get; set; }

        public DbSet<Comentarios> comentarios { get; set; }
        public DbSet<Calificaciones> calificaciones { get; set; }

    }
}
