// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml.Generators;

public class HtmlGenerator : XmlGeneratorHtml
{
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}