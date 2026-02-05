namespace SunamoHtml._public.SunamoXml.Generators;

/// <summary>
/// XML/HTML generator that builds XML/HTML content using a StringBuilder.
/// </summary>
public class XmlGeneratorHtml
{
    private readonly Stack<string>? _stack;
    private readonly bool _isUseStack;

    /// <summary>
    /// Gets or sets the StringBuilder used to build the XML/HTML content.
    /// </summary>
    public StringBuilder StringBuilder { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the XmlGeneratorHtml class without stack usage.
    /// </summary>
    public XmlGeneratorHtml() : this(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XmlGeneratorHtml class.
    /// </summary>
    /// <param name="isUseStack">Whether to use a stack to track opened tags.</param>
    public XmlGeneratorHtml(bool isUseStack)
    {
        _isUseStack = isUseStack;
        if (isUseStack) _stack = new Stack<string>();
    }

    /// <summary>
    /// Returns the current length of the generated content.
    /// </summary>
    /// <returns>The length of the StringBuilder content.</returns>
    public int Length()
    {
        return StringBuilder.Length;
    }

    /// <summary>
    /// 
    /// CZ: Zapíše nepárový tag se dvěma atributy.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attribute1Name">The first attribute name.</param>
    /// <param name="attribute1Value">The first attribute value.</param>
    /// <param name="attribute2Name">The second attribute name.</param>
    /// <param name="attribute2Value">The second attribute value.</param>
    public void WriteNonPairTagWith2Attrs(string tagName, string attribute1Name, string attribute1Value, string attribute2Name, string attribute2Value)
    {
        StringBuilder.AppendFormat("<{0} {1}=\"{2}\" {3}=\"{4}\" />", tagName, attribute1Name, attribute1Value, attribute2Name, attribute2Value);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše nepárový tag s jedním atributem.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    public void WriteNonPairTagWithAttr(string tagName, string attributeName, string attributeValue)
    {
        StringBuilder.AppendFormat("<{0} {1}=\"{2}\" />", tagName, attributeName, attributeValue);
    }

    /// <summary>
    /// Inserts text at the specified index in the StringBuilder.
    /// </summary>
    /// <param name="index">The index to insert at.</param>
    /// <param name="text">The text to insert.</param>
    public void Insert(int index, string text)
    {
        StringBuilder.Insert(index, text);
    }

    /// <summary>
    /// Appends a line terminator to the StringBuilder.
    /// </summary>
    public void AppendLine()
    {
        StringBuilder.AppendLine();
    }

    /// <summary>
    /// Starts an HTML comment.
    /// </summary>
    public void StartComment()
    {
        StringBuilder.Append("<!--");
    }

    /// <summary>
    /// Ends an HTML comment.
    /// </summary>
    public void EndComment()
    {
        StringBuilder.Append("-->");
    }

    /// <summary>
    /// 
    /// CZ: Zapíše nepárový tag s více atributy.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">List of attribute name-value pairs (alternating name, value).</param>
    public void WriteNonPairTagWithAttrs(string tagName, List<string> attributes)
    {
        WriteNonPairTagWithAttrs(tagName, attributes.ToArray());
    }

    /// <summary>
    /// 
    /// CZ: Zapíše nepárový tag s více atributy.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">Attribute name-value pairs (alternating name, value).</param>
    public void WriteNonPairTagWithAttrs(string tagName, params string[] attributes)
    {
        StringBuilder.AppendFormat("<{0} ", tagName);
        for (var i = 0; i < attributes.Length; i++)
        {
            var attributeName = attributes[i];
            object attributeValue = attributes[++i];
            StringBuilder.AppendFormat("{0}=\"{1}\" ", attributeName, attributeValue);
        }

        StringBuilder.Append(" />");
    }

    /// <summary>
    /// 
    /// CZ: Zapíše CDATA sekci se zadaným obsahem.
    /// </summary>
    /// <param name="innerCData">The content inside the CDATA section.</param>
    public void WriteCData(string innerCData)
    {
        WriteRaw(string.Format("<![CDATA[{0}]]>", innerCData));
    }

    /// <summary>
    /// 
    /// CZ: Zapíše odkaz (anchor element).
    /// </summary>
    /// <param name="link">The href URL.</param>
    /// <param name="innerText">The link text.</param>
    public void WriteLink(string link, string innerText)
    {
        WriteTagWithAttrs("a", "href", link);
        WriteRaw(innerText);
        TerminateTag("a");
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag s jedním atributem. ZASTARALÉ: Použijte WriteTagWithAttrs.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <param name="isSkipEmptyOrNull">Whether to skip writing if value is empty or null.</param>
    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWithAttr(string tagName, string attributeName, string attributeValue, bool isSkipEmptyOrNull = false)
    {
        if (isSkipEmptyOrNull)
            if (string.IsNullOrWhiteSpace(attributeValue))
                return;
        var result = string.Format("<{0} {1}=\"{2}\">", tagName, attributeName, attributeValue);
        if (_isUseStack) _stack.Push(result);
        StringBuilder.Append(result);
    }

    /// <summary>
    /// Writes raw text to the StringBuilder without any formatting.
    /// </summary>
    /// <param name="text">The text to write.</param>
    public void WriteRaw(string text)
    {
        StringBuilder.Append(text);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše zavírací tag.
    /// </summary>
    /// <param name="tagName">The tag name to close.</param>
    public void TerminateTag(string tagName)
    {
        StringBuilder.AppendFormat("</{0}>", tagName);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag bez atributů.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    public void WriteTag(string tagName)
    {
        var result = $"<{tagName}>";
        if (_isUseStack) _stack.Push(result);
        StringBuilder.Append(result);
    }

    /// <summary>
    /// Returns the generated XML/HTML content as a string.
    /// </summary>
    /// <returns>The complete XML/HTML content.</returns>
    public override string ToString()
    {
        return StringBuilder.ToString();
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag s více atributy.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">List of attribute name-value pairs (alternating name, value).</param>
    public void WriteTagWithAttrs(string tagName, List<string> attributes)
    {
        WriteTagWithAttrs(tagName, attributes.ToArray());
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag s více atributy.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">Attribute name-value pairs (alternating name, value).</param>
    public void WriteTagWithAttrs(string tagName, params string[] attributes)
    {
        WriteTagWithAttrs(true, tagName, attributes);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag s atributy ze slovníku.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">Dictionary of attribute name-value pairs.</param>
    private void WriteTagWithAttrs(string tagName, Dictionary<string, string> attributes)
    {
        WriteTagWithAttrs(true, tagName, DictionaryHelper.GetListStringFromDictionary(attributes).ToArray());
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag s atributy, kontroluje null hodnoty.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">Attribute name-value pairs (alternating name, value).</param>
    public void WriteTagWithAttrsCheckNull(string tagName, params string[] attributes)
    {
        WriteTagWithAttrs(false, tagName, attributes);
    }

    /// <summary>
    /// Checks if a text is null, empty, or equals "(null)".
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True if the text is null, empty, or "(null)", false otherwise.</returns>
    private bool IsNulledOrEmpty(string text)
    {
        if (string.IsNullOrEmpty(text) || text == "(null)") return true;
        return false;
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag s XML namespace managerem a dalšími atributy.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="namespaceManager">The XML namespace manager.</param>
    /// <param name="attributes">Additional attribute name-value pairs (alternating name, value).</param>
    public void WriteTagNamespaceManager(string tagName, XmlNamespaceManager namespaceManager, params string[] attributes)
    {
        var allAttributes = XHelper.XmlNamespaces(namespaceManager, true);
        for (var i = 0; i < attributes.Count(); i++) allAttributes.Add(attributes[i], attributes[++i]);
        WriteTagWithAttrs(tagName, allAttributes);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše nepárový tag s více atributy, volitelně připojí null hodnoty.
    /// </summary>
    /// <param name="isAppendNull">Whether to append null values.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">Attribute name-value pairs (alternating name, value).</param>
    public void WriteNonPairTagWithAttrs(bool isAppendNull, string tagName, params string[] attributes)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("<{0} ", tagName);
        for (var i = 0; i < attributes.Length; i++)
        {
            var attributeName = attributes[i];
            var attributeValue = attributes[++i];
            if ((string.IsNullOrEmpty(attributeValue) && isAppendNull) || !string.IsNullOrEmpty(attributeValue))
                if ((!IsNulledOrEmpty(attributeName) && isAppendNull) || !IsNulledOrEmpty(attributeValue))
                    stringBuilder.AppendFormat("{0}=\"{1}\" ", attributeName, attributeValue);
        }

        stringBuilder.Append(" /");
        stringBuilder.Append(">");
        var result = stringBuilder.ToString();
        if (_isUseStack) _stack.Push(result);
        StringBuilder.Append(result);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag s více atributy, volitelně připojí null hodnoty.
    /// </summary>
    /// <param name="isAppendNull">Whether to append null values.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">Attribute name-value pairs (alternating name, value).</param>
    private void WriteTagWithAttrs(bool isAppendNull, string tagName, params string[] attributes)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("<{0} ", tagName);
        for (var i = 0; i < attributes.Length; i++)
        {
            var attributeName = attributes[i];
            var attributeValue = attributes[++i];
            if ((string.IsNullOrEmpty(attributeValue) && isAppendNull) || !string.IsNullOrEmpty(attributeValue))
                if ((!IsNulledOrEmpty(attributeName) && isAppendNull) || !IsNulledOrEmpty(attributeValue))
                    stringBuilder.AppendFormat("{0}=\"{1}\" ", attributeName, attributeValue);
        }

        stringBuilder.Append(">");
        var result = stringBuilder.ToString();
        if (_isUseStack) _stack.Push(result);
        StringBuilder.Append(result);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše kompletní element s otevíracím tagem, vnitřním obsahem a zavíracím tagem.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="innerContent">The inner content.</param>
    public void WriteElement(string tagName, string innerContent)
    {
        StringBuilder.AppendFormat("<{0}>{1}</{0}>", tagName, innerContent);
    }

    /// <summary>
    /// Writes the XML declaration.
    /// </summary>
    public void WriteXmlDeclaration()
    {
        StringBuilder.Append(XmlTemplates.Xml);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše otevírací tag se dvěma atributy. ZASTARALÉ: Použijte WriteTagWithAttrs.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attribute1Name">The first attribute name.</param>
    /// <param name="attribute1Value">The first attribute value.</param>
    /// <param name="attribute2Name">The second attribute name.</param>
    /// <param name="attribute2Value">The second attribute value.</param>
    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWith2Attrs(string tagName, string attribute1Name, string attribute1Value, string attribute2Name, string attribute2Value)
    {
        var result = string.Format("<{0} {1}=\"{2}\" {3}=\"{4}\">", tagName, attribute1Name, attribute1Value, attribute2Name, attribute2Value);
        if (_isUseStack) _stack.Push(result);
        StringBuilder.Append(result);
    }

    /// <summary>
    /// 
    /// CZ: Zapíše nepárový tag bez atributů.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    public void WriteNonPairTag(string tagName)
    {
        StringBuilder.AppendFormat("<{0} />", tagName);
    }
}