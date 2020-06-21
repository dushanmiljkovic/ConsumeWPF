using Models.DTO;
using Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EatCodeDesktop.Helper
{
    public interface IAPIHelper
    {
        Task<string> Auth(string userName, string pass);
        Task<string> CreateRecipe(CreateRecipeRequestModel model);
        Task<string> UpdateRecipe(UpdateRecipeRequestModel model);
        Task<bool> DeleteRecipe(Guid guid);
        Task<List<RecipeDTO>> GetAllRecipes();
    }
}