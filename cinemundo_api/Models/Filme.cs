using System;
using System.Collections.Generic;

namespace cinemundo_api.Models;

public partial class Filme
{
    public int FilmeId { get; set; }

    public int? DiretorId { get; set; }

    public int? GeneroId { get; set; }

    public string FilmeTitulo { get; set; } = null!;

    public int? FilmeAno { get; set; }

    public int? FilmeDuracao { get; set; }

    public string? FilmeClassificacao { get; set; }

    public virtual Diretore? Diretor { get; set; }

    public virtual ICollection<Elenco> Elencos { get; set; } = new List<Elenco>();

    public virtual Genero? Genero { get; set; }
}
