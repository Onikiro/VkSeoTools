using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VkInstruments.Core.Enums
{
    public enum Sex
    {
        [Description("Любой пол")]
        Unknown,
        [Description("Только женский")]
        Female,
        [Description("Только мужской")]
        Male
    }
}
