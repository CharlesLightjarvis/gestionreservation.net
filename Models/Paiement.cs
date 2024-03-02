
namespace GestionReservation.Data;

public partial class Paiement
{
    public int IdPaiement { get; set; }

    public string? CodePaiement { get; set; }

    public int? IdReservation { get; set; }

    public decimal? Montant { get; set; }

    public DateTime? DatePaiement { get; set; }

    public string ModeDePaiement { get; set; } = null!;

    public virtual Reservation? IdReservationNavigation { get; set; }
}
