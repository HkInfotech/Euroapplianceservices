using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class CustomerInvoiceSignatureModel
    {
        public long CustomerSignatureId { get; set; }
        public Nullable<long> WorkOrderId { get; set; }
        public string InvoiceSigned { get; set; }
        public string Covid_answer_1 { get; set; }
        public string Covid_answer_2 { get; set; }
        public string Covid_answer_3 { get; set; }
    }
}
