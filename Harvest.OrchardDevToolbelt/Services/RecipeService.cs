using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Harvest.OrchardDevToolbelt.Models;
using Orchard;
using Orchard.Data;

namespace Harvest.OrchardDevToolbelt.Services {

    public interface IRecipeService : IDependency {
        Recipe CreateRecipe(string name, RecipeCategory category, params Ingredient[] ingredients);
        IEnumerable<Recipe> GetAllRecipes();
        IEnumerable<Recipe> FindRecipes(Expression<Func<Recipe, bool>> predicate);
        Recipe GetRecipeByName(string name);
    }
    
    public class RecipeService : IRecipeService {
        private readonly IRepository<Recipe> _recipeRepository;

        public RecipeService(IRepository<Recipe> recipeRepository) {
            _recipeRepository = recipeRepository;
        }

        public Recipe CreateRecipe(string name, RecipeCategory category, params Ingredient[] ingredients) {
            var recipe = new Recipe {Name = name, Category = category};

            foreach (var ingredient in ingredients) {
                recipe.AddIngredient(ingredient);
            }

            _recipeRepository.Create(recipe);
            return recipe;
        }

        public IEnumerable<Recipe> GetAllRecipes() {
            return _recipeRepository.Table.ToList();
        }

        public IEnumerable<Recipe> FindRecipes(Expression<Func<Recipe, bool>> predicate) {
            return _recipeRepository.Fetch(predicate).ToList();
        }

        public Recipe GetRecipeByName(string name) {
            return _recipeRepository.Get(x => x.Name == name);
        }
    }

    
}