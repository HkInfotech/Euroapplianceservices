using EuroWebApi.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class DocumentViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int FileSize { get; set; }
        public byte[] FileBlob { get; set; }
        public string FileURL { get; set; }
        public char IsActive { get; set; }
        public bool CanRemove { get; set; }
        public bool IsDelete { get; set; }
        public bool IsNew { get; set; }
        public string ServerDocumentPath { get; set; }
        public string ServerDocumentFullPath { get; set; }
        public DocumentOperationType DocumnetOperation { get; set; }
    }
}