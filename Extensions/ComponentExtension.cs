using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Commerce.Plugin.ExtendCatalog.Extensions
{
    public static class ComponentExtension
    {
        public static string GetDisplayName(this object instance, string propertyName)
        {
            var attrType = typeof(DisplayAttribute);
            var property = instance.GetType().GetProperty(propertyName);
            var displayAttributes = (DisplayAttribute)property.GetCustomAttributes(attrType, false).First();

            if (displayAttributes == null)
                return propertyName;
            else
                return displayAttributes.Name;
        }

    }
}
