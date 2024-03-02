using GestionReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionReservation.Data;


namespace GestionReservation.Controllers
{
    public class VolController : Controller
    {
        private readonly ApplicationDbContext context;

        public VolController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: VolController
        public ActionResult Index()
        {
            List<Vol> vols = context.Vols
                .Include(x=> x.IdCompagnieNavigation)
                .ToList();
            return View(vols);
        }

        // GET: VolController/Details/5
        // GET: VolController/Details/5
        public ActionResult Details(int id)
        {
            Vol vol = context.Vols
                .Include(x => x.IdCompagnieNavigation)
                .FirstOrDefault(x => x.IdVol == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }


        // GET: VolController/Create
        public ActionResult Create()
        {
            ViewBag.Compagnies = context.CompagnieAeriennes.ToList();
            return View();
        }

        // POST: VolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vol vol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(context.Vols.Where(x=> x.NumeroVol == vol.NumeroVol)
                        .Count() > 0)
                    {
                        ViewBag.Compagnies = "Le Vol existe deja";
                        return View(vol);
                    }
                    else
                    {
                        Vol vol1 = new Vol();
                        vol1.IdCompagnie = vol.IdCompagnie;
                        vol1.NumeroVol = vol.NumeroVol;
                        vol1.DateDepart = vol.DateDepart;
                        vol1.DateArrivee = vol.DateArrivee;
                        vol1.HeureArrivee = vol.HeureArrivee;
                        vol1.HeureDepart = vol.HeureDepart;
                        vol1.Destination = vol.Destination;
                        vol1.AvionUtilise = vol.AvionUtilise;
                        vol1.Depart = vol.Depart;
                        context.Vols.Add(vol1);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));

                    }
                }
                return View(vol);
            }
            catch
            {
                return View();
            }
        }

        // GET: VolController/Edit/5
        public ActionResult Edit(int id)
        {
            Vol vol = context.Vols.Find(id);
            if (vol == null)
            {
                return NotFound();
            }

            ViewBag.Compagnies = context.CompagnieAeriennes.ToList();
            return View(vol);
        }

        // POST: VolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Vol updatedVol)
        {
            try
            {
                if (id != updatedVol.IdVol)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var vol = context.Vols.Find(id);
                    if (vol == null)
                    {
                        return NotFound();
                    }

                    vol.IdCompagnie = updatedVol.IdCompagnie;
                    vol.NumeroVol = updatedVol.NumeroVol;
                    vol.DateDepart = updatedVol.DateDepart;
                    vol.DateArrivee = updatedVol.DateArrivee;
                    vol.HeureArrivee = updatedVol.HeureArrivee;
                    vol.HeureDepart = updatedVol.HeureDepart;
                    vol.Destination = updatedVol.Destination;
                    vol.AvionUtilise = updatedVol.AvionUtilise;
                    vol.Depart = updatedVol.Depart;

                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Compagnies = context.CompagnieAeriennes.ToList();
                return View(updatedVol);
            }
            catch
            {
                return View();
            }
        }

        // GET: VolController/Delete/5
        public ActionResult Delete(int id)
        {
            Vol vol = context.Vols.Find(id);

            return View(vol);
        }

        // POST: CompagnieAerienneController/Delete/5
        public ActionResult DeleteConfirmed(int id, Vol vol)
        {
            context.Vols.Remove(vol);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
