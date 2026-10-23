using System;
using System.Collections.Generic;

namespace cinemundo_api.Models;

public partial class Elenco
{
    public int ElencoId { get; set; }

    public int FilmeId { get; set; }

    public int AtorId { get; set; }

    public string? ElencoPersonagens { get; set; }

    public virtual Atore Ator { get; set; } = null!;

    public virtual Filme Filme { get; set; } = null!;
}
