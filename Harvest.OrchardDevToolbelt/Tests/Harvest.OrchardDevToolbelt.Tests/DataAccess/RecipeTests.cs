using System;
using System.Collections.Generic;
using Autofac;
using Harvest.OrchardDevToolbelt.Models;
using NUnit.Framework;
using Orchard.Data;
using Orchard.Tests;

namespace Harvest.OrchardDevToolbelt.Tests.DataAccess {
    [TestFixture]
    public class RecipeTests : DatabaseEnabledTestsBase {

        [Test]
        public void DataAccessDemo() {
            // Arrange
            var recipeRepository = _container.Resolve<IRepository<Recipe>>();
            var recipeCategoryRepository = _container.Resolve<IRepository<RecipeCategory>>();
            var ingredientRepository = _container.Resolve<IRepository<Ingredient>>();

            // Act
            var italianCategory = new RecipeCategory { Name = "Italian" };
            var chineseCategory = new RecipeCategory { Name = "Chinese" };

            recipeCategoryRepository.Create(italianCategory);
            recipeCategoryRepository.Create(chineseCategory);

            var recipe = new Recipe {
                Name = "Pizza",
                Category = italianCategory
            };

            recipeRepository.Create(recipe);

            var ingredient1 = new Ingredient {Name = "Pizza crust", Quantity = 1, Unit = QuantityUnit.Pieces };
            var ingredient2 = new Ingredient { Name = "Tomato sauce", Quantity = 0.2f, Unit = QuantityUnit.Liter };
            var ingredient3 = new Ingredient { Name = "Cheese", Quantity = 0.3f, Unit = QuantityUnit.Kilogram };
            var ingredient4 = new Ingredient { Name = "Spinach", Quantity = 0.01f, Unit = QuantityUnit.Kilogram };

            ingredientRepository.Create(ingredient1);
            ingredientRepository.Create(ingredient2);
            ingredientRepository.Create(ingredient3);
            ingredientRepository.Create(ingredient4);

            recipe.AddIngredient(ingredient1);
            recipe.AddIngredient(ingredient2);
            recipe.AddIngredient(ingredient3);
            recipe.AddIngredient(ingredient4);

            // Assert
            Assert.Greater(recipe.Id, 0);
            Assert.IsTrue(recipe.Ingredients.Contains(ingredient1));
        }

        protected override IEnumerable<Type> DatabaseTypes {
            get {
                yield return typeof(Recipe);
                yield return typeof(RecipeCategory);
                yield return typeof(Ingredient);
            }
        }

        public override void Register(ContainerBuilder builder) {
            
        }
    }
}