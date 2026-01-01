// variables names: ok
namespace SunamoHtml._public.SunamoEnums.Enums;

/// <summary>
/// EN: Specifies the syntax type of an HTML tag.
/// CZ: Určuje typ syntaxe HTML tagu.
/// </summary>
public enum HtmlTagSyntax
{
    /// <summary>
    /// EN: Opening tag (e.g., &lt;div&gt;).
    /// CZ: Otevírací tag (např. &lt;div&gt;).
    /// </summary>
    Start,

    /// <summary>
    /// EN: Closing tag (e.g., &lt;/div&gt;).
    /// CZ: Zavírací tag (např. &lt;/div&gt;).
    /// </summary>
    End,

    /// <summary>
    /// EN: Self-closing non-pairing tag (e.g., &lt;br /&gt;).
    /// CZ: Samouzavírací nepárový tag (např. &lt;br /&gt;).
    /// </summary>
    NonPairingEnded,

    /// <summary>
    /// EN: Non-pairing tag without self-closing (e.g., &lt;br&gt;).
    /// CZ: Nepárový tag bez samouzavření (např. &lt;br&gt;).
    /// </summary>
    NonPairingNotEnded
}
