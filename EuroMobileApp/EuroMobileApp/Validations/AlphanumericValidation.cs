using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EuroMobileApp.Validations
{
    public class AlphanumericValidation<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return true;
            }
            const string emailRegex = @"^[a-zA-Z0-9]*$";
            var str = value as string;
            Regex regex = new Regex(emailRegex);
            Match match = regex.Match(str.Trim());
            return match.Success;
        }
    }
}
