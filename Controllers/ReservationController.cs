using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionReservation.Data;


namespace GestionReservation.Controllers
{

    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext context;

        public ReservationController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: ReservationController
        public ActionResult Index()
        {
            List<Reservation> reservations = context.Reservations
                .Include(x =>x.IdClientNavigation)
                .Include(x => x.IdVolNavigation)
                .Include (x => x.IdCompagnieNavigation)
                .ToList();
            return View(reservations);
        }

        // GET: ReservationController/Details/5
        // GET: ReservationController/Details/5
        public ActionResult Details(int id)
        {
            Reservation reservation = context.Reservations
                .Include(x => x.IdClientNavigation)
                .Include(x => x.IdVolNavigation)
                .Include(x => x.IdCompagnieNavigation)
                .FirstOrDefault(x => x.IdReservation == id);

            if (reservation == null)
            {
                return NotFound(); // Gérer le cas où la réservation n'est pas trouvée
            }

            return View(reservation);
        }


        // GET: ReservationController/Create
        public ActionResult Create()
        {
            var clients = context.Clients.ToList(); // Ou tout autre moyen de récupérer les clients

            // Modifie la liste des clients pour inclure une propriété "NomComplet" qui contient le nom et le prénom concaténés
            var clientsAvecNomComplet = clients.Select(c => new {
                IdClient = c.IdClient,
                NomComplet = $"{c.Nom} {c.Prenom}" // Concaténation du nom et du prénom
            }).ToList();

            ViewBag.Clients = clientsAvecNomComplet;
            ViewBag.Vols = context.Vols.ToList();
            ViewBag.Compagnies = context.CompagnieAeriennes.ToList();
            return View();
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(context.Reservations.Where(x => x.CodeReservation == reservation.CodeReservation)
                        .Count() > 0)
                    {
                        ViewBag.Clients = "Reservation deja existante";
                        return View(reservation);
                    }
                    else
                    {
                        Reservation reservation1 = new Reservation();
                        reservation1.CodeReservation = reservation.CodeReservation;
                        reservation1.DateReservation = reservation.DateReservation;
                        reservation1.IdClient = reservation.IdClient;
                        reservation1.IdVol = reservation.IdVol;
                        reservation1.IdCompagnie = reservation.IdCompagnie;
                        reservation1.Statut = reservation.Statut;
                        context.Reservations.Add(reservation1);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));

                    }
                }
                return View(reservation);
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            var clients = context.Clients.ToList(); // Ou tout autre moyen de récupérer les clients

            // Modifie la liste des clients pour inclure une propriété "NomComplet" qui contient le nom et le prénom concaténés
            var clientsAvecNomComplet = clients.Select(c => new {
                IdClient = c.IdClient,
                NomComplet = $"{c.Nom} {c.Prenom}" // Concaténation du nom et du prénom
            }).ToList();

            ViewBag.Clients = clientsAvecNomComplet;
            ViewBag.Vols = context.Vols.ToList();
            ViewBag.Compagnies = context.CompagnieAeriennes.ToList();
            Reservation reservation = context.Reservations.Find(id);

            if (reservation == null)
            {
                return NotFound(); // Gérer le cas où la réservation n'est pas trouvée
            }

            return View(reservation);
        }

        // POST: ReservationController/Edit/5
        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Reservation updatedReservation)
        {
            if (id != updatedReservation.IdReservation)
            {
                return NotFound(); // Gérer le cas où l'ID ne correspond pas
            }

            if (ModelState.IsValid)
            {
                context.Entry(updatedReservation).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(updatedReservation);
        }


        public ActionResult Delete(int id)
        {
            Reservation reservation = context.Reservations.Find(id);

            return View(reservation);
        }

        // POST: CompagnieAerienneController/Delete/5
        public ActionResult DeleteConfirmed(int id, Reservation reservation)
        {
            context.Reservations.Remove(reservation);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
