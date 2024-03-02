
namespace GestionReservation.Data;

public partial class Reservation
{
    public int IdReservation { get; set; }

    public string? CodeReservation { get; set; }

    public int? IdClient { get; set; }

    public int? IdVol { get; set; }

    public int? IdCompagnie { get; set; }

    public DateTime? DateReservation { get; set; }

    public string? Statut { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual CompagnieAerienne? IdCompagnieNavigation { get; set; }

    public virtual Vol? IdVolNavigation { get; set; }

    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
