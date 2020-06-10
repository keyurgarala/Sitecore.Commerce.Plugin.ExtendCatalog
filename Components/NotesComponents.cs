using Sitecore.Commerce.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Commerce.Plugin.ExtendCatalog.Components
{
    public class NotesComponents:Component
    {
        [Display(Name = "Warranty Information")]
        public string WarrantyInformation { get; set; } = string.Empty;
        [Display(Name = "Internal Notes")]
        public string InternalNotes { get; set; } = string.Empty;
    }
}
