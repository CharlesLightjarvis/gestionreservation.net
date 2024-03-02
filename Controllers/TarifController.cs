using GestionReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionReservation.Data;


namespace GestionReservation.Controllers
{
    public class TarifController : Controller
    {
        private readonly ApplicationDbContext context;

        public TarifController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: TarifController
        public ActionResult Index()
        {
            List<Tarif> tarifs = context.Tarifs
                .Include(x => x.IdVolNavigation)
                .ToList();
            return View(tarifs);
        }

        // GET: TarifController/Details/5
        public ActionResult Details(int id)
        {
            Tarif tarif = context.Tarifs
                .Include(x => x.IdVolNavigation)
                .FirstOrDefault(x => x.IdTarif == id);
            if (tarif == null)
            {
                return NotFound();
            }

            return View(tarif);
        }

        // GET: TarifController/Create
        public ActionResult Create()
        {
            ViewBag.Vols = context.Vols.ToList();
            return View();
        }

        // POST: TarifController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarif tarif)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Tarif tarif1 = new Tarif();
                    tarif1.IdVol = tarif.IdVol;
                    tarif1.Classe = tarif.Classe;
                    tarif1.PrixBase = tarif.PrixBase;
                    tarif1.PolitiqueAnnulation = tarif.PolitiqueAnnulation;
                    context.Tarifs.Add(tarif1);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return View(tarif);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TarifController/Edit/5
        public ActionResult Edit(int id)
        {
            Tarif tarif = context.Tarifs.Find(id);
            if (tarif == null)
            {
                return NotFound();
            }

            ViewBag.Vols = context.Vols.ToList();
            return View(tarif);
        }

        // POST: TarifController/Edit/5
        // POST: TarifController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarif updatedTarif)
        {
            try
            {
                if (id != updatedTarif.IdTarif)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var tarif = context.Tarifs.Find(id);
                    if (tarif == null)
                    {
                        return NotFound();
                    }

                    tarif.IdVol = updatedTarif.IdVol;
                    tarif.Classe = updatedTarif.Classe;
                    tarif.PrixBase = updatedTarif.PrixBase;
                    tarif.PolitiqueAnnulation = updatedTarif.PolitiqueAnnulation;

                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Vols = context.Vols.ToList();
                return View(updatedTarif);
            }
            catch
            {
                return View();
            }
        }


        // GET: TarifController/Delete/5
        public ActionResult Delete(int id)
        {
            Tarif tarif = context.Tarifs.Find(id);

            return View(tarif);
        }

        // POST: CompagnieAerienneController/Delete/5
        public ActionResult DeleteConfirmed(int id, Tarif tarif)
        {
            context.Tarifs.Remove(tarif);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
