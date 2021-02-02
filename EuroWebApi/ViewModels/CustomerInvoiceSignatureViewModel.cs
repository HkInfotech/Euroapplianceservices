using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class CustomerInvoiceSignatureViewModel
    {
        public long CustomerSignatureId { get; set; }
        public Nullable<long> WorkOrderId { get; set; }
        public string InvoiceSigned { get; set; }
        public string Covid_answer_1 { get; set; }
        public string Covid_answer_2 { get; set; }
        public string Covid_answer_3 { get; set; }
    }
}