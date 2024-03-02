using System;
using System.Collections.Generic;

namespace GestionReservation.Data;

public partial class SiegeDisponible
{
    public int IdSiege { get; set; }

    public int? IdVol { get; set; }

    public string? CodeSiege { get; set; }

    public bool? Disponibilite { get; set; }

    public string? Classe { get; set; }

    public decimal? Prix { get; set; }

    public virtual Vol? IdVolNavigation { get; set; }
}
