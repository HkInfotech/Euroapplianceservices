using EuroMobileApp.Configurations;
using EuroMobileApp.Helpers;
using EuroMobileApp.Models;
using EuroMobileApp.Models.Common.Enum;
using EuroMobileApp.Models.Common.Request;
using EuroMobileApp.Models.Common.Response;
using EuroMobileApp.PrismEvents;
using EuroMobileApp.Services.Interfaces;
using EuroMobileApp.Validations;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EuroMobileApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class WorkOrderDetailPageViewModel : ViewModelBase
    {

        public string PositionSelected { get; set; }

        SubscriptionToken HandleSelectedImageToken;
        public ObservableCollection<DocumentModel> DocumentItems { get; set; }

        private int ImageCount = 0;
        private DelegateCommand<string> _selectItemCommand;

        private string PhotoPath = "";
        public DelegateCommand<string> SelectItemCommand =>
            _selectItemCommand ?? (_selectItemCommand = new DelegateCommand<string>(async (a) => await ExecuteSelectItemCommand(a)));

        private async Task ExecuteSelectItemCommand(string parameter)
        {
            PositionSelected = parameter;
            //IsBusy = true;
            //switch (PositionSelected)
            //{

            //    case "1":
            //        {
            //            await GetWorkOrderDetailTabInfo();
            //            break;
            //        }
            //    case "2":
            //        {
            //            await GetWorkOrderServicesTabInfo();
            //            break;
            //        }
            //    case "3":
            //        {
            //            await GetWorkOrderPartsTabInfo();
            //            break;
            //        }
            //    case "4":
            //        {

            //            break;
            //        }
            //    case "0":
            //    default:
            //        {
            //            await GetWorkOrderApplianceTabInfo();
            //            break;
            //        }

            //}
            //IsBusy = false;


        }


        #region Services
        private readonly IEuroMobileService _euroMobileService;
        private readonly IAppSettings _appSettings;
        private readonly IAppConfiguration _appConfiguration;
        #endregion


        #region Command



        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(async () => await ExecuteSaveCommand()));

        async Task ExecuteSaveCommand()
        {
            try
            {
                if (IsConnected == true)
                {
                    ValidateFields();
                    if (OrderAppliance.IsValid && OrderManufacturer.IsValid && OrderSerialNumber.IsValid && ModelNumber.IsValid && ServiceDate.IsValid && ServiceTIme.IsValid && JobNature.IsValid && JobStatus.IsValid && Technician.IsValid && TicketNumber.IsValid && CodorWarNumber.IsValid && Mileage.IsValid && TechRemarksNote.IsValid)
                    {
                        IsBusy = true;
                        WorkOrderRequestModel request = new WorkOrderRequestModel();
                        request.ApplianceTypeId = string.IsNullOrEmpty(SelectedApplianceType.ApplianceName) ? 0 : Convert.ToInt64(SelectedApplianceType.ApplianceTypeId);
                        request.COD_WARN = CodorWarNumber.Value;
                        request.CustomerApplianceId = Applianceinfo.CustomerApplianceId;
                        request.CustomerId = Applianceinfo.CustomerId;
                        request.ServiceTime = ServiceTIme.Value.ToString();
                        request.UserId = Convert.ToInt32(_appSettings.UserId);
                        request.CustomerName = Applianceinfo.CustomerName;
                        request.JobNatureId = string.IsNullOrEmpty(SelectedJobNature.JobNature) ? 0 : Convert.ToInt32(SelectedJobNature.JobNatureId);
                        request.JobStatusId = string.IsNullOrEmpty(SelectedJobStatus.JobStatus) ? 0 : Convert.ToInt32(SelectedJobStatus.JobStatusId);
                        request.TechanicianId = string.IsNullOrEmpty(SelectedTechnician.TechnicanName) ? 0 : Convert.ToInt32(SelectedTechnician.UserId);
                        request.ManufacturerId = string.IsNullOrEmpty(SelectedManufacturer.ManufacturerName) ? 0 : Convert.ToInt32(SelectedManufacturer.ManufacturerId);
                        request.ModelNumber = ModelNumber.Value;
                        request.SerialNumber = OrderSerialNumber.Value;
                        request.WorkOrderTechRemark = TechRemarksNote.Value;
                        request.ServiceDate = ServiceDate.Value.ToString();
                        request.TicketNumber = string.IsNullOrEmpty(TicketNumber.Value) ? "" : TicketNumber.Value;
                        request.WorkOrderId = SelectedOrderModel.WorkOrderId;
                        request.WorkOrderParts = WorkOrderParts.ToList();
                        request.WorkOrderServiceNote = OrderNote.Value;
                        request.WorkOrderServices = WorkOrderServices.ToList();
                        request.Documents = DocumentItems.ToList();
                        request.Mileage = string.IsNullOrEmpty(Mileage.Value) ? 0 : Convert.ToDecimal(Mileage.Value);



                        var response = await _euroMobileService.SaveWorkOrder(request);
                        await GetWorkOrderApplianceTabInfo();
                        await GetWorkOrderDetailTabInfo();
                        await GetWorkOrderServicesTabInfo();
                        await GetWorkOrderPartsTabInfo();
                        await GetWorkOrderTechRemarkTabInfo();

                        await UserDialogsService.AlertAsync(StringResources.WorkOrderSaveAlert, null, StringResources.OK);
                        IsBusy = false;

                    }
                }
                else
                {
                    await UserDialogsService.AlertAsync($"{StringResources.OfflineNotAvailableForResource}", null, StringResources.OK);
                }


            }
            catch (Exception)
            {

            }

        }
        #endregion



        #region Properties
        public List<ApplianceTypeModel> ApplianceListItems { get; set; }
        public List<ManufacturerModel> ManufacturerListItem { get; set; }
        public List<JobStatusModel> JobStatusListItem { get; set; }
        public List<JobNatureModel> JobNatureListItem { get; set; }
        public List<TechnicianModel> TechnicianListItems { get; set; }
        public ApplianceTypeModel SelectedApplianceType { get; set; }
        public ManufacturerModel SelectedManufacturer { get; set; }
        public JobStatusModel SelectedJobStatus { get; set; }
        public JobNatureModel SelectedJobNature { get; set; }
        public TechnicianModel SelectedTechnician { get; set; }
        public WorkOrderDetailModel WorkOrderDetails { get; set; }
        public ApplianceModel Applianceinfo { get; set; }
        public OrderModel SelectedOrderModel { get; set; }
        public ObservableCollection<WorkOrderServiceChargeModel> OrderServiceCharges { get; set; }
        public ObservableCollection<WorkOrderPartModel> WorkOrderParts { get; set; }
        public ObservableCollection<WorkOrderServiceModel> WorkOrderServices { get; set; }

        #endregion

        #region Constructor

        public WorkOrderDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IEuroMobileService euroMobileService, IEventAggregator eventAggregator, IAppSettings appSettings, IAppConfiguration appConfiguration) : base(navigationService)
        {
            PositionSelected = "0";
            PageDialogService = pageDialogService;
            _appSettings = appSettings;
            OrderAppliance = new ValidatableObject<string>() { Value = string.Empty };
            OrderManufacturer = new ValidatableObject<string>() { Value = string.Empty };
            OrderSerialNumber = new ValidatableObject<string>() { Value = string.Empty };
            ModelNumber = new ValidatableObject<string>() { Value = string.Empty };
            AddValidations();
            ApplianceListItems = new List<ApplianceTypeModel>();
            ManufacturerListItem = new List<ManufacturerModel>();
            _euroMobileService = euroMobileService;
            Applianceinfo = new ApplianceModel();
            EventAggregator = eventAggregator;
            SelectedApplianceType = new ApplianceTypeModel();
            SelectedTechnician = new TechnicianModel();
            SelectedJobNature = new JobNatureModel();
            SelectedJobStatus = new JobStatusModel();
            SelectedManufacturer = new ManufacturerModel();
            SelectedOrderModel = new OrderModel();

            DocumentItems = new ObservableCollection<DocumentModel>();
            OrderServiceCharges = new ObservableCollection<WorkOrderServiceChargeModel>();
            WorkOrderParts = new ObservableCollection<WorkOrderPartModel>();
            WorkOrderServices = new ObservableCollection<WorkOrderServiceModel>();
            _appConfiguration = appConfiguration;
        }

        #endregion Constructor

        #region Commands it's Properties


        #region Appliance

        private ValidatableObject<string> _orderAppliance = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> OrderAppliance
        {
            get { return _orderAppliance; }
            set
            {
                _orderAppliance = value;
                RaisePropertyChanged(() => OrderAppliance);
            }
        }

        private DelegateCommand _validateOrderApplianceCommand;

        public DelegateCommand ValidateOrderApplianceCommand =>
            _validateOrderApplianceCommand ?? (_validateOrderApplianceCommand = new DelegateCommand(async () => await ExecuteValidateOrderApplianceCommandAsync()));

        private async Task<bool> ExecuteValidateOrderApplianceCommandAsync()
        {

            var buttons = new List<IActionSheetButton>() {

                ActionSheetButton.CreateCancelButton(StringResources.Cancel, () =>
                {
                    _orderAppliance.Value =  SelectedApplianceType.ApplianceTypeId==0 ? string.Empty : SelectedApplianceType.ApplianceName;
                })
            };
            foreach (var item in ApplianceListItems)
            {
                buttons.Add(ActionSheetButton.CreateButton(item.ApplianceName, () =>
                {
                    SelectedApplianceType = item;
                    _orderAppliance.Value = item.ApplianceName;
                }));
            }
            await PageDialogService.DisplayActionSheetAsync(null, buttons.ToArray());
            return _orderAppliance.Validate();
        }

        private ValidatableObject<string> _orderManufacturer = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> OrderManufacturer
        {
            get { return _orderManufacturer; }
            set
            {
                _orderManufacturer = value;
                RaisePropertyChanged(() => OrderManufacturer);
            }
        }

        private DelegateCommand _validateOrderManufacturerCommand;

        public DelegateCommand ValidateOrderManufacturerCommand =>
            _validateOrderManufacturerCommand ?? (_validateOrderManufacturerCommand = new DelegateCommand(async () => await ExecuteValidateOrderManufacturerCommand()));

        async private Task<bool> ExecuteValidateOrderManufacturerCommand()
        {
            var buttons = new List<IActionSheetButton>() {

                ActionSheetButton.CreateCancelButton(StringResources.Cancel, () =>
                {
                    _orderManufacturer.Value =   SelectedManufacturer.ManufacturerId==0 ? string.Empty : SelectedManufacturer.ManufacturerName;
                })
            };
            foreach (var item in ManufacturerListItem)
            {
                buttons.Add(ActionSheetButton.CreateButton(item.ManufacturerName, () =>
                {
                    SelectedManufacturer = item;
                    _orderManufacturer.Value = item.ManufacturerName;
                }));
            }
            await PageDialogService.DisplayActionSheetAsync(null, buttons.ToArray());
            return _orderManufacturer.Validate();
        }

        private ValidatableObject<string> _orderSerialNumber = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> OrderSerialNumber
        {
            get { return _orderSerialNumber; }
            set
            {
                _orderSerialNumber = value;
                RaisePropertyChanged(() => OrderSerialNumber);
            }
        }

        private DelegateCommand _validateOrderSerialNumberCommand;

        public DelegateCommand ValidateOrderSerialNumberCommand =>
            _validateOrderSerialNumberCommand ?? (_validateOrderSerialNumberCommand = new DelegateCommand(() => ExecuteValidateOrderSerialNumberCommand()));

        private bool ExecuteValidateOrderSerialNumberCommand()
        {
            return _orderSerialNumber.Validate();
        }

        private ValidatableObject<string> _modelNumber = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> ModelNumber
        {
            get { return _modelNumber; }
            set
            {
                _modelNumber = value;
                RaisePropertyChanged(() => ModelNumber);
            }
        }

        private DelegateCommand _validateOrderModelNumberCommand;

        public DelegateCommand ValidateModelNumberCommand =>
            _validateOrderModelNumberCommand ?? (_validateOrderModelNumberCommand = new DelegateCommand(() => ExecuteValidateModelNumberCommandCommand()));

        private bool ExecuteValidateModelNumberCommandCommand()
        {
            return _modelNumber.Validate();
        }

        private DelegateCommand _addImageCommand;
        public DelegateCommand AddImageCommand =>
            _addImageCommand ?? (_addImageCommand = new DelegateCommand(async () => await ExecuteAddImageCommand()));

        async Task ExecuteAddImageCommand()
        {
            try
            {

                var CanAddNew = DocumentItems.Where(a => a.Name.Equals("")).ToList();
                if (CanAddNew.Count == 0)
                {
                    await UserDialogsService.AlertAsync(StringResources.MaxImageUploadAlert, null, StringResources.OK);
                    return;
                }


                List<IActionSheetButton> buttons = new List<IActionSheetButton>()
                    {
                        ActionSheetButton.CreateButton(StringResources.Gallery, new Action(async () =>
                            {
                               await UplaodImageFromGallery();

                            })),
                         ActionSheetButton.CreateButton(StringResources.Camera, new Action(async () =>
                            {
                               await UplaodImageFromCamera();
                            })),
                            ActionSheetButton.CreateCancelButton(StringResources.Cancel, new Action(() => { }))
                    };

                await PageDialogService.DisplayActionSheetAsync(StringResources.SelectOption, buttons.ToArray());

            }
            catch (Exception ex)
            {

            }
        }

        async private Task UplaodImageFromCamera()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                UpdateDocuments(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async private Task UplaodImageFromGallery()
        {
            try
            {

                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                UpdateDocuments(photo);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        private void UpdateDocuments(FileResult photo)
        {
            if (!string.IsNullOrEmpty(PhotoPath))
            {
                var attachbytes = !string.IsNullOrWhiteSpace(PhotoPath) && File.Exists(PhotoPath) ? File.ReadAllBytes(PhotoPath) : null;
                var AddNewDocument = DocumentItems.Where(a => a.Name.Equals("")).FirstOrDefault();

                AddNewDocument.Description = "";
                AddNewDocument.LocalPath = PhotoPath;
                AddNewDocument.IsActive = 'Y';
                AddNewDocument.Name = photo.FileName;
                AddNewDocument.FileType = Models.Common.Enum.AttachmentType.Image;
                AddNewDocument.FileBlob = attachbytes;
                AddNewDocument.FileURL = PhotoPath;
                AddNewDocument.ServerDocumentPath = photo.FileName;
                AddNewDocument.DocumnetOperation = DocumentOperationType.Add;
                int i;
                i = DocumentItems.IndexOf(AddNewDocument);
                DocumentItems.RemoveAt(i);
                DocumentItems.Insert(i, AddNewDocument);
            }
        }
        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }
        public string ImageFile1 { get; set; }
        public string ImageFile2 { get; set; }
        public string ImageFile3 { get; set; }
        public string ImageFile4 { get; set; }
        #endregion Appliance

        #region Details

        private ValidatableObject<DateTime> _serviceDate = new ValidatableObject<DateTime>() { Value = DateTime.Now };

        public ValidatableObject<DateTime> ServiceDate
        {
            get { return _serviceDate; }
            set
            {
                _serviceDate = value;
                RaisePropertyChanged(() => ServiceDate);
            }
        }

        private DelegateCommand _validateServiceDateCommand;

        public DelegateCommand ValidateServiceDateCommand =>
            _validateServiceDateCommand ?? (_validateServiceDateCommand = new DelegateCommand(async () => await ExecuteValidateServiceDateCommandAsync()));

        private async Task<bool> ExecuteValidateServiceDateCommandAsync()
        {
            return _serviceDate.Validate();
        }

        private ValidatableObject<TimeSpan> _serviceTime = new ValidatableObject<TimeSpan>() { Value = new TimeSpan(0, 0, 0) };

        public ValidatableObject<TimeSpan> ServiceTIme
        {
            get { return _serviceTime; }
            set
            {
                _serviceTime = value;
                RaisePropertyChanged(() => ServiceTIme);
            }
        }

        private DelegateCommand _validateServiceTimeCommand;

        public DelegateCommand ValidateServiceTimeCommand =>
            _validateServiceTimeCommand ?? (_validateServiceTimeCommand = new DelegateCommand(async () => await ExecuteValidateServiceTimeCommandAsync()));

        private async Task<bool> ExecuteValidateServiceTimeCommandAsync()
        {
            return _serviceTime.Validate();
        }


        private ValidatableObject<string> _jobNature = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> JobNature
        {
            get { return _jobNature; }
            set
            {
                _jobNature = value;
                RaisePropertyChanged(() => JobNature);
            }
        }

        private DelegateCommand _validateJobNatureCommand;

        public DelegateCommand ValidateJobNatureCommand =>
            _validateJobNatureCommand ?? (_validateJobNatureCommand = new DelegateCommand(async () => await ExecuteValidateJobNatureCommandAsync()));

        private async Task<bool> ExecuteValidateJobNatureCommandAsync()
        {
            var buttons = new List<IActionSheetButton>() {

                ActionSheetButton.CreateCancelButton(StringResources.Cancel, () =>
                {
                    _jobNature.Value =  SelectedJobNature.JobNatureId==0 ? string.Empty : SelectedJobNature.JobNature;
                })
            };
            foreach (var item in JobNatureListItem)
            {
                buttons.Add(ActionSheetButton.CreateButton(item.JobNature, () =>
                {
                    SelectedJobNature = item;
                    _jobNature.Value = item.JobNature;
                }));
            }
            await PageDialogService.DisplayActionSheetAsync(null, buttons.ToArray());
            return _jobNature.Validate();
        }


        private ValidatableObject<string> _jobStatus = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> JobStatus
        {
            get { return _jobStatus; }
            set
            {
                _jobStatus = value;
                RaisePropertyChanged(() => JobStatus);
            }
        }

        private DelegateCommand _validateJobStatusCommand;

        public DelegateCommand ValidateJobStatusCommand =>
            _validateJobStatusCommand ?? (_validateJobStatusCommand = new DelegateCommand(async () => await ExecuteValidateJobStatusCommandAsync()));

        private async Task<bool> ExecuteValidateJobStatusCommandAsync()
        {
            var buttons = new List<IActionSheetButton>() {

                ActionSheetButton.CreateCancelButton(StringResources.Cancel, () =>
                {
                    _jobStatus.Value =  SelectedJobStatus.JobStatusId==0 ? string.Empty : SelectedJobStatus.JobStatus;
                })
            };
            foreach (var item in JobStatusListItem)
            {
                buttons.Add(ActionSheetButton.CreateButton(item.JobStatus, () =>
                {
                    SelectedJobStatus = item;
                    _jobStatus.Value = item.JobStatus;
                }));
            }
            await PageDialogService.DisplayActionSheetAsync(null, buttons.ToArray());
            return _jobStatus.Validate();
        }

        private ValidatableObject<string> _technician = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> Technician
        {
            get { return _technician; }
            set
            {
                _technician = value;
                RaisePropertyChanged(() => Technician);
            }
        }

        private DelegateCommand _validateTechnicianCommand;

        public DelegateCommand ValidateTechnicianCommand =>
            _validateTechnicianCommand ?? (_validateTechnicianCommand = new DelegateCommand(async () => await ExecuteValidateTechnicianCommandAsync()));

        private async Task<bool> ExecuteValidateTechnicianCommandAsync()
        {
            var buttons = new List<IActionSheetButton>() {

                ActionSheetButton.CreateCancelButton(StringResources.Cancel, () =>
                {
                    _technician.Value =  SelectedTechnician.UserId==0 ? string.Empty : SelectedTechnician.TechnicanName;
                })
            };
            foreach (var item in TechnicianListItems)
            {
                buttons.Add(ActionSheetButton.CreateButton(item.TechnicanName, () =>
                {
                    SelectedTechnician = item;
                    _technician.Value = item.TechnicanName;
                }));
            }
            await PageDialogService.DisplayActionSheetAsync(null, buttons.ToArray());
            return _technician.Validate();
        }

        private ValidatableObject<string> _ticketNumber = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> TicketNumber
        {
            get { return _ticketNumber; }
            set
            {
                _ticketNumber = value;
                RaisePropertyChanged(() => TicketNumber);
            }
        }

        private DelegateCommand _validateTicketNumberCommand;

        public DelegateCommand ValidateTicketNumberCommand =>
            _validateTicketNumberCommand ?? (_validateTicketNumberCommand = new DelegateCommand(async () => await ExecuteValidateTicketNumberCommandAsync()));

        private async Task<bool> ExecuteValidateTicketNumberCommandAsync()
        {
            return _ticketNumber.Validate();
        }

        private ValidatableObject<string> _codorWarNumber = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> CodorWarNumber
        {
            get { return _codorWarNumber; }
            set
            {
                _codorWarNumber = value;
                RaisePropertyChanged(() => CodorWarNumber);
            }
        }

        private DelegateCommand _validateCodorWarNumberCommand;

        public DelegateCommand ValidateCodorWarNumberCommand =>
            _validateCodorWarNumberCommand ?? (_validateCodorWarNumberCommand = new DelegateCommand(async () => await ExecuteValidateCodorWarNumberCommandAsync()));

        private async Task<bool> ExecuteValidateCodorWarNumberCommandAsync()
        {
            return _codorWarNumber.Validate();
        }

        private ValidatableObject<string> _mileage = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> Mileage
        {
            get { return _mileage; }
            set
            {
                _mileage = value;
                RaisePropertyChanged(() => Mileage);
            }
        }

        private DelegateCommand _validateMileageCommand;

        public DelegateCommand ValidateMileageCommand =>
            _validateMileageCommand ?? (_validateMileageCommand = new DelegateCommand(async () => await ExecuteValidateMileageCommandAsync()));

        private async Task<bool> ExecuteValidateMileageCommandAsync()
        {
            return _mileage.Validate();
        }

        private ValidatableObject<string> _orderNote = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> OrderNote
        {
            get { return _orderNote; }
            set
            {
                _orderNote = value;
                RaisePropertyChanged(() => OrderNote);
            }
        }

        private DelegateCommand _validateOrderNoteCommand;

        public DelegateCommand ValidateOrderNoteCommand =>
            _validateOrderNoteCommand ?? (_validateOrderNoteCommand = new DelegateCommand(async () => await ExecuteValidateOrderNoteCommandAsync()));

        private async Task<bool> ExecuteValidateOrderNoteCommandAsync()
        {
            return _orderNote.Validate();
        }

        #endregion

        #region Work Order Parts
        private DelegateCommand _navigatePartDetailPopUpCommand;
        public DelegateCommand NavigatePartDetailPopUpCommand =>
            _navigatePartDetailPopUpCommand ?? (_navigatePartDetailPopUpCommand = new DelegateCommand(async () => await ExecuteNavigatePartDetailPopUpCommand()));

        async Task ExecuteNavigatePartDetailPopUpCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters()
            {
                {
                    "PartModel",new WorkOrderPartModel(){
                        WorkOrderPartID=0,
                        WorkOrderId=SelectedOrderModel.WorkOrderId,
                        UserId=SelectedOrderModel.UserId
                    }
                }
            };
            await NavigationService.NavigateAsync(PageName.WorkOrderPartDetailPage, navigationParameters);
        }

        private DelegateCommand<WorkOrderPartModel> _editPartCommand;
        public DelegateCommand<WorkOrderPartModel> EditPartCommand =>
            _editPartCommand ?? (_editPartCommand = new DelegateCommand<WorkOrderPartModel>(async (a) => await ExecuteEditPartCommand(a)));

        async Task ExecuteEditPartCommand(WorkOrderPartModel parameter)
        {
            NavigationParameters navigationParameters = new NavigationParameters()
            {
                {
                    "PartModel",parameter
                }
            };
            await NavigationService.NavigateAsync(PageName.WorkOrderPartDetailPage, navigationParameters);
        }
        private DelegateCommand<WorkOrderPartModel> _deletePartCommand;
        public DelegateCommand<WorkOrderPartModel> DeletePartCommand =>
            _deletePartCommand ?? (_deletePartCommand = new DelegateCommand<WorkOrderPartModel>(async (a) => await ExecuteDeletePartCommand(a)));

        async Task ExecuteDeletePartCommand(WorkOrderPartModel parameter)
        {
            bool confirm = await UserDialogsService.ConfirmAsync(StringResources.DeleteWorkOrderPartConfirmAlert, null, StringResources.OK, StringResources.Cancel);
            if (confirm)
            {
                var DeletePartItem = WorkOrderParts.Where(a => a.WorkOrderPartID == parameter.WorkOrderPartID)?.FirstOrDefault();
                if (DeletePartItem != null)
                {
                    IsBusy = true;
                    await _euroMobileService.DeleteWorkOrderPart(parameter);
                    await GetWorkOrderPartsTabInfo();
                    IsBusy = false;
                    //await UserDialogsService.AlertAsync(StringResources.WorkOrderPartDeleteSuccessAlert, null, StringResources.OK);
                }
            }


        }

        #endregion


        #region TechRemarks
        private ValidatableObject<string> _techRemarksNote = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> TechRemarksNote
        {
            get { return _techRemarksNote; }
            set
            {
                _techRemarksNote = value;
                RaisePropertyChanged(() => TechRemarksNote);
            }
        }

        private DelegateCommand _validateTechRemarksNoteCommand;

        public DelegateCommand ValidateTechRemarksNoteCommand =>
            _validateTechRemarksNoteCommand ?? (_validateTechRemarksNoteCommand = new DelegateCommand(async () => await ExecuteValidateTechRemarksCommandAsync()));

        private async Task<bool> ExecuteValidateTechRemarksCommandAsync()
        {
            return _techRemarksNote.Validate();
        }
        #endregion

        #endregion

        #region Methods
        public void ValidateFields()
        {
            //ValidateOrderApplianceCommand.Execute();
            //ValidateOrderManufacturerCommand.Execute();
            if (string.IsNullOrEmpty(_orderAppliance.Value))
            {
                _orderAppliance.Value = "";
            }
            if (string.IsNullOrEmpty(OrderManufacturer.Value))
            {
                _orderManufacturer.Value = "";
            }

            ValidateOrderSerialNumberCommand.Execute();
            ValidateModelNumberCommand.Execute();
            ValidateServiceDateCommand.Execute();
            ValidateServiceTimeCommand.Execute();
            //ValidateJobNatureCommand.Execute();
            if (string.IsNullOrEmpty(JobNature.Value))
            {
                _jobNature.Value = "";
            }

            //ValidateJobStatusCommand.Execute();
            if (string.IsNullOrEmpty(_jobStatus.Value))
            {
                _jobStatus.Value = "";
            }
            //ValidateTechnicianCommand.Execute();
            if (string.IsNullOrEmpty(_technician.Value))
            {
                _technician.Value = "";
            }
            ValidateTicketNumberCommand.Execute();
            ValidateCodorWarNumberCommand.Execute();
            ValidateMileageCommand.Execute();
            ValidateOrderNoteCommand.Execute();
            //ValidateTechRemarksNoteCommand.Execute();
        }
        private void AddValidations()
        {

            //Appliance Tab Validation
            _orderAppliance.Validations.Add(new ActionSheetValidationRule<string>
            {
                ValidationMessage = StringResources.ApplianceRequired
            });
            _orderManufacturer.Validations.Add(new ActionSheetValidationRule<string>
            {
                ValidationMessage = StringResources.ManufacturerRequired
            });
            _orderSerialNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.SerialNumberRequired
            });
            _modelNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.ModelNumberRequired
            });

            _jobNature.Validations.Add(new ActionSheetValidationRule<string>
            {
                ValidationMessage = StringResources.JobNatureRequired
            });
            _jobStatus.Validations.Add(new ActionSheetValidationRule<string>
            {
                ValidationMessage = StringResources.JobStatusRequired
            });
            _technician.Validations.Add(new ActionSheetValidationRule<string>
            {
                ValidationMessage = StringResources.TechnicianRequired
            });
            
            //_ticketNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            //{
            //    ValidationMessage = StringResources.TicketNumberRequired
            //});
            //_codorWarNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            //{
            //    ValidationMessage = StringResources.CODWARRequired
            //});
            //_mileage.Validations.Add(new IsNotNullOrEmptyRule<string>
            //{
            //    ValidationMessage = StringResources.MileageRequired
            //});
            //_orderNote.Validations.Add(new IsNotNullOrEmptyRule<string>
            //{
            //    ValidationMessage = StringResources.NoteRequired
            //});

            //_techRemarksNote.Validations.Add(new IsNotNullOrEmptyRule<string>
            //{
            //    ValidationMessage = StringResources.TechRemarksRequired
            //});
            //Order Detail Tab Validation


        }

        async public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        private async Task GetWorkOrderApplianceTabInfo()
        {

            ApplianceListItems = await _euroMobileService.GetApplianceTypes() ?? new List<ApplianceTypeModel>();
            ManufacturerListItem = await _euroMobileService.GetManufacturers() ?? new List<ManufacturerModel>();
            JobStatusListItem = await _euroMobileService.GetJobStatus() ?? new List<JobStatusModel>();
            JobNatureListItem = await _euroMobileService.GetJobNatures() ?? new List<JobNatureModel>();
            TechnicianListItems = await _euroMobileService.GetTechnicians() ?? new List<TechnicianModel>();

            var appliances = await _euroMobileService.GetCustomerAppliancesByWorkOrderId(SelectedOrderModel.WorkOrderId);

            Applianceinfo = appliances.FirstOrDefault();
            SelectedApplianceType = ApplianceListItems.Where(a => a.ApplianceTypeId == Applianceinfo.ApplianceTypeId).FirstOrDefault();
            SelectedManufacturer = ManufacturerListItem.Where(a => a.ManufacturerId == Applianceinfo.ManufacturerId).FirstOrDefault();
            ModelNumber.Value = Applianceinfo.ModelNumber;
            OrderSerialNumber.Value = Applianceinfo.SerialNumber;
            OrderManufacturer.Value = SelectedManufacturer.ManufacturerName;
            OrderAppliance.Value = SelectedApplianceType.ApplianceName;
            ImageFile1 = Applianceinfo.ImageFile1;
            ImageFile2 = Applianceinfo.ImageFile2;
            ImageFile3 = Applianceinfo.ImageFile3;
            ImageFile4 = Applianceinfo.ImageFile4;

            
            DocumentItems = new ObservableCollection<DocumentModel>();
            DocumentItems.Add(new DocumentModel()
            {
                FileURL = string.IsNullOrEmpty(Applianceinfo.ImageFile1) ? "" : $"{_appConfiguration.BaseUrl}{Applianceinfo.ImageFile1}",
                IsActive = 'N',
                Id = 1,
                Name = string.IsNullOrEmpty(Applianceinfo.ImageFile1) ? "" : ReturnLastStringFormImageUrl(Applianceinfo.ImageFile1),
                ServerDocumentPath = Applianceinfo.ImageFile1,
               

            });
            DocumentItems.Add(new DocumentModel()
            {
                FileURL = string.IsNullOrEmpty(Applianceinfo.ImageFile2) ? "" : $"{_appConfiguration.BaseUrl}{Applianceinfo.ImageFile2}",
                IsActive = 'N',
                Id = 2,
                Name = string.IsNullOrEmpty(Applianceinfo.ImageFile2) ? "" : ReturnLastStringFormImageUrl(Applianceinfo.ImageFile2),
                ServerDocumentPath = Applianceinfo.ImageFile2,
            });
            DocumentItems.Add(new DocumentModel()
            {
                FileURL = string.IsNullOrEmpty(Applianceinfo.ImageFile3) ? "" : $"{_appConfiguration.BaseUrl}{Applianceinfo.ImageFile3}",
                IsActive = 'N',
                Id = 3,
                Name = string.IsNullOrEmpty(Applianceinfo.ImageFile3) ? "" : ReturnLastStringFormImageUrl(Applianceinfo.ImageFile3),
                ServerDocumentPath = Applianceinfo.ImageFile3,

            });
            DocumentItems.Add(new DocumentModel()
            {
                FileURL = string.IsNullOrEmpty(Applianceinfo.ImageFile4) ? "" : $"{_appConfiguration.BaseUrl}{Applianceinfo.ImageFile4}",
                IsActive = 'N',
                Id = 4,
                Name = string.IsNullOrEmpty(Applianceinfo.ImageFile4) ? "" : ReturnLastStringFormImageUrl(Applianceinfo.ImageFile4),
                ServerDocumentPath = Applianceinfo.ImageFile4,
            });



        }
        private string ReturnLastStringFormImageUrl(string ImageUrl)
        {
            return ImageUrl.Split('/').LastOrDefault();
        }
        private async Task GetWorkOrderDetailTabInfo()
        {
            try
            {
                // Detail Data Fill
                var details = await _euroMobileService.GetWorkOrderDetailById(SelectedOrderModel.WorkOrderId);
                if (details != null)
                {
                    if (details.JobStatusId != 0 && details.JobStatusId != null)
                    {
                        SelectedJobStatus = JobStatusListItem.Where(a => a.JobStatusId == details.JobStatusId)?.FirstOrDefault();
                        JobStatus.Value = details.JobStatus;
                    }
                    if (details.JobNatureId != 0 && details.JobNatureId != null)
                    {
                        SelectedJobNature = JobNatureListItem.Where(a => a.JobNatureId == details.JobNatureId)?.FirstOrDefault();
                        JobNature.Value = details.JobNature;
                    }
                    if (details.UserId != 0 && details.UserId != null)
                    {
                        SelectedTechnician = TechnicianListItems.Where(a => a.UserId == details.UserId)?.FirstOrDefault();
                        Technician.Value = details.UserFullName;
                    }
                    if (!string.IsNullOrEmpty(details.TicketNumber))
                    {
                        TicketNumber.Value = details.TicketNumber;
                    }

                    Mileage.Value = Convert.ToString(details.Mileage);
                    OrderNote.Value = details.Notes;
                    if (!string.IsNullOrEmpty(details.COD_WARN))
                    {
                        CodorWarNumber.Value = details.COD_WARN;
                    }

                    ServiceDate.Value = Convert.ToDateTime(details.ServiceDate);
                    if (!string.IsNullOrWhiteSpace(details.ServiceTime))
                    {
                        TimeSpan ts = TimeSpan.Parse(details.ServiceTime);
                        ServiceTIme.Value = ts;
                    }






                }
            }
            catch (Exception)
            {


            }

        }

        private async Task GetWorkOrderServicesTabInfo()
        {
            // Services Data
            var services = await _euroMobileService.GetWorkOrderServicesbyId(SelectedOrderModel.WorkOrderId) ?? new List<WorkOrderServiceModel>();
            WorkOrderServices = new ObservableCollection<WorkOrderServiceModel>(services);
        }

        private async Task GetWorkOrderPartsTabInfo()
        {
            //Work Order Parts
            var workOrderPartsResponse = await _euroMobileService.GetWorkOrderPartsById(SelectedOrderModel.WorkOrderId) ?? new List<WorkOrderPartModel>();

            WorkOrderParts = new ObservableCollection<WorkOrderPartModel>(workOrderPartsResponse) ?? new ObservableCollection<WorkOrderPartModel>();
        }
        private async Task GetWorkOrderTechRemarkTabInfo()
        {
            //Work Order Parts
            var result = await _euroMobileService.GetWorkOrderTechRemarks(SelectedOrderModel.WorkOrderId);
            TechRemarksNote.Value = result.WorkOrderTechRemarkNote;
        }
        public override void OnAppearing()
        {
            HandleSelectedImageToken = EventAggregator.GetEvent<SelectFileNotificationEvent>().Subscribe((a) => HandleSelectedImageAction(a));
        }

        public override void OnDisappearing()
        {
            EventAggregator.GetEvent<SelectFileNotificationEvent>().Unsubscribe(HandleSelectedImageToken);
        }
        public async void HandleSelectedImageAction(SelectFilePayload selectFilePayload)
        {



            if (!string.IsNullOrEmpty(selectFilePayload.SelectedFile.LocalPath) || !string.IsNullOrEmpty(selectFilePayload.SelectedFile.FileURL))
            {
                var buttons = new List<IActionSheetButton>() {

                ActionSheetButton.CreateCancelButton(StringResources.Cancel, () =>
                {

                })
            };
                buttons.Add(ActionSheetButton.CreateButton("View", async () =>
                {

                    NavigationParameters navigationParameters = new NavigationParameters()
                {
                    {
                        "DocumentModel",selectFilePayload.SelectedFile
                    }
                };
                    await NavigationServiceExtensions.TryNavigateAsync(NavigationService, PageName.ViewImagePage, navigationParameters);


                }));
                buttons.Add(ActionSheetButton.CreateButton("Delete", () =>
                {
                    DocumentModel documentToUpdate = null;
                    documentToUpdate = DocumentItems.Where(a => a.Id == selectFilePayload.SelectedFile.Id).FirstOrDefault();
                    documentToUpdate.IsActive = 'N';
                    documentToUpdate.FileURL = "";
                    documentToUpdate.LocalPath = "";
                    documentToUpdate.Name = "";
                    documentToUpdate.FileBlob = null;
                    documentToUpdate.ServerDocumentPath = "";
                    switch (documentToUpdate.Id)
                    {
                        case 1:
                            {
                                ImageFile1 = "";
                                break;
                            }
                        case 2:
                            {
                                ImageFile2 = "";
                                break;
                            }
                        case 3:
                            {
                                ImageFile3 = "";
                                break;
                            }
                        case 4:
                            {
                                ImageFile4 = "";
                                break;
                            }

                    }
                    int i;
                    i = DocumentItems.IndexOf(documentToUpdate);
                    DocumentItems.RemoveAt(i);
                    DocumentItems.Insert(i, documentToUpdate);

                }));
                await PageDialogService.DisplayActionSheetAsync(null, buttons.ToArray());
            }



        }
        async public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            switch (parameters.GetNavigationMode())
            {
                case Prism.Navigation.NavigationMode.Back:
                    {
                        if (parameters.TryGetValue("PartModel", out WorkOrderPartModel partModel) && parameters.TryGetValue("SaveWorkOrderPartResponse", out SaveWorkOrderPartResponse saveWorkOrderPartResponse))
                        {
                            IsBusy = true;
                            await GetWorkOrderPartsTabInfo();
                           // await DisplayAlertAsync(saveWorkOrderPartResponse.Status, null);
                            IsBusy = false;
                        }
                        break;
                    }
            }
            if (parameters.TryGetValue("OrderModel", out OrderModel orderModel))
            {
                SelectedOrderModel = orderModel;
                IsBusy = true;
                await GetWorkOrderApplianceTabInfo();
                await GetWorkOrderDetailTabInfo();
                await GetWorkOrderServicesTabInfo();
                await GetWorkOrderPartsTabInfo();
                await GetWorkOrderTechRemarkTabInfo();
                IsBusy = false;
            }
        }

        #endregion
    }
}