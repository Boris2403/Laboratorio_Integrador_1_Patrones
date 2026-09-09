using LibroFacil.Domain.Exceptions;

namespace LibroFacil.Domain.Entities;

public class Libro
{
    public Guid Id { get; private set; }
    public string Isbn { get; private set; } = string.Empty;
    public string Titulo { get; private set; } = string.Empty;
    public string Autor { get; private set; } = string.Empty;
    public int AnioPublicacion { get; private set; }
    public int Stock { get; private set; }

    private Libro() { }

    public static Libro Crear(string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        var libro = new Libro { Id = Guid.NewGuid() };
        libro.ActualizarInformacion(isbn, titulo, autor, anioPublicacion);
        libro.ActualizarStock(stock);
        return libro;
    }

    public void ActualizarInformacion(string isbn, string titulo, string autor, int anioPublicacion)
    {
        if (string.IsNullOrWhiteSpace(isbn)) 
            throw new ReglaDominioException("El ISBN es obligatorio.");
        
        if (string.IsNullOrWhiteSpace(titulo)) 
            throw new ReglaDominioException("El título es obligatorio.");
        
        if (string.IsNullOrWhiteSpace(autor)) 
            throw new ReglaDominioException("El autor es obligatorio.");

        int anioActual = DateTime.Now.Year;
        if (anioPublicacion <= 0 || anioPublicacion > anioActual)
            throw new ReglaDominioException($"El año de publicación debe ser mayor que 0 y no puede ser mayor al año actual ({anioActual}).");

        Isbn = isbn.Trim();
        Titulo = titulo.Trim();
        Autor = autor.Trim();
        AnioPublicacion = anioPublicacion;
    }

    public void ActualizarStock(int stock)
    {
        if (stock < 0) 
            throw new ReglaDominioException("El stock no puede ser negativo.");
        
        Stock = stock;
    }
}