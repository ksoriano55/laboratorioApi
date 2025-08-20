using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class ResponseSAP
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string AbsoluteEntry { get; set; } = string.Empty;
        
    }

    public class ResponseSAPDraft
    {
        public string DocEntry { get; set; } = string.Empty;
        public string DocNum { get; set; } = string.Empty;
    }
}
