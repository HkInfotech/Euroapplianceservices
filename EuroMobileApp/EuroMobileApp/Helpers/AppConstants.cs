using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Helpers
{
    public static class PageName
    {
        internal static string Navigation = "NavigationPage";
        internal static string MainPage = "MainPage";
        internal static string LoginPage = "LoginPage";
        internal static string OrderListPage = "OrderListPage";
        internal static string OrderDetailPage = "OrderDetailPage";
        internal static string WorkOrderDetailPage = "WorkOrderDetailPage";
        internal static string WorkOrderPartDetailPage = "WorkOrderPartDetailPage";
        internal static string ViewImagePage = "ViewImagePage";
        internal static string WorkOrderSearchFilterPage = "WorkOrderSearchFilterPage";
    }
    //public static class AlertMessages
    //{
    //    public static string GoOnline = "Please Go online to use this functionality!";
    //    public static string NoRecordsOffline = "No Record Found.\nPlease go online to view full list.";
    //    public static string NoRecordsOnline = "No Record Found.";

    //    internal static class EmptyState
    //    {
    //        public static string DefaultTitle = "No results found!";
    //        public static string ConnectivityTitle = "Connectivity issue!";
    //        public static string DefaultSubtitle = "Try adjusting your search or fliter to find what you're looking for";
    //        public static string ConnectivitySubtitle = "We can't connect to the server, please contact your system admin or try again later";
    //        public static string ActivityLogTitle = "No activities found!";
    //    }
    //    public static string OfflineNotAvailableForResource = "Currently, we do not have access to the INTERNET. Please connect to the Internet and try again";
    //}

    public static class Endpoint
    {
        public const string Authenticate = "/api/EuroApiV1/Authenticate";
        public const string GetOrdersByUserId = "/api/EuroApiV1/GetOrdersByUserId";
        public const string GetManufacturers = "/api/EuroApiV1/GetManufacturers";
        public const string GetApplianceTypes = "/api/EuroApiV1/GetApplianceTypes";
        public const string GetCustomerAppliancesByWorkOrderId = "/api/EuroApiV1/GetCustomerAppliancesByWorkOrderId";
        public const string GetJobStatus = "/api/EuroApiV1/GetJobStatus";
        public const string GetTechnicians = "/api/EuroApiV1/GetTechnicians";
        public const string GetWorkOrderDetailById = "/api/EuroApiV1/GetWorkOrderDetailById";
        public const string GetWorkOrderServicesbyId = "/api/EuroApiV1/GetWorkOrderServicesbyId";
        public const string GetJobNatures = "/api/EuroApiV1/GetJobNatures";
        public const string GetWorkOrderPartsById = "/api/EuroApiV1/GetWorkOrderPartsById";
        public const string GetWorkOrderTechRemarks = "/api/EuroApiV1/GetWorkOrderTechRemarks";
        public const string SaveWorkOrder = "/api/EuroApiV1/SaveWorkOrder";
        public const string SaveWorkOrderPart = "/api/EuroApiV1/SaveWorkOrderPart";
        public const string DeleteWorkOrderPart = "/api/EuroApiV1/DeleteWorkOrderPart";
    }
}
