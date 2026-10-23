
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cinemundo_api.Data;
using cinemundo_api.Models;

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
        return await _context.Filmes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Filme>> GetFilme(int id)
    {
        var filme = await _context.Filmes.FindAsync(id);

        if (filme == null)
            return NotFound();

        return filme;
    }
}
