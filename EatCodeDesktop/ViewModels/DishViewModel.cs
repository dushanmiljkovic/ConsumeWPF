using Caliburn.Micro;
using EatCodeDesktop.Helper;
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
    public class DishViewModel : Screen
    {
        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        public DishViewModel(IAPIHelper apiHelper, IWindowManager windowManager)
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
                NotifyOfPropertyChange(() => CanUpdateDish);
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
                NotifyOfPropertyChange(() => CanCreateDish);
            }
        }
        private string _season;
        public string Season
        {
            get { return _season; }
            set
            {
                _season = value;
                NotifyOfPropertyChange(() => Season);
            }
        }

        private string _origin;
        public string Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                NotifyOfPropertyChange(() => Origin);
            }
        }

        private ServedType _servedType;
        public ServedType ServedType
        {
            get { return _servedType; }
            set
            {
                _servedType = value;
                NotifyOfPropertyChange(() => ServedType);
            }
        }

        private string _servedOnEvents;
        public string ServedOnEvents
        {
            get { return _servedOnEvents; }
            set
            {
                _servedOnEvents = value;
                NotifyOfPropertyChange(() => ServedOnEvents);
            }
        }

        private string _externalLink;
        public string ExternalLink
        {
            get { return _externalLink; }
            set
            {
                _externalLink = value;
                NotifyOfPropertyChange(() => ExternalLink);
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
        public bool CanCreateDish
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

        public async void CreateDish()
        {
            var model = ComponentExport();
            try
            {
                var result = await apiHelper.CreateDishe(model);
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
        public bool CanUpdateDish
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
        public async void UpdateDish()
        {
            var model = ComponentExport();
            try
            {
                //var result = await apiHelper.UpdateDishe(model);
                ShowSimpleMessage("Udapting disesh is disabled!");
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
        public void InitComponent(DisheDTO model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Origin = model.Origin;
            this.Season = model.Season;
            this.ServedOnEvents = model.ServedOnEvents;
            this.ServedType = model.ServedType;
            this.ExternalLink = model.ExternalLink;
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
        public DisheDTO ComponentExport()
        {
            var disheDto = new DisheDTO()
            {
                Id = Id,
                ExternalLink = ExternalLink,
                Name = Name,
                Origin = Origin,
                Season = Season,
                ServedType = ServedType,
                ServedOnEvents = ServedOnEvents                
            };
            return disheDto;
        }
        #endregion
    }
}
