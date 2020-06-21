using Caliburn.Micro;
using EatCodeDesktop.EventModels;
using EatCodeDesktop.Helper;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatCodeDesktop.ViewModels
{
    public class IngredientViewModel : Screen
    {
        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        private readonly IEventAggregator eventAggregator;

        public IngredientViewModel(IAPIHelper apiHelper, IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            this.apiHelper = apiHelper;
            this.windowManager = windowManager;
            this.eventAggregator = eventAggregator;
        }

        #region Props
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanCreateIngredient);
            }
        }
        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                NotifyOfPropertyChange(() => Unit);
                NotifyOfPropertyChange(() => CanCreateIngredient);
            }
        }

        private int _unitCount;
        public int UnitCount
        {
            get { return _unitCount; }
            set
            {
                _unitCount = value;
                NotifyOfPropertyChange(() => UnitCount);
                NotifyOfPropertyChange(() => CanCreateIngredient);
            }
        }

        #endregion
        #region Buttons
        public bool CanCreateIngredient
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    output = true;
                }

                return output;
            }
        }
        public void CreateIngredient()
        {
            TryClose();
        }

        #endregion
        #region Events

        public void AddIngredient()
        {
            try
            {
                //var model = await apiHelper.Auth(userName, password);
                eventAggregator.PublishOnCurrentThread(new IngredientCreatedOnEvent()
                {
                    Name = this.Name
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
        #region EventsHandles
        #endregion
        #region PrivateMethod
        #endregion
        #region Publicethod
        public void InitComponent(IngredientDTO model)
        {
            Name = model.Name;
            UnitCount = model.UnitCount;
            Unit = model.Unit;

        }

        public IngredientDTO ExportComponent()
        {
            var ingrid = new IngredientDTO()
            {
                Name = Name,
                UnitCount = UnitCount,
                Unit = Unit
            };
            return ingrid;
        }
        #endregion
    }
}
