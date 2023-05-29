using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LaMiaPizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (PizzaContext db = new())
            {
                List<PizzaModel> pizze = db.Pizza.ToList();
                return View(pizze);
            }

        }

        [HttpGet]
        public IActionResult SingolaPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaDettagli = db.Pizza.Where(pizzaDettagli => pizzaDettagli.Id == id).Include(pizzaDettagli => pizzaDettagli.Category).FirstOrDefault();

                if (pizzaDettagli != null)
                {
                    return View("SingolaPizza", pizzaDettagli);
                }
                else
                {
                    return NotFound($"La pizza con id {id} non è stata trovata");
                }
            }
        }


        [HttpGet]

        public IActionResult Privacy()
        {
            return View("Privacy1");
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return View("Contacts");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}