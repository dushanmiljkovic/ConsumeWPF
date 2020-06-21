using Caliburn.Micro;
using EatCodeDesktop.Helper;
using Models.DTO;
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
    public class RecipeXXViewModel : Screen
    {
        private IAPIHelper apiHelper;
        private readonly StatusInfoViewModel statusInfoViewModel;
        private readonly IWindowManager windowManager;
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
            set {
                _selectedRecipe = value;
                NotifyOfPropertyChange(() => SelectedRecipe);
            }
        }
         
        private BindingList<string> _products;
        public BindingList<string> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _cart;
        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity;
        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public string SubTotal
        {
            get
            {
                return "0";
            }
        }
        public string Total
        {
            get
            {
                return "0";
            }
        }
        public string Taxes
        {
            get
            {
                return "0";
            }
        }

        public RecipeXXViewModel(IAPIHelper apiHelper, StatusInfoViewModel statusInfoViewModel, IWindowManager windowManager)
        {
            this.apiHelper = apiHelper;
            this.statusInfoViewModel = statusInfoViewModel;
            this.windowManager = windowManager;
             

        }

        public void AddToCart()
        {
        }
        public void RemoveFromCart()
        {
        }
        public void CheckOut()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "Info Message";

            //// Get New Instance :)
            //var info = IoC.Get<StatusInfoViewModel>();
            statusInfoViewModel.UpdateMessage("Checkout", "Checkout message!");
            this.windowManager.ShowDialog(statusInfoViewModel, null, settings);


            //TryClose();
        }

        public void CreateRecipe()
        {
            var model = new CreateRecipeRequestModel()
            {
                Name = "test"
            };
            var result = apiHelper.CreateRecipe(model);
        }

        public void CanEditRecipe()
        {

        }


        private async Task LoadRecipes()
        {
            var recipes = await apiHelper.GetAllRecipes();
            Recipes = new BindingList<RecipeDTO>(recipes);
        }

        // Events:
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadRecipes();
        }

    }
}