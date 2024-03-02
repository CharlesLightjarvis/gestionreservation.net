
namespace GestionReservation.Data;

public partial class Client
{
    public int IdClient { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public string? MotDePasse { get; set; }

    public string? InformationsPaiement { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
