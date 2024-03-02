using GestionReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionReservation.Data;


namespace GestionReservation.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext context;

        public ClientController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: ClientController
        public ActionResult Index()
        {
            List<Client> clients = context.Clients.ToList();
            return View(clients);
        }

        // GET: ClientController/Details/5
        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            Client client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(context.Clients.Where(x=>x.Email == client.Email)
                        .Count() > 0)
                    {
                        ViewBag.Clients = "cet email est deja existant";
                        return View(client);
                    }
                    else
                    {
                        Client client1 = new Client();
                        client1.Email = client.Email;
                        client1.Prenom = client.Prenom;
                        client1.Nom= client.Nom;
                        client1.InformationsPaiement = client.InformationsPaiement;
                        client1.MotDePasse = client.MotDePasse;
                        context.Clients.Add(client1);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));

                    }
                }
                return View(client);
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            Client client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Client updatedClient)
        {
            try
            {
                if (id != updatedClient.IdClient)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var client = context.Clients.Find(id);
                    if (client == null)
                    {
                        return NotFound();
                    }

                    client.Email = updatedClient.Email;
                    client.Prenom = updatedClient.Prenom;
                    client.Nom = updatedClient.Nom;
                    client.InformationsPaiement = updatedClient.InformationsPaiement;
                    client.MotDePasse = updatedClient.MotDePasse;

                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                return View(updatedClient);
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            Client client = context.Clients.Find(id);

            return View(client);
        }

        // POST: CompagnieAerienneController/Delete/5
        public ActionResult DeleteConfirmed(int id, Client client)
        {
            context.Clients.Remove(client);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
