using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaAPIController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPizzs()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<PizzaModel> pizze = db.Pizza.ToList();
                return Ok(pizze);
            }
        }

        [HttpGet("{id}")]
        public IActionResult SearchById(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaToSearch = db.Pizza.Where(pizze => pizze.Id == id).FirstOrDefault();

                if (pizzaToSearch != null)
                {
                    return Ok(pizzaToSearch);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult SearchByNomePizza(string nomePizza)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaToSearch = db.Pizza.Where(pizze => pizze.NomePizza.Contains(nomePizza)).FirstOrDefault(); // == solo stringa

                if (pizzaToSearch != null)
                {
                    return Ok(pizzaToSearch);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PizzaModel pizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                using (PizzaContext db = new PizzaContext())
                {
                    PizzaModel savedPizza = db.Pizza.Add(pizza).Entity;
                    db.SaveChanges();

                    return Create(savedPizza);

                }
            }
        }







    }
}
