using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tareasAPI.Models;
namespace tareasAPI.Data;

public class ApplicationDbContext : IdentityDbContext<Usuario>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Tarea> Tareas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Aquí puedes configurar otras relaciones y restricciones si es necesario
        builder.Entity<Usuario>()
            .HasMany(u => u.Tareas)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.UsuarioId)
            .IsRequired();
    }
    public DbSet<Rol> Roles { get; set; }
}
