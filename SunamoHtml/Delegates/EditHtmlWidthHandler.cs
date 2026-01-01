namespace SunamoHtml.Delegates;

/// <summary>
/// Delegate for editing HTML content with a given HTML node.
/// </summary>
/// <param name="htmlNode">The HTML node to reference.</param>
/// <param name="htmlContent">The HTML content string to edit.</param>
/// <returns>The modified HTML content.</returns>
public delegate string EditHtmlWidthHandler(ref HtmlNode htmlNode, string htmlContent);