using Models.Domein;
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


      
        Task<string> CreateDishe(DisheDTO model);
        Task<string> UpdateDishe(DisheDTO model);
       
        Task<string> CreateDrink(DrinkDTO model);
        Task<string> UpdateDrink(DrinkDTO model);
        Task<bool> DeleteDrink(string id);

        Task<List<DisheDTO>> GetAllDishes();
        Task<List<DrinkDTO>> GetAllDrinks();

        Task<bool> RelateDisheDrink(string disheId, string drinkId, DisheDrink relation);
        Task<(Dishe, List<Drink>)> GetDishSuggestionDrinks(string disheId, DisheDrink relation);

    }
}