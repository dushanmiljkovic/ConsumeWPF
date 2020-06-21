using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatCodeDesktop.ViewModels
{

    public class SimpleTextImputViewModel : Screen
    {
        public string DisplayText { get; private set; } 
        public string Hint { get; private set; }

        public SimpleTextImputViewModel()
        {
            
        }

        public void InitComponent(string displayText, string hint)
        {
            DisplayText = displayText;
            Hint = hint;

            NotifyOfPropertyChange(() => DisplayText);
            NotifyOfPropertyChange(() => Hint);
        }

        private string _stringValue;
        public string StringValue
        {
            get { return _stringValue; }
            set
            {
                _stringValue = value;
                NotifyOfPropertyChange(() => StringValue); 
            }
        }

        public string ExportComponent()
        {
            return StringValue;
        }

        public void Close()
        {
            TryClose();
        }
    }

}
