using Caliburn.Micro;
using EatCodeDesktop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatCodeDesktop.ViewModels
{
    public class DefaultVM : Screen
    {
        private readonly IAPIHelper apiHelper;
        private readonly IWindowManager windowManager;
        public DefaultVM(IAPIHelper apiHelper, IWindowManager windowManager)
        {
            this.apiHelper = apiHelper;
            this.windowManager = windowManager;
        }

        #region Props
        #endregion
        #region Buttons
        #endregion
        #region Events
        #endregion
        #region EventsHandles
        #endregion
        #region PrivateMethod
        #endregion
        #region Publicethod
        #endregion
    }
}
