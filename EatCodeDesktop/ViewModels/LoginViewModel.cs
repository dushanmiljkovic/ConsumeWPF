using Caliburn.Micro;
using EatCodeDesktop.EventModels;
using EatCodeDesktop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatCodeDesktop.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper apiHelper;
        private IEventAggregator _eventAggregator;
        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator eventAggregator)
        {
            this.apiHelper = apiHelper;
            _eventAggregator = eventAggregator;
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);

            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get
            {
                var output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }


        }

        public async Task LogIn(string userName, string password)
        {
            try
            {
                //var model = await apiHelper.Auth(userName, password);
                _eventAggregator.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }
    }
}
