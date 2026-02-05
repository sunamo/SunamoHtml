// variables names: ok


///// <summary>
///// 
///// Stale se to strida #text, div
///// </summary>
//public class HtmlHelperManipulationWithoutMockTests : HtmlHelperBaseTests
//{
//    //[Fact]
//    public void HasTagName()
//    {
//        // TODO: usage *
//    }

//    //[Fact]
//    public void HighlightingWordsTest()
//    {
//        string words = "Hello world Hello hello";

//        //string veta = SH.XCharsBeforeAndAfterWholeWords(SHReplace.ReplaceAll(celyObsah, " ", CA.ToListString(AllChars.whiteSpacesChars).ToArray()), stred, naKazdeStrane);
//        var s = HtmlHelper.HighlightingWords(words, 100, 4, CA.ToListString("Hello"));
//        int i = 0;
//    }

//    //[Fact]
//    public void GetTagOfAtributeTest()
//    {
//        var node = HtmlHelper.GetTagOfAtribute(DocumentNode, HtmlTags.div, HtmlAttrs.cAttr, CssClassHello);
//        Assert.Null(node);

//        var node2 = HtmlHelper.GetTagOfAtribute(BodyNode, HtmlTags.div, HtmlAttrs.cAttr, CssClassHello);
//        Assert.NotNull(node2);
//    }

//    // OK
//    //[Fact]
//    public void ReturnTagsWithAttrRekTest()
//    {
//        var nodes = HtmlHelper.ReturnTagsWithAttrRek(DocumentNode, HtmlTags.span, HtmlAttrs.cAttr, CssClassC);
//        Assert.Equal(3, nodes.Count);
//    }

//    // OK
//    //[Fact]
//    public void GetTagOfAtributeRekTest()
//    {
//        var divFirst = HtmlHelper.GetTagOfAtributeRek(DocumentNode, HtmlTags.div, HtmlAttrs.id, DivFirstId);
//        var nodes = HtmlHelper.ReturnTagsWithAttrRek(divFirst, HtmlTags.span, HtmlAttrs.cAttr, CssClassC);
//        Assert.Equal(2, nodes.Count);

//        var node = HtmlHelper.GetTagOfAtributeRek(DocumentNode, HtmlTags.div, HtmlAttrs.cAttr, CssClassHello);
//        Assert.Null(node);

//        var node2 = HtmlHelper.GetTagOfAtributeRek(BodyNode, HtmlTags.div, HtmlAttrs.cAttr, CssClassHello);
//        Assert.NotNull(node2);
//    }
//}
