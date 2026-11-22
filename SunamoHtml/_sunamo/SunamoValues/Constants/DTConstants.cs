namespace SunamoHtml._sunamo.SunamoValues.Constants;

internal class DTConstants
{
    internal const long SecondsInMinute = 60;
    internal const long SecondsInHour = SecondsInMinute * 60;
    internal const long SecondsInDay = SecondsInHour * 24;
    internal const int YearStartUnixDate = 1970;

    internal static readonly List<string> DaysInWeekENShortcut =
        new List<string>(["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"]);

    internal static readonly List<string> DaysInWeekEN = new()
        { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    internal static readonly List<string> MonthsInYearEN = new()
    {
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November",
        "December"
    };

    internal static readonly DateTime UnixFsStart = new(YearStartUnixDate, 1, 1);

    internal static readonly List<string> DaysInWeekCS = new()
        { MondayCs, TuesdayCs, WednesdayCs, ThursdayCs, FridayCs, SaturdayCs, SundayCs };

    internal static DateTime UnixTimeStartEpoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    internal static DateTime WinTimeStartEpoch = new(1601, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    internal static readonly List<string> MonthsInYearCZ = new()
        { JanuaryCs, FebruaryCs, MarchCs, AprilCs, MayCs, JuneCs, JulyCs, AugustCs, SeptemberCs, OctoberCs, NovemberCs, DecemberCs };

    #region Days of the week CS

    internal const string MondayCs = "Pond\u011Bl\u00ED";
    internal const string TuesdayCs = "\u00DAter\u00FD";
    internal const string WednesdayCs = "St\u0159eda";
    internal const string ThursdayCs = "\u010Ctvrtek";
    internal const string FridayCs = "P\u00E1tek";
    internal const string SaturdayCs = "Sobota";
    internal const string SundayCs = "Ned\u011Ble";

    #endregion

    #region Months of the year CS

    internal const string JanuaryCs = "Leden";
    internal const string FebruaryCs = "\u00DAnor";
    internal const string MarchCs = "B\u0159ezen";
    internal const string AprilCs = "Duben";
    internal const string MayCs = "Kv\u011Bten";
    internal const string JuneCs = "\u010Cerven";
    internal const string JulyCs = "\u010Cervenec";
    internal const string AugustCs = "Srpen";
    internal const string SeptemberCs = "Z\u00E1\u0159\u00ED";
    internal const string OctoberCs = "\u0158\u00EDjen";
    internal const string NovemberCs = "Listopad";
    internal const string DecemberCs = "Prosinec";

    #endregion
}