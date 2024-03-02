using GestionReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionReservation.Data;

namespace GestionReservation.Controllers
{
    public class CompagnieController : Controller
    {
        private readonly ApplicationDbContext context;

        public CompagnieController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: CompagnieController
        public ActionResult Index()
        {
            List<CompagnieAerienne> compagnieAeriennes = context.CompagnieAeriennes.ToList();
            return View(compagnieAeriennes);
        }

        // GET: CompagnieController/Details/5
        // GET: CompagnieController/Details/5
        public ActionResult Details(int id)
        {
            CompagnieAerienne compagnieAerienne = context.CompagnieAeriennes.Find(id);
            if (compagnieAerienne == null)
            {
                return NotFound();
            }

            return View(compagnieAerienne);
        }


        // GET: CompagnieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompagnieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompagnieAerienne compagnieAerienne)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(context.CompagnieAeriennes
                        .Where(x=> x.Nom == compagnieAerienne.Nom).Count() > 0)
                    {
                        ViewBag.CompagnieAeriennes = "Cette Compagnie existe deja";
                        return View(compagnieAerienne);
                    }
                    else
                    {
                        CompagnieAerienne compagnieAerienne1 = new CompagnieAerienne();
                        compagnieAerienne1.Nom = compagnieAerienne.Nom;
                        compagnieAerienne1.FlotteDAvions = compagnieAerienne.FlotteDAvions;
                        context.CompagnieAeriennes.Add(compagnieAerienne1);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));

                    }
                }
                return View(compagnieAerienne);

            }
            catch
            {
                return View();
            }
        }

        // GET: CompagnieController/Edit/5
        public ActionResult Edit(int id)
        {
            CompagnieAerienne compagnieAerienne = context.CompagnieAeriennes.Find(id);
            if (compagnieAerienne == null)
            {
                return NotFound();
            }

            return View(compagnieAerienne);
        }

        // POST: CompagnieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CompagnieAerienne updatedCompagnie)
        {
            try
            {
                if (id != updatedCompagnie.IdCompagnie)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var compagnie = context.CompagnieAeriennes.Find(id);
                    if (compagnie == null)
                    {
                        return NotFound();
                    }

                    compagnie.Nom = updatedCompagnie.Nom;
                    compagnie.FlotteDAvions = updatedCompagnie.FlotteDAvions;

                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                return View(updatedCompagnie);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            CompagnieAerienne compagnieAerienne = context.CompagnieAeriennes.Find(id);

            return View(compagnieAerienne);
        }

        // POST: CompagnieAerienneController/Delete/5
        public ActionResult DeleteConfirmed(int id, CompagnieAerienne compagnieAerienne)
        {
            context.CompagnieAeriennes.Remove(compagnieAerienne);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
