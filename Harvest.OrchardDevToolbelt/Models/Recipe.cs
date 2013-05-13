using System.Collections.Generic;

namespace Harvest.OrchardDevToolbelt.Models {
    public class Recipe {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual RecipeCategory Category { get; set; } // e.g. Italian, French, Chinese
        public virtual IList<Ingredient> Ingredients { get; protected set; }

        public Recipe() {
            Ingredients = new List<Ingredient>();
        }

        public virtual void AddIngredient(Ingredient ingredient) {
            ingredient.Recipe = this;
            Ingredients.Add(ingredient);
        }
    }
}