using System.Collections.Generic;
using System.Threading.Tasks;
using cinemundo_api.Data;
using cinemundo_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinemundo_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmesController : ControllerBase
{
    private readonly CinemaContext _context;

    public FilmesController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes()
    {
        return await _context.Set<Filme>()
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Filme>> GetFilme(int id)
    {
        var filme = await _context.Set<Filme>().FindAsync(id);

        if (filme == null)
        {
            return NotFound();
        }

        return filme;
    }

    [HttpPost]
    public async Task<ActionResult<Filme>> PostFilme(Filme filme)
    {
        if (filme.DiretorId.HasValue)
        {
            var diretor = await _context.Set<Diretore>().FindAsync(filme.DiretorId.Value);

            if (diretor == null)
            {
                return BadRequest(new { message = "O DiretorId informado não existe." });
            }
        }

        if (filme.GeneroId.HasValue)
        {
            var genero = await _context.Set<Genero>().FindAsync(filme.GeneroId.Value);

            if (genero == null)
            {
                return BadRequest(new { message = "O GeneroId informado não existe." });
            }
        }

        var entity = new Filme
        {
            DiretorId = filme.DiretorId,
            GeneroId = filme.GeneroId,
            FilmeTitulo = filme.FilmeTitulo,
            FilmeAno = filme.FilmeAno,
            FilmeDuracao = filme.FilmeDuracao,
            FilmeClassificacao = filme.FilmeClassificacao
        };

        _context.Set<Filme>().Add(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível salvar o filme." });
        }

        return CreatedAtAction(nameof(GetFilme), new { id = entity.FilmeId }, new
        {
            entity.FilmeId,
            entity.DiretorId,
            entity.GeneroId,
            entity.FilmeTitulo,
            entity.FilmeAno,
            entity.FilmeDuracao,
            entity.FilmeClassificacao
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFilme(int id, Filme filme)
    {
        if (id != filme.FilmeId)
        {
            return BadRequest(new { message = "O id da rota deve ser igual ao FilmeId do corpo." });
        }

        var entity = await _context.Set<Filme>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        if (filme.DiretorId.HasValue)
        {
            var diretor = await _context.Set<Diretore>().FindAsync(filme.DiretorId.Value);

            if (diretor == null)
            {
                return BadRequest(new { message = "O DiretorId informado não existe." });
            }
        }

        if (filme.GeneroId.HasValue)
        {
            var genero = await _context.Set<Genero>().FindAsync(filme.GeneroId.Value);

            if (genero == null)
            {
                return BadRequest(new { message = "O GeneroId informado não existe." });
            }
        }

        entity.DiretorId = filme.DiretorId;
        entity.GeneroId = filme.GeneroId;
        entity.FilmeTitulo = filme.FilmeTitulo;
        entity.FilmeAno = filme.FilmeAno;
        entity.FilmeDuracao = filme.FilmeDuracao;
        entity.FilmeClassificacao = filme.FilmeClassificacao;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível atualizar o filme." });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilme(int id)
    {
        var entity = await _context.Set<Filme>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<Filme>().Remove(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { message = "Não foi possível excluir o filme." });
        }

        return NoContent();
    }
}
