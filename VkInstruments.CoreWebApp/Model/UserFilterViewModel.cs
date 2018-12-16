using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using VkInstruments.Core.Enums;
using VkInstruments.WebApp.Utils;

namespace VkInstruments.WebApp.Model
{
    public class UserFilterViewModel
    {
        public string UserIds { get; set; }

        [Display(Name = "Пол")]
        public Sex Sex { get; set; }

        [Display(Name = "От")]
        [Range(14, 80)]
        public ushort? AgeFrom { get; set; }

        [Display(Name = "До")]
        [Range(14, 80)]
        public ushort? AgeTo { get; set; }

        [Display(Name = "День")]
        [Range(1, 31)]
        public ushort? BirthDay { get; set; }

        [Display(Name = "Месяц")]
        [Range(1, 12)]
        public ushort? BirthMonth { get; set; }

        [Display(Name = "Год")]
        [Range(1900, 2018)]
        public uint? BirthYear { get; set; }

        [Display(Name = "Страна")]
        public long? Country { get; set; }

        [Display(Name = "Город")]
        public long? City { get; set; }

        public string Query { get; set; }

        public SelectList Countries { get; set; }
        public SelectList Cities { get; set; }
        public SelectList AgeRange => Enumerable.Range(14, 66).ToSelectList();
        public SelectList DayRange => Enumerable.Range(1, 31).ToSelectList();
        public SelectList MonthRange => Enumerable.Range(1, 12)
            .ToDictionary(x => x, 
                x => DateTimeFormatInfo.CurrentInfo?.GetMonthName(x))
            .ToSelectList();
    }
}
