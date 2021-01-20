using EuroMobileApp.Helpers;
using EuroMobileApp.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobileApp.ViewModels.Base
{
    public abstract class CollectionViewModelBase<TCellViewModel> : ViewModelBase where TCellViewModel : CellViewModelBase
    {
        public bool IsRefreshing { get; set; }
        public ObservableCollection<TCellViewModel> ListItems { get; set; }
        public TCellViewModel SelectedItem { get; set; }

        private DelegateCommand<TCellViewModel> _itemSelectedCommand;
        public DelegateCommand<TCellViewModel> ItemSelectedCommand =>
            _itemSelectedCommand ?? (_itemSelectedCommand = new DelegateCommand<TCellViewModel>(async (a) => await ExecuteItemSelectedCommand(a)));

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(async () => await RefreshAsync()));

        public CollectionViewModelBase(IAggregatedServices aggregatedServices) :base(aggregatedServices)
        {

        }

      


        private Task ExecuteItemSelectedCommand(TCellViewModel item)
        {
            SelectedItem = null;
            if (item == null)
            {
                return AppHelpers.CompletedTask;
            }
            return OnItemSelectedAsync(item);

        }

        protected virtual Task OnItemSelectedAsync(TCellViewModel item) => AppHelpers.CompletedTask;

        protected virtual Task RefreshAsync() => AppHelpers.CompletedTask;
    }
}
