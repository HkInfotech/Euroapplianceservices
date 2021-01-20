using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Validations
{
    public class ValueIsNotZeroRules<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var val = Convert.ToDecimal(value);
            return val != 0;
        }
    }
}
