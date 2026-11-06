using System.Collections.Generic;
using System.Threading.Tasks;
using cinemundo_api.Data;
using cinemundo_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinemundo_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenerosController : ControllerBase
{
    private readonly CinemaContext _context;

    public GenerosController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
    {
        return await _context.Set<Genero>()
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Genero>> GetGenero(int id)
    {
        var genero = await _context.Set<Genero>().FindAsync(id);

        if (genero == null)
        {
            return NotFound();
        }

        return genero;
    }

    [HttpPost]
    public async Task<ActionResult<Genero>> PostGenero(Genero genero)
    {
        var entity = new Genero
        {
            GeneroNome = genero.GeneroNome
        };

        _context.Set<Genero>().Add(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível salvar o gênero." });
        }

        return CreatedAtAction(nameof(GetGenero), new { id = entity.GeneroId }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenero(int id, Genero genero)
    {
        if (id != genero.GeneroId)
        {
            return BadRequest(new { message = "O id da rota deve ser igual ao GeneroId do corpo." });
        }

        var entity = await _context.Set<Genero>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.GeneroNome = genero.GeneroNome;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível atualizar o gênero." });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenero(int id)
    {
        var entity = await _context.Set<Genero>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<Genero>().Remove(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível excluir o gênero." });
        }

        return NoContent();
    }
}
