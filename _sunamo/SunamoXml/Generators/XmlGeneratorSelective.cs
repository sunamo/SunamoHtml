namespace SunamoHtml._sunamo.SunamoXml.Generators;

internal class XmlGeneratorSelective : XmlGeneratorHtml
{
    /// <summary>
    ///     A1 nemůže být null, musí to být v nejhorším případě Array.Empty
    /// </summary>
    /// <param name="p"></param>
    /// <param name="vynechat"></param>
    /// <param name="p_2"></param>
    internal void WriteTagWithAttrsSelective(string p, List<string> vynechat, List<string> p_2)
    {
        sb.AppendFormat("<{0} ", p);
        for (var i = 0; i < p_2.Count / 2; i++)
        {
            var nameAtt = p_2[i * 2];
            if (!vynechat.Contains(nameAtt)) sb.AppendFormat("{0}=\"{1}\"", nameAtt, p_2[i * 2 + 1]);
        }

        sb.Append(">");
    }

    public override string ToString()
    {
        return sb.ToString();
    }
}