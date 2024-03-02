using System;
using System.Collections.Generic;

namespace GestionReservation.Data;

public partial class Tarif
{
    public int IdTarif { get; set; }

    public int? IdVol { get; set; }

    public string? Classe { get; set; }

    public decimal? PrixBase { get; set; }

    public string? PolitiqueAnnulation { get; set; }

    public virtual Vol? IdVolNavigation { get; set; }
}
