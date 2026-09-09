using LibroFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibroFacil.Infrastructure.Persistence;

public class LibroFacilDbContext : DbContext
{
    public DbSet<Libro> Libros { get; set; }

    public LibroFacilDbContext(DbContextOptions<LibroFacilDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Isbn).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Autor).IsRequired().HasMaxLength(150);
        });
    }
}