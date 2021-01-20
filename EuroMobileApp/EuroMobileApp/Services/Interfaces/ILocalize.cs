﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EuroMobileApp.Services.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale();
    }
}
