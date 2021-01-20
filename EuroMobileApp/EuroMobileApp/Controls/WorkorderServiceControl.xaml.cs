using EuroMobileApp.Models;
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
    public partial class WorkorderServiceControl : StackLayout
    {
        public WorkorderServiceControl()
        {
            InitializeComponent();
        }

       

        public static readonly BindableProperty ServiceChargeTitleProperty =
        BindableProperty.Create(nameof(ServiceChargeTitle), typeof(string), typeof(WorkorderServiceControl), default(string), BindingMode.TwoWay);

        public static readonly BindableProperty ServiceChargeDescriptionProperty =
      BindableProperty.Create(nameof(ServiceChargeDescription), typeof(string), typeof(WorkorderServiceControl), default(string), BindingMode.TwoWay);

        public static readonly BindableProperty ServiceChargeAmountProperty =
     BindableProperty.Create(nameof(ServiceChargeAmount), typeof(decimal), typeof(WorkorderServiceControl), default(decimal), BindingMode.TwoWay);
        
        public string ServiceChargeTitle
        {
            get
            {
                return (string)GetValue(ServiceChargeTitleProperty);
            }
            set
            {
                SetValue(ServiceChargeTitleProperty, value);
            }
        }

        public string ServiceChargeDescription
        {
            get
            {
                return (string)GetValue(ServiceChargeDescriptionProperty);
            }
            set
            {
                SetValue(ServiceChargeDescriptionProperty, value);
            }
        }
        public decimal ServiceChargeAmount
        {
            get
            {
                return (decimal)GetValue(ServiceChargeAmountProperty);
            }
            set
            {
                SetValue(ServiceChargeAmountProperty, value);
            }
        }
    }
}