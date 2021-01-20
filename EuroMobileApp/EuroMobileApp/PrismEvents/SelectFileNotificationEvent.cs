using EuroMobileApp.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.PrismEvents
{
    public class SelectFileNotificationEvent : PubSubEvent<SelectFilePayload>
    {

    }
    public class SelectFilePayload
    {
        public object BindingContextItems { get; set; }
        public DocumentModel SelectedFile { get; set; }
    }
}
