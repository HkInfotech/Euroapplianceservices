using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class InvoiceTotalViewModel
    {
        public string Total { get; set; }
        public string HST { get; set; }
        public string TotalAmount { get; set; }
        public string AmountPaid { get; set; }
        public string TotalAmountDue { get; set; }
    }
}