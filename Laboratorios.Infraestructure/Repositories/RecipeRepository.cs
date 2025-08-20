using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderTrack.Infraestructure.Data;


namespace Laboratorios.Infraestructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly LaboratoriosContext _context;
        static ServiceLayerCN serviceLayer = new ServiceLayerCN();

        public RecipeRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            try
            {
                var Recipes = await _context.Recipe.Include(x=>x.RecipeDetail).ToListAsync();
                return Recipes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<Recipe> InsertRecipe(Recipe recipe)
        {
            try
            {
                var validate = await ValidateRecipe(recipe);
                if (recipe.recipeId > 0)
                {
                    _context.Entry(recipe).State = EntityState.Modified;
                    if (recipe.isOriginal)
                    {
                        //Sincronizar y editar la receta original
                        var recipeDB = await _context.RecipeDetail.AsNoTracking().Where(x => x.recipeId == recipe.recipeId).ToListAsync();
                        foreach (var item in recipeDB)
                        {
                            var itemDB = recipe.RecipeDetail.Where(a => a.itemCode == item.itemCode).FirstOrDefault();
                            if (itemDB != null)
                            {
                                item.nOfTubes = itemDB.nOfTubes;
                                item.quantity = itemDB.quantity;
                                _context.Entry(item).State = EntityState.Modified;
                            }
                            else
                            {
                                _context.RecipeDetail.Remove(item);
                            }

                        }
                        foreach (var item in recipe.RecipeDetail)
                        {
                            var itemDB = recipeDB.Where(a => a.itemCode == item.itemCode).FirstOrDefault();
                            if (itemDB == null)
                            {
                                _context.RecipeDetail.Add(item);
                            }

                        }
                    }
                    else
                    {
                        //Editar las recetas duplicadas
                        foreach (var item in recipe.RecipeDetail)
                        {
                            if (item.recipeDetailId == 0 && item.included == true)
                            {
                                _context.RecipeDetail.Add(item);
                            }
                            else if (item.recipeDetailId > 0 && item.included == false)
                            {
                                _context.RecipeDetail.Remove(item);
                            }
                            else if (item.recipeDetailId > 0)
                            {
                                _context.Entry(item).State = EntityState.Modified;
                            }
                        }
                    }
                }
                else
                {
                    if (recipe.isOriginal == false)
                    {
                        recipe.RecipeDetail = recipe.RecipeDetail.Where(x => x.included == true).ToList();
                    }
                    _context.Recipe.Add(recipe);
                    
                }
                _context.SaveChanges();

                return recipe;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<string> ValidateRecipe(Recipe recipe)
        {
            try
            {
                var exists = await _context.Recipe.AsNoTracking().Where(x => x.name == recipe.name && x.recipeId != recipe.recipeId).FirstOrDefaultAsync();
                if(exists != null)
                {
                    throw new InvalidOperationException("Ya existe una receta con este nombre.");
                }
                if (recipe.isOriginal)
                {
                    var existsOriginal = await _context.Recipe.AsNoTracking().Where(x => x.treeCode == recipe.treeCode && x.isOriginal && x.recipeId != recipe.recipeId).FirstOrDefaultAsync();
                    if(existsOriginal != null)
                        throw new InvalidOperationException("Ya existe una receta Principal para esta configuración.");
                }
                if (recipe.isFav)
                {
                    var existsOriginal = await _context.Recipe.AsNoTracking().Where(x => x.treeCode == recipe.treeCode && x.isFav && x.matrixId == recipe.matrixId && x.analisysId == recipe.analisysId && x.recipeId != recipe.recipeId).FirstOrDefaultAsync();
                    if (existsOriginal != null)
                        throw new InvalidOperationException("Ya existe una receta Favorita para esta configuración.");
                }
                var jsonString = "";
                return jsonString;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<ProductTrees> GetProductTree(string treeCode)
        {
            try
            {
                if (serviceLayer.loginResponse.Token < DateTime.Now)
                {
                    serviceLayer.Login();
                }
                var productTrees = new ProductTrees();
                var path = String.Format(@"ProductTrees('{0}')", treeCode);
                serviceLayer.client.DefaultRequestHeaders.Add("Cookie", "B1SESSION=" + serviceLayer.loginResponse.SessionId + ";HttpOnly");
                HttpResponseMessage response = await serviceLayer.client.GetAsync(path);
                if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "401")
                {
                    serviceLayer = new ServiceLayerCN();
                    await GetProductTree(treeCode);
                }
                if (response != null)
                {
                    if (response.StatusCode.ToString() == "NotFound" || response.StatusCode.ToString() == "404")
                    {
                        throw new InvalidOperationException("No se encontro receta de referencia");
                    }
                    var jsonString = await response.Content.ReadAsStringAsync();
                    productTrees = JsonConvert.DeserializeObject<ProductTrees>(jsonString);

                }
                return productTrees;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
