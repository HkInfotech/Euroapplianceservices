using EuroMobileApp.Models;
using EuroMobileApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.ViewModels.CellViewModel
{
    public class OrderCellViewModel : CellViewModelBase
    {

        public OrderModel Order { get; set; }
        public OrderCellViewModel(OrderModel order)
        {
            this.Order = order;
        }

    }
}
