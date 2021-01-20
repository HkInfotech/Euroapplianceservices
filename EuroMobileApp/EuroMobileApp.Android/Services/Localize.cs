using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EuroMobileApp.Droid.Services;
using EuroMobileApp.Services.Interfaces;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace EuroMobileApp.Droid.Services
{
    public class Localize: ILocalize
    {
		#region Public Methods

		public CultureInfo GetCurrentCultureInfo()
		{
			var androidLocale = Locale.Default;

			var language = androidLocale.ToString().Replace("_", "-");

			var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
			if (!cultures.Any(c => c.Name == language))
			{
				language = "en";
			}

			return new CultureInfo(language);
		}

		public void SetLocale()
		{
			var culture = GetCurrentCultureInfo();

			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}

		#endregion

	}
}