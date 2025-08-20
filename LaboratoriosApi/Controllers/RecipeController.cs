using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _RecipeRepository;

        public RecipeController(IRecipeRepository RecipeRepository)
        {
            _RecipeRepository = RecipeRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetRecipe()
        {
            try
            {
               var Recipe = await _RecipeRepository.GetRecipes();
               return Ok(Recipe);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertRecipe([FromBody] Recipe Recipe)
        {
            try
            {
                var user = await _RecipeRepository.InsertRecipe(Recipe);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("RecipeByRef")]
        public async Task<IActionResult> ValidateRecipe([FromBody]Recipe recipe)
        {
            try
            {
                var user = await _RecipeRepository.ValidateRecipe(recipe);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("GetProductTree")]
        public async Task<IActionResult> GetProductTree(string treeCode)
        {
            try
            {
                var user = await _RecipeRepository.GetProductTree(treeCode);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
