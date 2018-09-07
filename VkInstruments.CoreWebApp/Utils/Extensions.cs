using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VkInstruments.CoreWebApp.Utils
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> source)
        {
            var list = source.Select(item => new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                })
                .ToList();

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList<TKey, TValue>(this Dictionary<TKey, TValue> source)
        {
            var list = new List<SelectListItem>();

            foreach (var item in source)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}