using LibroFacil.Application.Interfaces;
using LibroFacil.Domain.Entities;
using LibroFacil.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibroFacil.Infrastructure.Repositories;

public class LibroRepositoryEf : ILibroRepository
{
    private readonly LibroFacilDbContext _context;

    public LibroRepositoryEf(LibroFacilDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Libro>> ObtenerTodosAsync() 
        => await _context.Libros.ToListAsync();

    public async Task<Libro?> ObtenerPorIdAsync(Guid id) 
        => await _context.Libros.FindAsync(id);

    public async Task<bool> ExisteIsbnAsync(string isbn) 
        => await _context.Libros.AnyAsync(l => l.Isbn == isbn);

    public async Task AgregarAsync(Libro libro)
    {
        await _context.Libros.AddAsync(libro);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Libro libro)
    {
        _context.Libros.Update(libro);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(Guid id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro != null)
        {
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
        }
    }
}