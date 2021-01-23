using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models.Common.Request
{
    public class WorkOrderRequestModel
    {
        public int WorkOrderId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }
        public long CustomerApplianceId { get; set; }
        public long ApplianceTypeId { get; set; }
        public long ManufacturerId { get; set; }
        public string SerialNumber { get; set; }
        public string ModelNumber { get; set; }
        public List<DocumentModel> Documents { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceTime { get; set; }
        public int JobNatureId { get; set; }
        public int JobStatusId { get; set; }
        public int TechanicianId { get; set; }
        public string TicketNumber { get; set; }
        public string COD_WARN { get; set; }
        public decimal Mileage { get; set; }
        public string WorkOrderServiceNote { get; set; }

        public List<WorkOrderServiceModel> WorkOrderServices { get; set; }

        public List<WorkOrderPartModel> WorkOrderParts { get; set; }

        public string WorkOrderTechRemark { get; set; }
        public string ImageFile1 { get; set; }
        public string ImageFile2 { get; set; }
        public string ImageFile3 { get; set; }
        public string ImageFile4 { get; set; }

    }
}
