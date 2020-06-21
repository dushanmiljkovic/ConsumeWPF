using Caliburn.Micro;
using EatCodeDesktop.Helper;
using Models.Domein;
using Models.DTO;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EatCodeDesktop.ViewModels
{ 
    public class DrinkViewModel : Screen
    {
        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        public DrinkViewModel(IAPIHelper apiHelper, IWindowManager windowManager)
        {
            this.apiHelper = apiHelper;
            this.windowManager = windowManager;
        }

        #region Props
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
                NotifyOfPropertyChange(() => CanUpdateDrink);
                NotifyOfPropertyChange(() => IsEditMode);
                NotifyOfPropertyChange(() => IsCreateMode);
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanCreateDrink);
            }
        }

       
    
        private DrinkType _type;
        public DrinkType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyOfPropertyChange(() => Type); 
            }
        }
         
        private int _alcoholLevel;
        public int AlcoholLevel
        {
            get { return _alcoholLevel; }
            set
            {
                _alcoholLevel = value;
                NotifyOfPropertyChange(() => AlcoholLevel); 
            }
        }
        #endregion

        #region Buttons
        public bool IsCreateMode
        {
            get
            {
                var output = false;
                if (string.IsNullOrWhiteSpace(Id))
                {
                    output = true;
                }

                return output;
            }
        }
        public bool CanCreateDrink
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

        public async void CreateDrink()
        {
            var model = ComponentExport();
            try
            {
                var result = await apiHelper.CreateDrink(model);
                ShowSimpleMessage("Created!");
                TryClose();
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                ShowSimpleMessage("Error", "Error", test);
            }
        }

        public bool IsEditMode
        {
            get
            {
                var output = false;
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    output = true;
                }

                return output;
            }
        }
        public bool CanUpdateDrink
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(Id))
                {
                    output = true;
                }

                return output;
            }
        }
        public async void UpdateDrink()
        {
            var model = ComponentExport();
            try
            {
                var result = await apiHelper.UpdateDrink(model);
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
        #region Events

        public void InitComponent(DrinkDTO model)
        {
            this.Id = model.Id;
            this.Name = model.Name; 
            this.Type = model.Type;
            this.AlcoholLevel = model.AlcoholLevel; 
        }

        #endregion
        #region EventsHandles
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
        #region Publicethod
        public DrinkDTO ComponentExport()
        {
            var drinkDto = new DrinkDTO()
            {
                Id = Id, 
                Name = Name, 
                AlcoholLevel = AlcoholLevel,
                Type = Type
            };
            return drinkDto;
        }
        #endregion
    }
}
