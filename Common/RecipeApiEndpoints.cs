 namespace Common
{
    public class RecipeApiEndpoints
    {
        //Recipes
        public static string RecipeCreate => "/api/Recipe/create";
        public static string RecipeUpdate => "api/Recipe/update";
        public static string RecipeGet => "/api/Recipe/{guide}";
        public static string RecipeAll => "/api/Recipe/all";
        public static string RecipeDelete(string guide) => "/api/Recipe/delete/" + guide;

        //Files
        public static string FileGetByName(string filename) => "/api/File/get-by-file-name/{fileName}".Replace("{fileName}",filename);
        public static string FileGetByID(string id)=> "/api/File/get-by-id/{id}".Replace("{id}", id);
        public static string FileUploadImg => "/api/File/upload-image";
        public static string FileUpload => "/api/File/upload";
        public static string FileDelete => "/api/File";

        //Dish
        public static string DishCreate => "api/Related/create-dishe";
        public static string DishGet(string id) => "api/Related/dishe/" + id;
        public static string DrinkCreate => "api/Related/create-drink";
        public static string DrinkGet(string id) => "api/Related/drink/" + id;
        public static string DrinkUpdate(string id) => "api/Related/update-drink/" + id;
        public static string DrinkDelete(string id) => "api/Related/delete-drink/" + id;
        public static string RelateDrinkDish => "api/Related/relate-drink-dish";
        public static string DishDrinkGoodCount(string id) => "api/Related/dish-has-good-drink/" + id;
        public static string DishDrinkGood(string id) => "api/Related/dish-good-drink/" + id;
        public static string DishDrinkNever(string id) => "api/Related/dish-never-drink/" + id;
        public static string DerelateDisheDrink => "api/Related/derelate-drink-dish/";

    }
}
