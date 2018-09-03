using System.ComponentModel;

namespace VkInstruments.Core.Enums
{
    public enum Status
    {
        [Description("Не женат")]
        Single = 1,
        [Description("Встречается")]
        Meets = 2,
        [Description("Помолвлен")]
        Engaged = 3,
        [Description("Женат")]
        Married = 4,
        [Description("Всё сложно")]
        ItsComplicated = 5,
        [Description("В активном поиске")]
        TheActiveSearch = 6,
        [Description("Влюблён")]
        InLove = 7
    }
}
