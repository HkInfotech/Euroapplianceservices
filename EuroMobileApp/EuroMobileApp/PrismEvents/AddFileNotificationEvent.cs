using EuroMobileApp.Models.Common.Enum;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.PrismEvents
{
    public class AddFileNotificationEvent : PubSubEvent<AddFilePayload>
    {

    }
    public class AddFilePayload
    {
        public object BindingContextItems { get; set; }
        public AttachmentType FileType { get; set; }
        public bool IsMultiFile { get; set; }
    }
}
