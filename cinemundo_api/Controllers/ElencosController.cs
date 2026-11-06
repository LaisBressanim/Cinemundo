using System.Collections.Generic;
using System.Threading.Tasks;
using cinemundo_api.Data;
using cinemundo_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinemundo_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElencosController : ControllerBase
{
    private readonly CinemaContext _context;

    public ElencosController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Elenco>>> GetElencos()
    {
        return await _context.Set<Elenco>()
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Elenco>> GetElenco(int id)
    {
        var elenco = await _context.Set<Elenco>().FindAsync(id);

        if (elenco == null)
        {
            return NotFound();
        }

        return elenco;
    }

    [HttpPost]
    public async Task<ActionResult<Elenco>> PostElenco(Elenco elenco)
    {
        if (elenco.FilmeId <= 0)
        {
            return BadRequest(new { message = "FilmeId deve ser positivo." });
        }

        if (elenco.AtorId <= 0)
        {
            return BadRequest(new { message = "AtorId deve ser positivo." });
        }

        var filme = await _context.Set<Filme>().FindAsync(elenco.FilmeId);

        if (filme == null)
        {
            return BadRequest(new { message = "O FilmeId informado não existe." });
        }

        var ator = await _context.Set<Atore>().FindAsync(elenco.AtorId);

        if (ator == null)
        {
            return BadRequest(new { message = "O AtorId informado não existe." });
        }

        var duplicate = await _context.Set<Elenco>()
            .AnyAsync(item => item.FilmeId == elenco.FilmeId && item.AtorId == elenco.AtorId);

        if (duplicate)
        {
            return BadRequest(new { message = "Já existe um elenco para este filme e ator." });
        }

        var entity = new Elenco
        {
            FilmeId = elenco.FilmeId,
            AtorId = elenco.AtorId,
            ElencoPersonagens = elenco.ElencoPersonagens
        };

        _context.Set<Elenco>().Add(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível salvar o elenco." });
        }

        return CreatedAtAction(nameof(GetElenco), new { id = entity.ElencoId }, new
        {
            entity.ElencoId,
            entity.FilmeId,
            entity.AtorId,
            entity.ElencoPersonagens
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutElenco(int id, Elenco elenco)
    {
        if (id != elenco.ElencoId)
        {
            return BadRequest(new { message = "O id da rota deve ser igual ao ElencoId do corpo." });
        }

        var entity = await _context.Set<Elenco>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        if (elenco.FilmeId <= 0)
        {
            return BadRequest(new { message = "FilmeId deve ser positivo." });
        }

        if (elenco.AtorId <= 0)
        {
            return BadRequest(new { message = "AtorId deve ser positivo." });
        }

        var filme = await _context.Set<Filme>().FindAsync(elenco.FilmeId);

        if (filme == null)
        {
            return BadRequest(new { message = "O FilmeId informado não existe." });
        }

        var ator = await _context.Set<Atore>().FindAsync(elenco.AtorId);

        if (ator == null)
        {
            return BadRequest(new { message = "O AtorId informado não existe." });
        }

        var duplicate = await _context.Set<Elenco>()
            .AnyAsync(item => item.ElencoId != id
                && item.FilmeId == elenco.FilmeId
                && item.AtorId == elenco.AtorId);

        if (duplicate)
        {
            return BadRequest(new { message = "Já existe um elenco para este filme e ator." });
        }

        entity.FilmeId = elenco.FilmeId;
        entity.AtorId = elenco.AtorId;
        entity.ElencoPersonagens = elenco.ElencoPersonagens;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível atualizar o elenco." });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteElenco(int id)
    {
        var entity = await _context.Set<Elenco>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<Elenco>().Remove(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível excluir o elenco." });
        }

        return NoContent();
    }
}
