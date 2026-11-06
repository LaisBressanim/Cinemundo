using System.Collections.Generic;
using System.Threading.Tasks;
using cinemundo_api.Data;
using cinemundo_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinemundo_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiretoresController : ControllerBase
{
    private readonly CinemaContext _context;

    public DiretoresController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Diretore>>> GetDiretores()
    {
        return await _context.Set<Diretore>()
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Diretore>> GetDiretore(int id)
    {
        var diretore = await _context.Set<Diretore>().FindAsync(id);

        if (diretore == null)
        {
            return NotFound();
        }

        return diretore;
    }

    [HttpPost]
    public async Task<ActionResult<Diretore>> PostDiretore(Diretore diretore)
    {
        var entity = new Diretore
        {
            DiretorNome = diretore.DiretorNome,
            DiretorPais = diretore.DiretorPais
        };

        _context.Set<Diretore>().Add(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível salvar o diretor." });
        }

        return CreatedAtAction(nameof(GetDiretore), new { id = entity.DiretorId }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDiretore(int id, Diretore diretore)
    {
        if (id != diretore.DiretorId)
        {
            return BadRequest(new { message = "O id da rota deve ser igual ao DiretorId do corpo." });
        }

        var entity = await _context.Set<Diretore>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.DiretorNome = diretore.DiretorNome;
        entity.DiretorPais = diretore.DiretorPais;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível atualizar o diretor." });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiretore(int id)
    {
        var entity = await _context.Set<Diretore>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<Diretore>().Remove(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível excluir o diretor." });
        }

        return NoContent();
    }
}
