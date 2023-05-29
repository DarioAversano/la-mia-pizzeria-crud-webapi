namespace LaMiaPizzeria.Models.ModelForView
{
    public class PizzaListCategory
    {
        public PizzaModel Pizzas { get; set; }

        public List<PizzaCategory>? PizzasCategories { get; set; }

        public PizzaListCategory() { }

        /*  public PizzaListCategory(List<PizzaCategory>? pizzaCategories)
          {
              this.Pizzas = new PizzaModel();
              this.PizzasCategories = pizzaCategories;
          }*/
    }
}