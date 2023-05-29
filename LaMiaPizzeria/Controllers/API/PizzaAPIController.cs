using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaAPIController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPizzas()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<PizzaModel> pizza = db.Pizza.ToList();
                return Ok(pizza);
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
        public IActionResult SearchByName(string nomePizza)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaToSearch = db.Pizza.Where(pizze => pizze.NomePizza.Contains(nomePizza)).FirstOrDefault(); // == solo INT ! stringa

                if (pizzaToSearch != null)
                {
                    return Ok(pizzaToSearch);
                }
                else
                {
                    return NotFound("Non ho trovato la pizza");
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
                    db.Pizza.Add(pizza);
                    db.SaveChanges();

                    return Ok();

                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaToDelete = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound("Non ho trovato la pizza da eliminare");

                }
            }
        }













    }
}
