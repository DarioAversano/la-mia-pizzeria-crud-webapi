using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Models.ModelForView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LaMiaPizzeria.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<PizzaModel> pizze = db.Pizza.ToList();
                return View("Index", pizze);
            }

        }





        // AZIONE PER LA CREAZIONE DI UNA PIZZA SIMILE A QUELLA DELLA MODIFICA 
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Creazione()
        {
            using (PizzaContext context = new PizzaContext())
            {
                List<PizzaCategory> pizzacategories = context.pizzaCategories.ToList();
                PizzaListCategory model = new PizzaListCategory();
                model.Pizzas = new PizzaModel();
                model.PizzasCategories = pizzacategories;
                return View("Creazione", model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public IActionResult Creazione(PizzaListCategory data)
        {
            if (!ModelState.IsValid)
            {
                return View("Creazione", data);
            }

            using (PizzaContext context = new PizzaContext())
            {
                List<PizzaCategory> pizzaCategories = context.pizzaCategories.ToList();
                PizzaListCategory modelForView = new PizzaListCategory();
                modelForView.Pizzas = new PizzaModel();
                modelForView.PizzasCategories = pizzaCategories;

                context.Pizza.Add(data.Pizzas);
                context.SaveChanges();


                return RedirectToAction("Index");
            }

        }
        // azione COME CREAZIONE
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult modificaPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? PizzaModifica = db.Pizza.Where(Pizza => Pizza.Id == id).FirstOrDefault();
                return View("modificaPizza", PizzaModifica);
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreaNuovaPizza(PizzaModel nuovaPizza)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Creazione", nuovaPizza);
        //    }

        //    using (PizzaContext db = new PizzaContext())
        //    {
        //        db.Pizza.Add(nuovaPizza);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult modificaPizza(int id, PizzaModel pizzaModificata)
        {
            if (!ModelState.IsValid)
            {
                return View("modificaPizza", pizzaModificata);
            }

            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaDaModificare = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaModificare != null)
                {
                    pizzaDaModificare.NomePizza = pizzaModificata.NomePizza;
                    pizzaDaModificare.Descrizione = pizzaModificata.Descrizione;
                    pizzaDaModificare.Immagine = pizzaModificata.Immagine;
                    pizzaDaModificare.Prezzo = pizzaModificata.Prezzo;

                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound("La pizza non è stata trovata");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult eliminaPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaDaEliminare = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaEliminare != null)
                {
                    db.Remove(pizzaDaEliminare);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La pizza non è stata trovata");
                }

            }
        }







    }

}


