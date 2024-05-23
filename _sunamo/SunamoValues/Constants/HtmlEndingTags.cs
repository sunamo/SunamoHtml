namespace SunamoHtml;


public class HtmlEndingTags
{
    static Type type = typeof(HtmlEndingTags);
    public const string b = "</b>";
    public const string i = "</i>";
    public const string s = "</s>";

    public static string Get(string value)
    {
        return "</" + value + ">"; 
    }
    //public static string Get(string value)
    //{
    //    var v = RH.GetValuesOfConsts(type, value);
    //    return v.First().Value;
    //}
}