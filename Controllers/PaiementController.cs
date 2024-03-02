using GestionReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionReservation.Data;


namespace GestionReservation.Controllers
{
    public class PaiementController : Controller
    {
        private readonly ApplicationDbContext context;

        public PaiementController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: PaiementController
        public ActionResult Index()
        {
            List<Paiement> paiements = context.Paiements
                .Include(x =>x.IdReservationNavigation)
                .ToList();
            return View(paiements);
        }

        // GET: PaiementController/Details/5
        // GET: PaiementController/Details/5
        public ActionResult Details(int id)
        {
            Paiement paiement = context.Paiements
                .Include(x => x.IdReservationNavigation) // Si vous avez besoin des détails de la réservation
                .FirstOrDefault(p => p.IdPaiement == id);

            if (paiement == null)
            {
                return NotFound();
            }

            return View(paiement);
        }


        // GET: PaiementController/Create
        public ActionResult Create()
        {
            ViewBag.Reservations = context.Reservations.ToList();
            return View();
        }

        // POST: PaiementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paiement paiement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingPayment = context.Paiements.FirstOrDefault(x => x.CodePaiement == paiement.CodePaiement);

                    if (existingPayment != null)
                    {
                        ModelState.AddModelError("CodePaiement", "Ce Code de Paiement existe déjà.");
                        ViewBag.Reservations = context.Reservations.ToList();
                        return View(paiement);
                    }
                    else
                    {
                        context.Paiements.Add(paiement);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }
                ViewBag.Reservations = context.Reservations.ToList();
                return View(paiement);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de la création du paiement : " + ex.Message);
                ViewBag.Reservations = context.Reservations.ToList();
                return View(paiement);
            }
        }


        // GET: PaiementController/Edit/5
        public ActionResult Edit(int id)
        {
            Paiement paiement = context.Paiements.Find(id);

            if (paiement == null)
            {
                return NotFound();
            }

            ViewBag.Reservations = context.Reservations.ToList();
            return View(paiement);
        }

        // POST: PaiementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Paiement paiement)
        {
            try
            {
                if (id != paiement.IdPaiement)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    context.Entry(paiement).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Reservations = context.Reservations.ToList();
                return View(paiement);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de la modification du paiement : " + ex.Message);
                ViewBag.Reservations = context.Reservations.ToList();
                return View(paiement);
            }
        }


        // GET: PaiementController/Delete/5
        public ActionResult Delete(int id)
        {
            Paiement paiement = context.Paiements.Find(id);

            return View(paiement);
        }

        // POST: CompagnieAerienneController/Delete/5
        public ActionResult DeleteConfirmed(int id, Paiement paiement)
        {
            context.Paiements.Remove(paiement);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
