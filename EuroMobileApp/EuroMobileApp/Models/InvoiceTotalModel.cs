using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class InvoiceTotalModel
    {
        public string Total { get; set; }
        public string HST { get; set; }
        public string TotalAmount { get; set; }
        public string AmountPaid { get; set; }
        public string TotalAmountDue { get; set; }
    }
}
