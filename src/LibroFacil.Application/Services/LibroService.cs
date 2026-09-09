using LibroFacil.Application.Interfaces;
using LibroFacil.Domain.Entities;
using LibroFacil.Domain.Exceptions;

namespace LibroFacil.Application.Services;

public class LibroService
{
    private readonly ILibroRepository _repository;

    public LibroService(ILibroRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Libro>> ObtenerTodosAsync() 
        => await _repository.ObtenerTodosAsync();

    public async Task<Libro?> ObtenerPorIdAsync(Guid id) 
        => await _repository.ObtenerPorIdAsync(id);

    public async Task<Libro> AgregarLibroAsync(string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        if (await _repository.ExisteIsbnAsync(isbn))
            throw new ReglaDominioException($"El ISBN '{isbn}' ya está registrado en el catálogo.");

        var libro = Libro.Crear(isbn, titulo, autor, anioPublicacion, stock);
        
        await _repository.AgregarAsync(libro);
        return libro;
    }

    public async Task ActualizarLibroAsync(Guid id, string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        var libro = await _repository.ObtenerPorIdAsync(id) 
            ?? throw new KeyNotFoundException($"No se encontró el libro con Id {id}.");

        if (libro.Isbn != isbn && await _repository.ExisteIsbnAsync(isbn))
            throw new ReglaDominioException($"El ISBN '{isbn}' ya está registrado en otro libro.");

        libro.ActualizarInformacion(isbn, titulo, autor, anioPublicacion);
        libro.ActualizarStock(stock);

        await _repository.ActualizarAsync(libro);
    }

    public async Task EliminarLibroAsync(Guid id)
    {
        var libro = await _repository.ObtenerPorIdAsync(id) 
            ?? throw new KeyNotFoundException($"No se encontró el libro con Id {id}.");
            
        await _repository.EliminarAsync(libro.Id);
    }
}