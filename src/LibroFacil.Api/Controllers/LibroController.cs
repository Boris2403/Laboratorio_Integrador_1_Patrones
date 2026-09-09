using LibroFacil.Api.DTOs;
using LibroFacil.Application.Services;
using LibroFacil.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibroFacil.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibrosController : ControllerBase
{
    private readonly LibroService _libroService;

    public LibrosController(LibroService libroService)
    {
        _libroService = libroService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var libros = await _libroService.ObtenerTodosAsync();
        return Ok(libros);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Consultar(Guid id)
    {
        var libro = await _libroService.ObtenerPorIdAsync(id);
        if (libro == null) return NotFound();
        return Ok(libro);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar([FromBody] LibroDto dto)
    {
        try
        {
            var libro = await _libroService.AgregarLibroAsync(
                dto.Isbn, dto.Titulo, dto.Autor, dto.AnioPublicacion, dto.Stock);
            
            return CreatedAtAction(nameof(Consultar), new { id = libro.Id }, libro);
        }
        catch (ReglaDominioException ex)
        {
            return BadRequest(new { error = ex.Message }); 
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] LibroDto dto)
    {
        try
        {
            await _libroService.ActualizarLibroAsync(
                id, dto.Isbn, dto.Titulo, dto.Autor, dto.AnioPublicacion, dto.Stock);
            
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); 
        }
        catch (ReglaDominioException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        try
        {
            await _libroService.EliminarLibroAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}