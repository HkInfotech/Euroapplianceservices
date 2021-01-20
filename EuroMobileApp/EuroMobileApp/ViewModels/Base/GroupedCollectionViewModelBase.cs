using EuroMobileApp.Helpers;
using EuroMobileApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EuroMobileApp.ViewModels.Base
{
    public class GroupedCollectionViewModelBase<TCellViewModel> : ViewModelBase where TCellViewModel : CellViewModelBase
    {
		public bool IsRefreshing { get; set; }
		public ObservableCollection<CollectionViewGroup<TCellViewModel>> ListItems { get; set; }

		

		protected GroupedCollectionViewModelBase(IAggregatedServices restService) : base(restService)
		{
		}

		
	}
}
