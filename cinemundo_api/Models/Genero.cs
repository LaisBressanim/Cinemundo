using System;
using System.Collections.Generic;

namespace cinemundo_api.Models;

public partial class Genero
{
    public int GeneroId { get; set; }

    public string GeneroNome { get; set; } = null!;

    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();
}
