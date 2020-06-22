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
    public class DishLinksViewModel : Screen
    {
        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        public DishLinksViewModel(IAPIHelper apiHelper, IWindowManager windowManager)
        {
            this.apiHelper = apiHelper;
            this.windowManager = windowManager;
        }

        public void InitComponent(DisheDTO disheDTO)
        {
            this.SelectedDishe = disheDTO;
            this.Name = SelectedDishe.Name;
        }

        #region Props
        private DisheDTO SelectedDishe;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name); 
            }
        }

        private BindingList<DrinkDTO> _goodDrinks;
        public BindingList<DrinkDTO> GoodDrinks
        {
            get { return _goodDrinks; }
            set
            {
                _goodDrinks = value;
                NotifyOfPropertyChange(() => GoodDrinks);
                NotifyOfPropertyChange(() => CanDeleteGoodDrink);
            }
        }

        private DrinkDTO _selectedGoodDrink;
        public DrinkDTO SelectedGoodDrink
        {
            get { return _selectedGoodDrink; }
            set
            {
                _selectedGoodDrink = value;
                NotifyOfPropertyChange(() => SelectedGoodDrink);  
                NotifyOfPropertyChange(() => CanDeleteGoodDrink);  
            }
        }

        private BindingList<DrinkDTO> _badDrinks;
        public BindingList<DrinkDTO> BadDrinks
        {
            get { return _badDrinks; }
            set
            {
                _badDrinks = value;
                NotifyOfPropertyChange(() => BadDrinks);
            }
        }

        private DrinkDTO _selectedBadDrink;
        public DrinkDTO SelectedBadDrink
        {
            get { return _selectedBadDrink; }
            set
            {
                _selectedBadDrink = value;
                NotifyOfPropertyChange(() => SelectedBadDrink);
                NotifyOfPropertyChange(() => CanDeleteBadDrink);
            }
        }

        #endregion

        #region Buttons
        public bool CanDeleteGoodDrink
        {
            get
            {
                bool output = false;

                if (SelectedGoodDrink != null && !string.IsNullOrWhiteSpace(SelectedDishe.Id))
                {
                    output = true;
                }

                return output;
            }
        }
        public async void DeleteGoodDrink()
        {
            var name = SelectedGoodDrink.Name;
            var status = await apiHelper.DerelateDisheDrink(SelectedDishe.Id, SelectedGoodDrink.Id, Models.Domein.DisheDrink.GoesWith);
            if (status)
            {
                await LoadGoodDrinks();
                ShowSimpleMessage("Info", "Info", "Deleted " + name);
                
            }
            else
            {
                ShowSimpleMessage("Error", "Error", "Error");
            }

        }

        public bool CanDeleteBadDrink
        {
            get
            {
                bool output = false;

                if (SelectedBadDrink != null && !string.IsNullOrWhiteSpace(SelectedDishe.Id))
                {
                    output = true;
                }

                return output;
            }
        }
        public async void DeleteBadDrink()
        {
            var name = SelectedBadDrink.Name;
            var status = await apiHelper.DerelateDisheDrink(SelectedDishe.Id, SelectedBadDrink.Id, Models.Domein.DisheDrink.Never);
            if (status)
            {
                await LoadBadDrinks();
                ShowSimpleMessage("Info", "Info", "Deleted " + name);
               
            }
            else
            {
                ShowSimpleMessage("Error", "Error", "Error");
            }

        }
        #endregion
        protected override async void OnViewLoaded(object view)
        {
            try
            {
                base.OnViewLoaded(view);
                await LoadGoodDrinks();
                await LoadBadDrinks();
            }
            catch (Exception ex)
            { 
                ShowSimpleMessage("Error", "Error", ex.Message);
            }
        }
        #region Events
        #endregion
        #region Private Hendls
        private async Task LoadGoodDrinks()
        {
            var drinks = await apiHelper.GetDishSuggestionDrinks(SelectedDishe.Id, Models.Domein.DisheDrink.GoesWith);
            GoodDrinks = new BindingList<DrinkDTO>(drinks.Item2);
        }
        private async Task LoadBadDrinks()
        {
            var drinks = await apiHelper.GetDishSuggestionDrinks(SelectedDishe.Id, Models.Domein.DisheDrink.Never);
            BadDrinks = new BindingList<DrinkDTO>(drinks.Item2);
        }
        #endregion
 
        #region PrivateMethod
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
        #endregion 
    }
}
