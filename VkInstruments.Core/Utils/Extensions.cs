using System;
using System.ComponentModel;

namespace VkInstruments.Core.Utils
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            return !(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute) ? value.ToString() : attribute.Description;
        }
    }
}