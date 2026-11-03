using Microsoft.EntityFrameworkCore;
// using OCCODE_CON.Models; // Asegúrate de importar tus modelos

namespace OCCODE_CON.Data
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options)
        {
        }

        // Aquí irán tus DbSets, por ejemplo:
        // public DbSet<Usuario> Usuarios { get; set; }
        // public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ¡CLAVE! Le decimos a EF Core que use el esquema 'seguridad' por defecto
            modelBuilder.HasDefaultSchema("seguridad");

            // Si necesitas mapeos específicos de llaves o relaciones, van aquí.
            // Ejemplo para evitar problemas de nombres en plural:
            // modelBuilder.Entity<Usuario>().ToTable("Usuario"); 
        }
    }
}