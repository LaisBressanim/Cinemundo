using System.Collections.Generic;
using System.Threading.Tasks;
using cinemundo_api.Data;
using cinemundo_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinemundo_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtoresController : ControllerBase
{
    private readonly CinemaContext _context;

    public AtoresController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Atore>>> GetAtores()
    {
        return await _context.Set<Atore>()
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Atore>> GetAtore(int id)
    {
        var atore = await _context.Set<Atore>().FindAsync(id);

        if (atore == null)
        {
            return NotFound();
        }

        return atore;
    }

    [HttpPost]
    public async Task<ActionResult<Atore>> PostAtore(Atore atore)
    {
        var entity = new Atore
        {
            AtorNome = atore.AtorNome,
            AtorPais = atore.AtorPais
        };

        _context.Set<Atore>().Add(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível salvar o ator." });
        }

        return CreatedAtAction(nameof(GetAtore), new { id = entity.AtorId }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAtore(int id, Atore atore)
    {
        if (id != atore.AtorId)
        {
            return BadRequest(new { message = "O id da rota deve ser igual ao AtorId do corpo." });
        }

        var entity = await _context.Set<Atore>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.AtorNome = atore.AtorNome;
        entity.AtorPais = atore.AtorPais;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível atualizar o ator." });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAtore(int id)
    {
        var entity = await _context.Set<Atore>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<Atore>().Remove(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível excluir o ator." });
        }

        return NoContent();
    }
}
