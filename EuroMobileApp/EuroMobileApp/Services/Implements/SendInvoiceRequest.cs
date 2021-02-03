using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Services.Implements
{
    public class SendInvoiceRequest
    {
        public long CustomerId { get; set; }
        public long WorkOrderId { get; set; }
    }
}
