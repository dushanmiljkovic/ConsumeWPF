using Models.DTO;
using Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatCodeDesktop.Helper
{
    public static class ModelFactory
    {
        public static CreateRecipeRequestModel CreateRecipeRequestModel(RecipeDTO model)
        {
            var ingrids = new List<IngredientRequestModel>();
            foreach (var ingr in model.Ingredients) { ingrids.Add(CreateIngredientRequestModel(ingr)); }

            var nutritionDto = CreateNutritionRequestModel(model);

            var modelDto = new CreateRecipeRequestModel()
            {
                Name = model.Name,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                ReadyIn = model.ReadyIn,
                SkillRequired = model.SkillRequired,
                Serves = model.Serves,
                PreparationMethod = model.PreparationMethod.ToList(),
                Description = model.Description,
                Nutrition = nutritionDto,
                Ingredients = ingrids
            };
            return modelDto;
        }

        public static UpdateRecipeRequestModel UpdateRecipeRequestModel(RecipeDTO model)
        {

            var ingrids = new List<IngredientRequestModel>();
            foreach (var ingr in model.Ingredients)
            {
                ingrids.Add(CreateIngredientRequestModel(ingr));
            }

            var nutritionDto = CreateNutritionRequestModel(model);

            var modelDto = new UpdateRecipeRequestModel()
            {
                Id = model.Id,
                Name = model.Name,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                ReadyIn = model.ReadyIn,
                SkillRequired = model.SkillRequired,
                Serves = model.Serves,
                PreparationMethod = model.PreparationMethod.ToList(),
                Description = model.Description,
                Nutrition = nutritionDto,
                Ingredients = ingrids
            };
            return modelDto;
        }

        public static NutritionRequestModel CreateNutritionRequestModel(RecipeDTO model)
        {
            var nutritionDto = new NutritionRequestModel()
            {
                Kcal = model.Nutrition.Kcal,
                Fat = model.Nutrition.Fat,
                Saturates = model.Nutrition.Saturates,
                Carbs = model.Nutrition.Carbs,
                Sugars = model.Nutrition.Sugars,
                Fibre = model.Nutrition.Fibre,
                Protein = model.Nutrition.Protein,
                Salt = model.Nutrition.Salt
            };
            return nutritionDto;
        }

        public static IngredientRequestModel CreateIngredientRequestModel(IngredientDTO model)
        {
            var ingrid = new IngredientRequestModel()
            {
                Name = model.Name,
                Unit = model.Unit,
                UnitCount = model.UnitCount
            };
            return ingrid;
        }
    }
}
