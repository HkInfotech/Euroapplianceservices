﻿
using EuroMobileApp.Controls;
using EuroMobileApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatetimePickerRendere))]

namespace EuroMobileApp.Droid.Renderer
{
    public class BorderlessDatetimePickerRendere: DatePickerRenderer
    {
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                //var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                //layoutParams.SetMargins(0, 0, 0, 0);
                //LayoutParameters = layoutParams;
                //Control.LayoutParameters = layoutParams;
                //Control.SetPadding(0, 0, 0, 0);
                //SetPadding(0, 0, 0, 0);
            }
        }
    }
}