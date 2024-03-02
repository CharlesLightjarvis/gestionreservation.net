using System;
using System.Collections.Generic;

namespace GestionReservation.Data;

public partial class Vol
{
    public int IdVol { get; set; }

    public DateTime? DateDepart { get; set; }

    public DateTime? DateArrivee { get; set; }

    public TimeSpan? HeureDepart { get; set; }

    public TimeSpan? HeureArrivee { get; set; }

    public string? Destination { get; set; }

    public string? AvionUtilise { get; set; }

    public int? IdCompagnie { get; set; }

    public string? NumeroVol { get; set; }

    public string? Depart { get; set; }

    public virtual CompagnieAerienne? IdCompagnieNavigation { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<SiegeDisponible> SiegeDisponibles { get; set; } = new List<SiegeDisponible>();

    public virtual ICollection<Tarif> Tarifs { get; set; } = new List<Tarif>();
}
