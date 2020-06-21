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


        // matrix
        Task<bool> CreateDishe(DisheDTO model);
        Task<bool> UpdateDishe(DisheDTO model);
        Task<bool> DeleteDishe(DisheDTO model);


        Task<bool> CreateDrink(DrinkDTO model);
        Task<bool> UpdateDrink(DrinkDTO model);
        Task<bool> DeleteDrink(DrinkDTO model);
    }
}