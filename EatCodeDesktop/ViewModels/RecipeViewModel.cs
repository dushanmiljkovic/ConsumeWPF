using Caliburn.Micro;
using EatCodeDesktop.EventModels;
using EatCodeDesktop.Helper;
using Models.Domein;
using Models.DTO;
using Models.Enums;
using Models.RequestModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EatCodeDesktop.ViewModels
{
    public class RecipeViewModel : Screen
    {
        #region Props
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanCreateRecipe);
            }
        }

        private int _prepTime;
        public int PrepTime
        {
            get { return _prepTime; }
            set { _prepTime = value; NotifyOfPropertyChange(() => PrepTime); }
        }

        private int _cookTime;
        public int CookTime
        {
            get { return _cookTime; }
            set { _cookTime = value; NotifyOfPropertyChange(() => CookTime); }
        }

        private int _readyIn;
        public int ReadyIn
        {
            get { return _readyIn; }
            set { _readyIn = value; NotifyOfPropertyChange(() => ReadyIn); }
        }

        private CookingSkills _skillRequired;
        public CookingSkills SkillRequired
        {
            get { return _skillRequired; }
            set { _skillRequired = value; NotifyOfPropertyChange(() => SkillRequired); }
        }

        private int _serves;
        public int Serves
        {
            get { return _serves; }
            set { _serves = value; NotifyOfPropertyChange(() => Serves); }
        }

        private BindingList<string> _preparationMethod;
        public BindingList<string> PreparationMethod
        {
            get { return _preparationMethod; }
            set { _preparationMethod = value; NotifyOfPropertyChange(() => PreparationMethod); }
        }
        private string _selectedStep;
        public string SelectedStep
        {
            get { return _selectedStep; }
            set
            {
                _selectedStep = value;
                NotifyOfPropertyChange(() => SelectedStep);
                NotifyOfPropertyChange(() => CanRemoveStept);
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; NotifyOfPropertyChange(() => Description); }
        }

        private int _file;
        public int File
        {
            get { return _file; }
            set { _file = value; NotifyOfPropertyChange(() => File); }
        }

        // Nutrition
        private int _kcal;
        public int Kcal
        {
            get { return _kcal; }
            set { _kcal = value; NotifyOfPropertyChange(() => Kcal); }
        }

        private int _fat;
        public int Fat
        {
            get { return _fat; }
            set { _fat = value; NotifyOfPropertyChange(() => Fat); }
        }

        private int _saturates;
        public int Saturates
        {
            get { return _saturates; }
            set { _saturates = value; NotifyOfPropertyChange(() => Saturates); }
        }

        private int _carbs;
        public int Carbs
        {
            get { return _carbs; }
            set { _carbs = value; NotifyOfPropertyChange(() => Carbs); }
        }
        private int _sugars;
        public int Sugars
        {
            get { return _sugars; }
            set { _sugars = value; NotifyOfPropertyChange(() => Sugars); }
        }
        private int _fibre;
        public int Fibre
        {
            get { return _fibre; }
            set { _fibre = value; NotifyOfPropertyChange(() => Fibre); }
        }
        private int _protein;
        public int Protein
        {
            get { return _protein; }
            set { _protein = value; NotifyOfPropertyChange(() => Protein); }
        }
        private int _salt;
        public int Salt
        {
            get { return _salt; }
            set { _salt = value; NotifyOfPropertyChange(() => Salt); }
        }

        // Ingredients
        private BindingList<IngredientDTO> _ingredients;
        public BindingList<IngredientDTO> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; NotifyOfPropertyChange(() => Ingredients); }
        }

        private IngredientDTO _selectedIngredient;
        public IngredientDTO SelectedIngredient
        {
            get { return _selectedIngredient; }
            set
            {
                _selectedIngredient = value;
                NotifyOfPropertyChange(() => SelectedIngredient);
                NotifyOfPropertyChange(() => CanRemoveIngredient);
            }
        }
        #endregion

        private Guid _recipeId;
        public Guid RecipeId
        {
            get { return _recipeId; }
            private set
            {
                _recipeId = value;
                NotifyOfPropertyChange(() => RecipeId);
                NotifyOfPropertyChange(() => CanUpdateRecipe);
                NotifyOfPropertyChange(() => IsEditMode);
                NotifyOfPropertyChange(() => IsCreateMode);
            }
        }

        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        private readonly IngredientViewModel ingredientViewModel;

        public RecipeViewModel(IAPIHelper apiHelper, IWindowManager windowManager, IngredientViewModel ingredientViewModel)
        {
            this.apiHelper = apiHelper;
            this.windowManager = windowManager;
            this.ingredientViewModel = ingredientViewModel;

            this.Ingredients = new BindingList<IngredientDTO>();
            this.PreparationMethod = new BindingList<string>();
        }
        public void InitComponent(RecipeDTO model)
        {
            if (model.Id != null)
            {
                RecipeId = model.Id;
            }

            Name = model.Name;
            PrepTime = model.PrepTime;
            CookTime = model.CookTime;
            ReadyIn = model.ReadyIn;
            SkillRequired = model.SkillRequired;
            Serves = model.Serves;
            PreparationMethod = new BindingList<string>(model.PreparationMethod);
            Description = model.Description;
            Kcal = model.Nutrition?.Kcal ?? 0;
            Fat = model.Nutrition?.Fat ?? 0;
            Saturates = model.Nutrition?.Saturates ?? 0;
            Carbs = model.Nutrition?.Carbs ?? 0;
            Sugars = model.Nutrition?.Sugars ?? 0;
            Fibre = model.Nutrition?.Fibre ?? 0;
            Protein = model.Nutrition?.Protein ?? 0;
            Salt = model.Nutrition?.Salt ?? 0;
            Ingredients = new BindingList<IngredientDTO>(model.Ingredients);
        }

        public RecipeDTO ComponentExport()
        {
            var nutritionDto = new NutritionDTO()
            {
                Kcal = Kcal,
                Fat = Fat,
                Saturates = Saturates,
                Carbs = Carbs,
                Sugars = Sugars,
                Fibre = Fibre,
                Protein = Protein,
                Salt = Salt
            };

            var modelDto = new RecipeDTO()
            {
                Id = RecipeId,
                Name = Name,
                PrepTime = PrepTime,
                CookTime = CookTime,
                ReadyIn = ReadyIn,
                SkillRequired = SkillRequired,
                Serves = Serves,
                PreparationMethod = PreparationMethod.ToList(),
                Description = Description,
                Nutrition = nutritionDto,
                Ingredients = Ingredients.ToList()
            };
            return modelDto;
        }

        #region Buttons

        public bool IsEditMode
        {
            get
            {
                var output = false;
                if (RecipeId != Guid.Empty)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool IsCreateMode
        {
            get
            {
                var output = false;
                if (RecipeId == Guid.Empty)
                {
                    output = true;
                }

                return output;
            }
        }


        public bool CanCreateRecipe
        {
            get
            {
                var output = false;

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    output = true;
                }

                return output;
            }
        }

        public async void CreateRecipe()
        {
            var model = ModelFactory.CreateRecipeRequestModel(ComponentExport());
            try
            {
                var result = await apiHelper.CreateRecipe(model);
                ShowSimpleMessage("Created!");
                TryClose();
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                ShowSimpleMessage("Error", "Error", test);
            }
        }

        public bool CanRemoveIngredient
        {
            get
            {
                bool output = false;

                if (SelectedIngredient != null)
                {
                    output = true;
                }

                return output;
            }
        }
        public void RemoveIngredient()
        {
            try
            {
                ShowSimpleMessage("Info", "Recipe", "Ingridiant " + SelectedIngredient.Name + " removed!");
                Ingredients.Remove(SelectedIngredient);
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                ShowSimpleMessage("Error", "Error", test);
            }
        }
        public void AddIngredient()
        {
            try
            {
                var dialogVM = IoC.Get<IngredientViewModel>();
                this.windowManager.ShowDialog(dialogVM, null, null);

                var ingridiant = dialogVM.ExportComponent();
                Ingredients.Add(ingridiant);

                ShowSimpleMessage("Info", "Recipe", "Ingridiant Added:" + ingridiant.Name);
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                ShowSimpleMessage("Error", "Error", test);
            }
        }
        public bool CanRemoveStept
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(SelectedStep))
                {
                    output = true;
                }

                return output;
            }
        }
        public void RemoveStept()
        {
            try
            {
                ShowSimpleMessage("Info", "Recipe", "Step " + SelectedStep + " removed!");
                PreparationMethod.Remove(SelectedStep);
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                ShowSimpleMessage("Error", "Error", test);
            }
        }
        public void AddStep()
        {
            try
            {
                var dialogVM = IoC.Get<SimpleTextImputViewModel>();
                dialogVM.InitComponent("Add new Step", "its easy");
                this.windowManager.ShowDialog(dialogVM, null, null);

                var step = dialogVM.ExportComponent();
                PreparationMethod.Add(step);

                ShowSimpleMessage("Info", "Recipe", "Step " + step + " added!");
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                ShowSimpleMessage("Error", "Error", test);
            }
        }

        public bool CanUpdateRecipe
        {
            get
            {
                bool output = false;

                if (RecipeId != Guid.Empty)
                {
                    output = true;
                }

                return output;
            }
        }
        public async void UpdateRecipe()
        {
            var model = ModelFactory.UpdateRecipeRequestModel(ComponentExport());
            try
            {
                var result = await apiHelper.UpdateRecipe(model);
                ShowSimpleMessage("Yey!");
                TryClose();
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                ShowSimpleMessage("Error", "Error", test);
            }
        }
        #endregion

        public void ShowSimpleMessage(string windowTitle = "", string header = "", string msg = "")
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = windowTitle;

            //// Get New Instance :)
            var info = IoC.Get<StatusInfoViewModel>();
            info.UpdateMessage(header, msg);
            this.windowManager.ShowDialog(info, null, settings);
        }



        //public CreateRecipeRequestModel CreateRecipeRequestModel()
        //{
        //    var ingrids = new List<IngredientRequestModel>();

        //    foreach(var ingr in Ingredients)
        //    {
        //        ingrids.Add(new IngredientRequestModel()
        //        {
        //            Name = ingr.Name,
        //            Unit = ingr.Unit,
        //            UnitCount = ingr.UnitCount
        //        });
        //    }

        //    var nutritionDto = new NutritionRequestModel()
        //    {
        //        Kcal = Kcal,
        //        Fat = Fat,
        //        Saturates = Saturates,
        //        Carbs = Carbs,
        //        Sugars = Sugars,
        //        Fibre = Fibre,
        //        Protein = Protein,
        //        Salt = Salt
        //    };

        //    var modelDto = new CreateRecipeRequestModel()
        //    {
        //        Name = Name,
        //        PrepTime = PrepTime,
        //        CookTime = CookTime,
        //        ReadyIn = ReadyIn,
        //        SkillRequired = SkillRequired,
        //        Serves = Serves,
        //        PreparationMethod = PreparationMethod.ToList(),
        //        Description = Description,
        //        Nutrition = nutritionDto,
        //        Ingredients = ingrids
        //    };
        //    return modelDto;
        //}

        //public UpdateRecipeRequestModel UpdateRecipeRequestModel(Guid id)
        //{
        //    var ingrids = new List<IngredientRequestModel>();

        //    foreach (var ingr in Ingredients)
        //    {
        //        ingrids.Add(new IngredientRequestModel()
        //        {
        //            Name = ingr.Name,
        //            Unit = ingr.Unit,
        //            UnitCount = ingr.UnitCount
        //        });
        //    }

        //    var nutritionDto = new NutritionRequestModel()
        //    {
        //        Kcal = Kcal,
        //        Fat = Fat,
        //        Saturates = Saturates,
        //        Carbs = Carbs,
        //        Sugars = Sugars,
        //        Fibre = Fibre,
        //        Protein = Protein,
        //        Salt = Salt
        //    };

        //    var modelDto = new UpdateRecipeRequestModel()
        //    {
        //        Id = id,
        //        Name = Name,
        //        PrepTime = PrepTime,
        //        CookTime = CookTime,
        //        ReadyIn = ReadyIn,
        //        SkillRequired = SkillRequired,
        //        Serves = Serves,
        //        PreparationMethod = PreparationMethod.ToList(),
        //        Description = Description,
        //        Nutrition = nutritionDto,
        //        Ingredients = ingrids
        //    };
        //    return modelDto;
        //}

    }
}
