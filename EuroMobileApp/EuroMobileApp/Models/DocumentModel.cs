using EuroMobileApp.Models.Common.Enum;
using FontAwesome;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuroMobileApp.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FileSize { get; set; }
        public byte[] FileBlob { get; set; }
        public string LocalPath { get; set; }
        public string FileURL { get; set; }
        public AttachmentType FileType { get; set; }
        public char IsActive { get; set; }
        //public bool CanRemove { get; set; }
        //public bool IsNew { get; set; }
        //public bool IsDelete { get; set; }

        public DocumentOperationType DocumnetOperation { get; set; }
        public string ServerDocumentPath { get; set; }
        public ImageSource FileImage
        {
            get
            {
                if (string.IsNullOrEmpty(LocalPath))
                {
                    return FileURL;
                }

                return ImageSource.FromFile(LocalPath);
            }
        }

        public string Document
        {
            get
            {
                if (string.IsNullOrEmpty(LocalPath))
                {
                    return string.Empty;
                }

                return IconFonts.FileAlt;
            }
        }
    }
}
