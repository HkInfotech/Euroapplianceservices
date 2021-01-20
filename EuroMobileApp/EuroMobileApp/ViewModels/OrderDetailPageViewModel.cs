using EuroMobileApp.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroMobileApp.ViewModels
{
    public class OrderDetailPageViewModel : ViewModelBase
    {
        #region Properties
        public int PositionSelected { get; set; }
        #endregion

        #region Commands
        private DelegateCommand<int> _selectItemCommand;
        public DelegateCommand<int> SelectItemCommand =>
            _selectItemCommand ?? (_selectItemCommand = new DelegateCommand<int>(ExecuteSelectItemCommand));

        void ExecuteSelectItemCommand(int parameter)
        {
            PositionSelected = parameter;
        }

        
        #endregion
        #region Constructor
        public OrderDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IAppSettings settings, IEventAggregator eventAggregator, IAccountService accountService) : base(navigationService, pageDialogService, settings, eventAggregator)
        {
            PositionSelected = 0;
        }

        #endregion

        #region Methods

        #endregion


    }
}
