using Caliburn.Micro;
using EatCodeDesktop.Helper;
using Models.DTO;
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
    public class RecipesViewModel : Screen
    {
        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        public RecipesViewModel(IAPIHelper apiHelper, IWindowManager windowManager)
        {
            this.apiHelper = apiHelper;
            this.windowManager = windowManager;
        }


        #region Props
        private BindingList<RecipeDTO> _recipes;
        public BindingList<RecipeDTO> Recipes
        {
            get { return _recipes; }
            set
            {

                _recipes = value;
                NotifyOfPropertyChange(() => Recipes);
            }
        }
        private RecipeDTO _selectedRecipe;
        public RecipeDTO SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                NotifyOfPropertyChange(() => SelectedRecipe);
                NotifyOfPropertyChange(() => CanDeliteRecipe);
                NotifyOfPropertyChange(() => CanEditRecipe);
            }
        }
        #endregion


        #region Events
        protected override async void OnViewLoaded(object view)
        {
            try
            {
                base.OnViewLoaded(view);
                await LoadRecipes();
            }
            catch (Exception)
            {

                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "Error";

                //// Get New Instance :)
                var info = IoC.Get<StatusInfoViewModel>();
                info.UpdateMessage("Error", "Error Geting Recipes");
                this.windowManager.ShowDialog(info, null, settings);
            }
        }
        #endregion

        #region Private Hendls
        private async Task LoadRecipes()
        {
            var recipes = await apiHelper.GetAllRecipes();
            Recipes = new BindingList<RecipeDTO>(recipes);
        }
        #endregion

        #region Buttons
        public bool CanEditRecipe
        {
            get
            {
                bool output = false;

                if (SelectedRecipe != null)
                {
                    output = true;
                }

                return output;
            }
        }
        public async void EditRecipe()
        {
            var dialogVM = IoC.Get<RecipeViewModel>();
            dialogVM.InitComponent(SelectedRecipe);
            this.windowManager.ShowDialog(dialogVM, null, null);
            await LoadRecipes();
        }

        public bool CanDeliteRecipe
        {
            get
            {
                bool output = false;

                if (SelectedRecipe != null)
                {
                    output = true;
                }

                return output;
            }
        }
        public async void DeliteRecipe()
        {
            var status = await apiHelper.DeleteRecipe(SelectedRecipe.Id);
            if (status)
            {
                ShowSimpleMessage("","","Deleted");
                await LoadRecipes(); 
            }
            else
            {
                ShowSimpleMessage("Error", "Error", "Error");
            }
          
        }
        public void NewRecipe()
        {

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

    }
}
