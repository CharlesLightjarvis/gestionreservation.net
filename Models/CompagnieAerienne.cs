using System;
using System.Collections.Generic;

namespace GestionReservation.Data;

public partial class CompagnieAerienne
{
    public int IdCompagnie { get; set; }

    public string? Nom { get; set; }

    public string? FlotteDAvions { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Vol> Vols { get; set; } = new List<Vol>();
}
