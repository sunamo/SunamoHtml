namespace SunamoHtml._sunamo.SunamoValues.Constants;

internal class HtmlStartingTags
{
    internal const string b = "<b>";
    internal const string i = "<i>";
    internal const string s = "<s>";
    private static Type type = typeof(HtmlStartingTags);

    internal static string Get(string value)
    {
        return "<" + value + ">";
    }
    //internal static string Get(string value)
    //{
    //    var v = RH.GetValuesOfConsts(type, value);
    //    return v.First().Value;
    //}
}