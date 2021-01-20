using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class OrdersListModel: EuroMobileApp.ViewModels.BaseVM
    {
        public bool status { get; set; }
        public string message { get; set; }
        private List<clsOrder> _data { get; set; }
        public List<clsOrder> data 
        {
            get { return _data; } 
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        public OrdersListModel()
        {
            status = false;
            message = "";
            data = new List<clsOrder>();
        }
    }
}
