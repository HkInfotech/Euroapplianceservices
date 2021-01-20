using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuroMobileApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingControl : ContentView
    {
        public static readonly BindableProperty LoadingTextProperty =
     BindableProperty.Create(nameof(LoadingText), typeof(string), typeof(LoadingControl), default(string), BindingMode.TwoWay);
        public string LoadingText
        {
            get { return (string)GetValue(LoadingTextProperty); }
            set { SetValue(LoadingTextProperty, value); }
        }

        public LoadingControl()
        {
            InitializeComponent();
        }
    }
}