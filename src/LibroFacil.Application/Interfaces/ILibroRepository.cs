using LibroFacil.Domain.Entities;

namespace LibroFacil.Application.Interfaces;

public interface ILibroRepository
{
    Task<IEnumerable<Libro>> ObtenerTodosAsync();
    Task<Libro?> ObtenerPorIdAsync(Guid id);
    Task<bool> ExisteIsbnAsync(string isbn);
    Task AgregarAsync(Libro libro);
    Task ActualizarAsync(Libro libro);
    Task EliminarAsync(Guid id);
}