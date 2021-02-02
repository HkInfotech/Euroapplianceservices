using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Helpers
{
    public class StringResources
    {
        private static object _locker = new object();
        private static StringResources _instance;
        public static StringResources Instance
        {
            get
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new StringResources();
                    }
                }

                return _instance;
            }
        }
        public static string OK { get; set; }
        public static string Yes { get; set; }
        public static string Confirm { get; set; }
        public static string No { get; set; }
        public static string Cancel { get; set; }
        public static string Save { get; set; }
        public static string Signatures { get; set; }

        public static string Disconnectedfromthenetwork { get; set; }

        public static string NoUserOrder { get; set; }
        public static string UserNameRequired { get; set; }
        public static string UserPasswordRequired { get; set; }
        public static string GoOnline { get; set; }
        public static string NoRecordsOffline { get; set; }
        public static string NoRecordsOnline { get; set; }
        public static string EmptyStateDefaultTitle { get; set; }
        public static string ConnectivityTitle { get; set; }
        public static string DefaultSubtitle { get; set; }
        public static string ConnectivitySubtitle { get; set; }
        public static string ActivityLogTitle { get; set; }
        public static string OfflineNotAvailableForResource { get; set; }
        public static string ApplianceRequired { get; set; }
        public static string ManufacturerRequired { get; set; }
        public static string SerialNumberRequired { get; set; }
        public static string ModelNumberRequired { get; set; }
        public static string InvalidLoginError { get; set; }
        public static string Permissiontoaccessthemedialibrary { get; set; }
        public static string Permissiontoaccessthecamera { get; set; }
        public static string Permissiontoaccessthephonelocalstorage { get; set; }
        public static string Gallery { get; set; }
        public static string Camera { get; set; }
        public static string SelectOption { get; set; }
        public static string PermissionIsNotGrantedToUseMedia { get; set; }
        public static string ServiceDateRequired { get; set; }
        public static string ServiceTimeRequired { get; set; }
        public static string JobNatureRequired { get; set; }
        public static string JobStatusRequired { get; set; }
        public static string TechnicianRequired { get; set; }
        public static string TicketNumberRequired { get; set; }
        public static string CODWARRequired { get; set; }
        public static string MileageRequired { get; set; }
        public static string NoteRequired { get; set; }
        public static string TechRemarksRequired { get; set; }
        public static string PartNameRequired { get; set; }
        public static string QuantityRequired { get; set; }
        public static string RateRequired { get; set; }
        public static string TotalAmountRequired { get; set; }
        public static string MaxImageUploadAlert { get; set; }
        public static string WorkOrderSaveAlert { get; set; }
        public static string LogOutConfirmAlert { get; set; }
        public static string DeleteWorkOrderPartConfirmAlert { get; set; }
        public static string WorkOrderPartDeleteSuccessAlert { get; set; }
        public static string CODValue { get; set; }
        public static string WARValue { get; set; }
        public static string NoAdminOrder { get; set; }
        public static string AlphanumericRequired { get; set; }
        public static string CustomerFirstName { get; set; }
        public static string CustomerFirstNameRequired { get; set; }
        public static string CustomerLastName { get; set; }
        public static string CustomerLastNameRequired { get; set; }
        public static string CustomerAddress { get; set; }
        public static string CustomerAddressRequired { get; set; }
        public static string CustomerCity { get; set; }
        public static string CustomerCityRequired { get; set; }
        public static string CustomerPostalCode { get; set; }
        public static string CustomerNearestIntersection { get; set; }
        public static string CustomerPhoneNumber { get; set; }
        public static string CustomerPhoneNumberRequired { get; set; }
        public static string CustomerCellNumber { get; set; }
        public static string CustomerCellNumberRequired { get; set; }
        public static string CustomerEmail { get; set; }
        public static string CustomerEmailRequired { get; set; }
        public static string CustomerEnterValidEmailAddress { get; set; }
        public static string CustomerDetailSaveSuccessAlert { get; set; }
        public static string OrderTotalCustomerSignature { get; set; }
        public static string SendInvoice { get; set; }
        public static string CovidQuestion1 { get; set; }
        public static string CovidQuestion2 { get; set; }
        public static string CovidQuestion3 { get; set; }

    }
}
