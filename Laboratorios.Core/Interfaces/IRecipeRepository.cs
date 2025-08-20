using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipes();

        Task<Recipe> InsertRecipe(Recipe recipe);
        Task<string> ValidateRecipe(Recipe recipe);
        Task<ProductTrees> GetProductTree(string recipe);
    }
}
