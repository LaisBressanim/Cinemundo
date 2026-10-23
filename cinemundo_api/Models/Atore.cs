using System;
using System.Collections.Generic;

namespace cinemundo_api.Models;

public partial class Atore
{
    public int AtorId { get; set; }

    public string AtorNome { get; set; } = null!;

    public string? AtorPais { get; set; }

    public virtual ICollection<Elenco> Elencos { get; set; } = new List<Elenco>();
}
