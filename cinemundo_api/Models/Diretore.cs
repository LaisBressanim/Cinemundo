using System;
using System.Collections.Generic;

namespace cinemundo_api.Models;

public partial class Diretore
{
    public int DiretorId { get; set; }

    public string DiretorNome { get; set; } = null!;

    public string? DiretorPais { get; set; }

    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();
}
