using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VkInstruments.Web.Utils
{
    public static class Extensions
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> source)
        {
            var list = source.Select(item => new SelectListItem
            {
                Text = item.ToString(),
                Value = item.ToString()
            }).ToList();

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList<TKey, TValue>(this Dictionary<TKey, TValue> source)
        {
            var list = source.Select(item => new SelectListItem { Text = item.Value.ToString(), Value = item.Key.ToString() }).ToList();

            return new SelectList(list, "Value", "Text");
        }
    }
}