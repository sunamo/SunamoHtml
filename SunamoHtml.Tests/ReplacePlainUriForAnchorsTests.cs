namespace SunamoHtml.Tests;

/// <summary>
/// Tests for HtmlAgilityHelper.ReplacePlainUriForAnchors.
/// </summary>
public class ReplacePlainUriForAnchorsTests
{
    /// <summary>
    /// Tests that a plain address in text becomes an anchor while an address already inside an anchor or a pre block stays as it is.
    /// </summary>
    [Fact]
    public void WrapsOnlyPlainAddresses()
    {
        var result = HtmlAgilityHelper.ReplacePlainUriForAnchors(
            "<div>see https://example.org/page now <a href=\"https://a.example/\">https://a.example/</a><pre>https://pre.example/</pre></div>");

        Assert.Contains("see <a href=\"https://example.org/page\">https://example.org/page</a> now", result);
        Assert.Contains("<a href=\"https://a.example/\">https://a.example/</a>", result);
        Assert.Contains("<pre>https://pre.example/</pre>", result);
        Assert.DoesNotContain("<a href=\"https://pre.example/\"", result);
    }
}