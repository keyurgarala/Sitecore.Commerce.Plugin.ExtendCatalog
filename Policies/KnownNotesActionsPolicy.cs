using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
namespace Sitecore.Commerce.Plugin.ExtendCatalog.Policies
{
    public  class KnownNotesActionsPolicy:Policy
    {
        public string EditNotes { get; set; } = nameof(EditNotes);
    }
}
