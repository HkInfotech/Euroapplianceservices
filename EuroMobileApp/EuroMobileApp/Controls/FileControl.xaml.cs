using EuroMobileApp.Models;
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
    public partial class FileControl : StackLayout
    {

        
        public static readonly BindableProperty AddCommandProperty =
         BindableProperty.Create(nameof(AddCommand), typeof(ICommand), typeof(FileControl), null, BindingMode.TwoWay);
        public ICommand AddCommand
        {
            get { return (ICommand)GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }

        public static readonly BindableProperty HasFilesProperty = 
         BindableProperty.Create(nameof(HasFile), typeof(string), typeof(FileControl), null, BindingMode.TwoWay);
        public string HasFile
        {
            get { return (string)GetValue(HasFilesProperty); }
            set { SetValue(HasFilesProperty, value); }
        }

        public static readonly BindableProperty FilesProperty =
            BindableProperty.Create(nameof(Files), typeof(IEnumerable), typeof(FileControl), null, BindingMode.TwoWay);
        public IEnumerable Files
        {
            get { return (IEnumerable)GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }

        public static readonly BindableProperty AttachedDocumentProperty =
          BindableProperty.Create(nameof(AttachedDocument), typeof(DocumentModel), typeof(FileControl), null, BindingMode.TwoWay);
        public DocumentModel AttachedDocument
        {
            get { return (DocumentModel)GetValue(AttachedDocumentProperty); }
            set { SetValue(AttachedDocumentProperty, value); }
        }

        public static readonly BindableProperty ImageTabCommandProperty =
        BindableProperty.Create(nameof(ImageTapCommand), typeof(ICommand), typeof(FileControl), null, BindingMode.TwoWay);

        public ICommand ImageTapCommand
        {
            get { return (ICommand)GetValue(ImageTabCommandProperty); }
            set { SetValue(ImageTabCommandProperty, value); }
        }
        public event EventHandler<DocumentModel> FileClicked;





        public FileControl()
        {
            InitializeComponent();
        }

        private void FileTapped(object sender, EventArgs e)
        {
            AddCommand?.Execute(null);           
        }
    }
}