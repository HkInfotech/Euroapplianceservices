using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Validations
{
    public class ActionSheetValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            var SelectedItemValue= value as string;
            return !string.IsNullOrEmpty(SelectedItemValue);
        }
    }
}
