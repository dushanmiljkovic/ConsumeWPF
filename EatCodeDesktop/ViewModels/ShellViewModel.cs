using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using EatCodeDesktop.EventModels;

namespace EatCodeDesktop.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private SimpleContainer simpleContainer;
        private LoginViewModel _loginWM;
       
        private IEventAggregator eventAggregator;

        public ShellViewModel(SimpleContainer simpleContainer, LoginViewModel loginWM, IEventAggregator eventAggregator)
        {
            this.simpleContainer = simpleContainer;
            this._loginWM = loginWM;
            
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            ActivateItem(_loginWM);
            //ActivateItem(simpleContainer.GetInstance<LoginViewModel>());
        }

        public void ExitApplication()
        {
            TryClose();
        }
        public void ShowRecipeList()
        { 
            var recipesView = simpleContainer.GetInstance<RecipesViewModel>();
            ActivateItem(recipesView);
            _loginWM = simpleContainer.GetInstance<LoginViewModel>();
        }

        public void Handle(LogOnEvent message)
        {
            CreateRecipes();
        }

        public void ShowCreateRecipe()
        {
            CreateRecipes();
        }
        private void CreateRecipes()
        {
            var recipeView = simpleContainer.GetInstance<RecipeViewModel>();
            ActivateItem(recipeView);
            _loginWM = simpleContainer.GetInstance<LoginViewModel>();
        }

        public void ShowDishsDrink()
        {
           
        }
        public void ShowCreateDish()
        {
         
        }
        public void ShowCreateDrink()
        {
           
        }
    }
}
