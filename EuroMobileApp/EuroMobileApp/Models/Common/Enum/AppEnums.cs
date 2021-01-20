using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models.Common.Enum
{
    public enum AttachmentType
    {
        None,
        Document,
        Image,
        Audio,
        Video,
        MultiFile
    }
    public enum ControlType
    {
        None,
        Entry,
        Picker,
        DatePicker,
        Editor,
        NumericEntry,
        ReadonlyEntry,
        PickerButton,
        Switch,
        UploadSingleFile,
        UploadMultiFile,
        PhotoGallery,
        Password
    }

    public enum DocumentOperationType
    {
        None,
        Add,
        Edit,
        Delete,
        Sync
    }
}
