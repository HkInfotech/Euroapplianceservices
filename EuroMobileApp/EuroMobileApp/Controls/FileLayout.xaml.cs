using EuroMobileApp.Models;
using EuroMobileApp.Models.Common.Enum;
using EuroMobileApp.PrismEvents;
using Prism.DryIoc;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuroMobileApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileLayout : StackLayout
    {
        private readonly IEventAggregator _eventAggregator;

        public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(FileLayout), default(string), BindingMode.TwoWay);

        public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(FileLayout), default(string), BindingMode.TwoWay);

        public static readonly BindableProperty FilesProperty =
        BindableProperty.Create(nameof(Files), typeof(IEnumerable), typeof(FileLayout), default(IEnumerable), BindingMode.TwoWay);

        public static readonly BindableProperty IsLoadingProperty =
           BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(FileLayout), default(bool), BindingMode.TwoWay);


        public static readonly BindableProperty HasFilesProperty =
        BindableProperty.Create(nameof(HasFiles), typeof(bool), typeof(FileLayout), default(bool), BindingMode.TwoWay);

        public static readonly BindableProperty AddCommandProperty =
            BindableProperty.Create(nameof(AddCommand), typeof(ICommand), typeof(FileLayout), null, BindingMode.TwoWay);

        public static readonly BindableProperty ButtonBackgroundColorProperty =
            BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(Color), typeof(FileLayout), Color.Gray, BindingMode.TwoWay);

        public static readonly BindableProperty RequiredColorProperty =
            BindableProperty.Create(nameof(RequiredColor), typeof(Color), typeof(FileLayout), Color.Transparent, BindingMode.TwoWay);

        public static readonly BindableProperty CanEditProperty =
           BindableProperty.Create(nameof(CanEdit), typeof(bool), typeof(FileLayout), true);

        public event EventHandler AddClicked;
        public event EventHandler<DocumentModel> FileClicked;

        public ICommand AddCommand
        {
            get { return (ICommand)GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public IEnumerable Files
        {
            get { return (IEnumerable)GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public bool HasFiles
        {
            get { return (bool)GetValue(HasFilesProperty); }
            set { SetValue(HasFilesProperty, value); }
        }

        public Color ButtonBackgroundColor
        {
            get { return (Color)GetValue(ButtonBackgroundColorProperty); }
            set { SetValue(ButtonBackgroundColorProperty, value); }
        }

        public Color RequiredColor
        {
            get { return (Color)GetValue(RequiredColorProperty); }
            set { SetValue(RequiredColorProperty, value); }
        }

        public bool CanEdit
        {
            get { return (bool)GetValue(CanEditProperty); }
            set { SetValue(CanEditProperty, value); }
        }

        public FileLayout()
        {
            InitializeComponent();
            _eventAggregator = (IEventAggregator)((PrismApplication)Application.Current).Container.Resolve(typeof(IEventAggregator));
        }

        private void AddFile_Clicked(object sender, EventArgs e)
        {
            AddCommand?.Execute(null);
            AddClicked?.Invoke(sender, e);

            //_eventAggregator.GetEvent<AddFileNotificationEvent>().Publish(
            //new AddFilePayload
            //{
            //    BindingContextItems = BindingContext,
            //    FileType = fileType,
            //    IsMultiFile = isMultiFile
            //});
        }

        private void FileTapped(object sender, EventArgs e)
        {
            var layout = (FlexLayout)sender;
            var tap = (TapGestureRecognizer)layout.GestureRecognizers[0];
            var selectedFile = tap?.CommandParameter as DocumentModel;
           // FileClicked?.Invoke(sender, selectedFile);

            _eventAggregator.GetEvent<SelectFileNotificationEvent>().Publish(
               new SelectFilePayload
               {
                   BindingContextItems = BindingContext,
                   SelectedFile = selectedFile
               });
        }
    }
}