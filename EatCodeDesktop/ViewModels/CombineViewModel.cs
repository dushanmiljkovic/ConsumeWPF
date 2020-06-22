using Caliburn.Micro;
using EatCodeDesktop.Helper;
using Models.Domein;
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
    public class CombineViewModel : Screen
    {
        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        public CombineViewModel(IAPIHelper apiHelper, IWindowManager windowManager)
        {
            this.apiHelper = apiHelper;
            this.windowManager = windowManager;
            Relation = Enum.GetValues(typeof(DisheDrink)).Cast<DisheDrink>().ToList();
        }

        #region Props

        private BindingList<DisheDTO> _dishes;
        public BindingList<DisheDTO> Dishes
        {
            get { return _dishes; }
            set
            {
                _dishes = value;
                NotifyOfPropertyChange(() => Dishes);
            }
        }

        private DisheDTO _selectedDishe;
        public DisheDTO SelectedDishe
        {
            get { return _selectedDishe; }
            set
            {
                _selectedDishe = value;
                NotifyOfPropertyChange(() => SelectedDishe);
                NotifyOfPropertyChange(() => CanEditDishe);
                NotifyOfPropertyChange(() => CanShowLinksForDish);
            }
        }

        private BindingList<DrinkDTO> _drinks;
        public BindingList<DrinkDTO> Drinks
        {
            get { return _drinks; }
            set
            {
                _drinks = value;
                NotifyOfPropertyChange(() => Drinks);
            }
        }

        private DrinkDTO _selectedDrink;
        public DrinkDTO SelectedDrink
        {
            get { return _selectedDrink; }
            set
            {
                _selectedDrink = value;
                NotifyOfPropertyChange(() => SelectedDrink);
                NotifyOfPropertyChange(() => CanEditDrink);
                NotifyOfPropertyChange(() => CanDeliteDrink);
                NotifyOfPropertyChange(() => CanRelateDishDrink);
            }
        }

        //private DisheDrink _relation;
        //public DisheDrink Relation
        //{
        //    get { return _relation; }
        //    set
        //    {
        //        _relation = value;
        //        NotifyOfPropertyChange(() => Relation);
        //        NotifyOfPropertyChange(() => CanRelateDishDrink);
        //        NotifyOfPropertyChange(() => CanShowLinksForDish);
        //    }
        //}

        private DisheDrink _selectedRelation;

        public DisheDrink SelectedRelation
        {
            get => _selectedRelation;
            set => Set(ref _selectedRelation, value);
        }
        public IReadOnlyList<DisheDrink> Relation { get; }

        #endregion
        #region Buttons
        public bool CanEditDishe
        {
            get
            {
                bool output = false;

                if (SelectedDishe != null)
                {
                    output = true;
                }

                return output;
            }
        }
        public async void EditDishe()
        {
            var dialogVM = IoC.Get<DishViewModel>();
            dialogVM.InitComponent(SelectedDishe);
            this.windowManager.ShowDialog(dialogVM, null, null);
            await LoadDishes();
        }
         
        public bool CanEditDrink
        {
            get
            {
                bool output = false;

                if (SelectedDrink != null)
                {
                    output = true;
                }

                return output;
            }
        }
        public async void EditDrink()
        {
            var dialogVM = IoC.Get<DrinkViewModel>();
            dialogVM.InitComponent(SelectedDrink);
            this.windowManager.ShowDialog(dialogVM, null, null);
            await LoadDrinks();
        }
        public bool CanDeliteDrink
        {
            get
            {
                bool output = false;

                if (SelectedDrink != null)
                {
                    output = true;
                }

                return output;
            }
        }
        public async void DeliteDrink()
        {
            var drinkName = SelectedDrink.Name;
            var status = await apiHelper.DeleteDrink(SelectedDrink.Id);
            if (status)
            {
                await LoadDrinks();
                ShowSimpleMessage("Info", "Info", "Deleted " + drinkName); 
            }
            else
            {
                ShowSimpleMessage("Error", "Error", "Error");
            }

        }

        public bool CanRelateDishDrink
        {
            get
            {
                bool output = false;

                if (SelectedDishe  != null && SelectedDrink != null)
                {
                    if(!string.IsNullOrWhiteSpace(SelectedDishe.Id) && !string.IsNullOrWhiteSpace(SelectedDrink.Id))
                    {
                        output = true;
                    } 
                }

                return output;
            }
        }
        public async void RelateDishDrink()
        {

            var status = await apiHelper.RelateDisheDrink(SelectedDishe.Id,SelectedDrink.Id, SelectedRelation);
            if (status)
            {
                ShowSimpleMessage("Info", "Info", "Related " + SelectedRelation.ToString() +" !"); 
            }
            else
            {
                ShowSimpleMessage("Error", "Error", "Error");
            }

        }

        public bool CanShowLinksForDish
        {
            get
            {
                bool output = false;

                if (SelectedDishe != null)
                {
                    if (!string.IsNullOrWhiteSpace(SelectedDishe.Id))
                    {
                        output = true;
                    }
                }

                return output;
            }
        }
        public void ShowLinksForDish()
        {
            var dialogVM = IoC.Get<DishLinksViewModel>();
            dialogVM.InitComponent(SelectedDishe);
            this.windowManager.ShowDialog(dialogVM, null, null);           
        }


        #endregion
        #region Events
        protected override async void OnViewLoaded(object view)
        {
            try
            {
                base.OnViewLoaded(view);
                await LoadDishes();
                await LoadDrinks();
            }
            catch (Exception ex)
            {
                ShowSimpleMessage("Error", "Error", ex.Message); 
            }
        }
        #endregion
        #region EventsHandles
        #endregion
        #region PrivateMethod
        private async Task LoadDishes()
        {
            var dishes = await apiHelper.GetAllDishes();
            Dishes = new BindingList<DisheDTO>(dishes);
        }

        private async Task LoadDrinks()
        {
            var drinks = await apiHelper.GetAllDrinks();
            Drinks = new BindingList<DrinkDTO>(drinks);
        }

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
        #region Publicethod
        #endregion
    }
}
