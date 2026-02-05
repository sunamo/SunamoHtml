namespace SunamoHtml._sunamo.SunamoValues.Constants;

/// <summary>
/// EN: Constants for date and time operations.
/// CZ: Konstanty pro operace s datem a časem.
/// </summary>
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
        { Pondeli, Utery, Streda, Ctvrtek, Patek, Sobota, Nedele };

    internal static DateTime UnixTimeStartEpoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    internal static DateTime WinTimeStartEpoch = new(1601, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    internal static readonly List<string> MonthsInYearCZ = new()
        { Leden, Unor, Brezen, Duben, Kveten, Cerven, Cervenec, Srpen, Zari, Rijen, Listopad, Prosinec };

    #region Dny v týdny CS

    internal const string Pondeli = "Pond\u011Bl\u00ED";
    internal const string Utery = "\u00DAter\u00FD";
    internal const string Streda = "St\u0159eda";
    internal const string Ctvrtek = "\u010Ctvrtek";
    internal const string Patek = "P\u00E1tek";
    internal const string Sobota = "Sobota";
    internal const string Nedele = "Ned\u011Ble";

    #endregion

    #region Měsíce v roce CS

    internal const string Leden = "Leden";
    internal const string Unor = "\u00DAnor";
    internal const string Brezen = "B\u0159ezen";
    internal const string Duben = "Duben";
    internal const string Kveten = "Kv\u011Bten";
    internal const string Cerven = "\u010Cerven";
    internal const string Cervenec = "\u010Cervenec";
    internal const string Srpen = "Srpen";
    internal const string Zari = "Z\u00E1\u0159\u00ED";
    internal const string Rijen = "\u0158\u00EDjen";
    internal const string Listopad = "Listopad";
    internal const string Prosinec = "Prosinec";

    #endregion
}