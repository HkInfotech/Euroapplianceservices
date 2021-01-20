using EuroMobileApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace EuroMobileApp.ViewModels
{
    public class ViewImagePageViewModel : ViewModelBase
    {
        public DocumentModel ImageDocumentModel { get; set; }
        public ImageSource FileSource { get; set; }
        public ViewImagePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue("DocumentModel", out DocumentModel documentModel))
            {
                if (string.IsNullOrEmpty(documentModel.LocalPath))
                {
                    FileSource = documentModel.FileURL;
                }
                else
                {
                    FileSource = ImageSource.FromFile(documentModel.LocalPath);
                }

            }
        }
    }
}
