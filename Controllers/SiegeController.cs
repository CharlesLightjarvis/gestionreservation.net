using GestionReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionReservation.Data;


namespace GestionReservation.Controllers
{
    public class SiegeController : Controller
    {
        private readonly ApplicationDbContext context;

        public SiegeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: SiegeController
        public ActionResult Index()
        {
            List<SiegeDisponible> siegeDisponibles = context.SiegeDisponibles
                .Include(x =>x.IdVolNavigation)
                .ToList();
            return View(siegeDisponibles);
        }

        // GET: SiegeController/Details/5
        // GET: SiegeController/Details/5
        public ActionResult Details(int id)
        {
            SiegeDisponible siege = context.SiegeDisponibles
                .Include(x => x.IdVolNavigation)
                .FirstOrDefault(x => x.IdSiege == id);
            if (siege == null)
            {
                return NotFound();
            }

            return View(siege);
        }


        // GET: SiegeController/Create
        public ActionResult Create()
        {
            ViewBag.Vols = context.Vols.ToList();
            return View();
        }

        // POST: SiegeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SiegeDisponible siegeDisponible)
        {
            try
            {   if(ModelState.IsValid)
                {
                    if(context.SiegeDisponibles.Where(x => x.CodeSiege == siegeDisponible.CodeSiege)
                        .Count() > 0 )
                    {
                        ViewBag.Vols = "Code Siege deja existant";
                        return View(siegeDisponible);
                    }
                    else
                    {
                        SiegeDisponible siegeDisponible1 = new SiegeDisponible();
                        siegeDisponible1.Disponibilite = siegeDisponible.Disponibilite;
                        siegeDisponible1.IdVol = siegeDisponible.IdVol;
                        siegeDisponible1.CodeSiege = siegeDisponible.CodeSiege;
                        siegeDisponible1.Classe = siegeDisponible.Classe;
                        siegeDisponible1.Prix = siegeDisponible.Prix;
                        context.SiegeDisponibles.Add(siegeDisponible1);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));

                    }
                }
                ViewBag.Vols = context.Vols.ToList();
                return View(siegeDisponible);
            }
            catch
            {
                return View();
            }
        }

        // GET: SiegeController/Edit/5
        public ActionResult Edit(int id)
        {
            SiegeDisponible siege = context.SiegeDisponibles.Find(id);
            if (siege == null)
            {
                return NotFound();
            }

            ViewBag.Vols = context.Vols.ToList();
            return View(siege);
        }

        // POST: SiegeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SiegeDisponible updatedSiege)
        {
            try
            {
                if (id != updatedSiege.IdSiege)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var siege = context.SiegeDisponibles.Find(id);
                    if (siege == null)
                    {
                        return NotFound();
                    }

                    siege.Disponibilite = updatedSiege.Disponibilite;
                    siege.IdVol = updatedSiege.IdVol;
                    siege.CodeSiege = updatedSiege.CodeSiege;
                    siege.Classe = updatedSiege.Classe;
                    siege.Prix = updatedSiege.Prix;

                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Vols = context.Vols.ToList();
                return View(updatedSiege);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            SiegeDisponible siegeDisponible = context.SiegeDisponibles.Find(id);

            return View(siegeDisponible);
        }

        // POST: CompagnieAerienneController/Delete/5
        public ActionResult DeleteConfirmed(int id, SiegeDisponible siegeDisponible)
        {
            context.SiegeDisponibles.Remove(siegeDisponible);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
