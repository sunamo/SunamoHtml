// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

using SunamoHtml.Html;
using System.Text;
using System.Text.RegularExpressions;
using TextCopy;

namespace SunamoHtml.Tests;

public class HtmlAgilityHelperTests
{
    [Fact]
    public async Task CreateHtmlDocumentTest()
    {
        HttpClient httpClient = new();
        var html = await httpClient.GetStringAsync("https://www.sreality.cz//detail/pronajem/byt/1+1/praha-troja-trojska/2113655372");
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(html);
        var node = HtmlAgilityHelper.NodeWithAttr(hd.DocumentNode, true, "div", "class", "css-j7qwjs");
        int count = Regex.Matches(node.OuterHtml, "záloha").Count;
    }
    [Fact]
    public void PairsDdDtTest2()
    {
        var input = "<div class=\"css-j7qwjs\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Cena<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\">1​<!-- -->5​<!-- -->&nbsp;​<!-- -->5​<!-- -->0​<!-- -->0​<!-- -->&nbsp;​<!-- -->K​<!-- -->č​<!-- -->/​<!-- -->m​<!-- -->ě​<!-- -->s​<!-- -->í​<!-- -->c​</dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Poznámka k ceně<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\">+ záloha poplatky a energie 4.400 Kč. Vratná kauce 19.900 Kč. Odměna RK 14.900 Kč.</dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Příslušenství<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\"><div class=\"css-wkk1xk\"><div class=\"MuiBox-root css-1b7capf\">Bez výtahu</div><div class=\"MuiBox-root css-1b7capf\">Nemá bezbariérový přístup</div><div class=\"MuiBox-root css-1b7capf\">Zařízeno</div></div></dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Stavba<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\">Cihlová, V dobrém stavu, 3. podlaží</dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Infrastruktura<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\">Silnice, MHD, Autobus</dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Plocha<!-- -->:<button class=\"MuiButtonBase-root MuiIconButton-root MuiIconButton-sizeMedium css-19gpipq\" tabindex=\"0\" type=\"button\"><svg class=\"MuiSvgIcon-root MuiSvgIcon-fontSizeMedium css-1ew05aa\" focusable=\"false\" aria-hidden=\"true\" viewBox=\"0 0 24 24\" data-testid=\"InfoIcon\"><path data=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2m1 15h-2v-6h2zm0-8h-2V7h2z\"></path></svg></button></dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\"><div class=\"MuiBox-root css-e2su9m\">Užitná plocha 65 m²</div><div class=\"MuiBox-root css-e2su9m\">Celková plocha 65 m²</div></dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Vlastnictví<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\">Osobní</dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-tm1g54\">Ostatní<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-urnwfg\"><div class=\"MuiBox-root css-jmbq56\"></div><div class=\"MuiBox-root css-e2su9m\">K nastěhování ihned</div></dd></div><hr class=\"MuiDivider-root MuiDivider-fullWidth css-1vr5n9i\"><div class=\" printStyles_noPrint__nu38M MuiBox-root css-0\"><div class=\"css-11wv1wc\"><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-f5eh9m\">Zobrazeno<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-1qhj9gc\">16&nbsp;876×</dd></div><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-f5eh9m\">Vloženo<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-1qhj9gc\">4. 11. 2024</dd></div><div class=\"css-1xhj18k\"><dt class=\"MuiTypography-root MuiTypography-body1 css-f5eh9m\">Upraveno<!-- -->:</dt><dd class=\"MuiTypography-root MuiTypography-body1 css-1qhj9gc\">14. 12. 2024</dd></div></div></div></div>";
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(input);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        var pairs = HtmlAgilityHelper.PairsDdDt(hd.DocumentNode, true, dict);
        var Příslušenství = pairs["Příslušenství:"];
        var Plocha = pairs["Plocha:"];
    }
    [Fact]
    public void PairsDdDtTest()
    {
        var input = @"<div class=""b-definition-columns mb-0"">
			<dl>
	<dt>Číslo zakázky</dt>
	<dd>IDNES-01994</dd>
	<dt>Cena</dt>
	<dd>
		4 700 000 Kč
				<a href=""http://www.finmarket.cz/hypoteky/srovnani/?utm_source=reality.idnes&amp;utm_medium=text&amp;utm_campaign=hypoteka&amp;realty_value=4700000"" target=""_blank"" id=""kalkulacka-button-atribut"" rel=""nofollow"" class=""b-detail__mortgage font-sm no-print"" data-hypobrand=""Partners"">
			<span class=""icon icon--calculator color-blue"">
				<svg class=""icon__svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"">
					<use xlink:href=""/ui/image/extend/icons/icons.svg#icon-calculator"" x=""0"" y=""0"" width=""100%"" height=""100%""></use>
				</svg>
			</span>
					Spočítat hypotéku
				</a>
	</dd>
					<dt>Konstrukce budovy</dt>
					<dd>cihlová</dd>
					<dt>Stav budovy</dt>
					<dd>dobrý stav</dd>
					<dt>Poloha domu</dt>
					<dd>samostatný</dd>
					<dt>Plocha pozemku</dt>
					<dd>281 m<sup>2</sup></dd>
					<dt>Užitná plocha</dt>
					<dd>63 m<sup>2</sup></dd>
					<dt>Plocha zahrady</dt>
					<dd>163 m<sup>2</sup></dd>
					<dt>Sklep</dt>
					<dd>
			<span class=""icon icon--check"">
				<svg class=""icon__svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"">
					<use xlink:href=""/ui/image/extend/icons/icons.svg#icon-check"" x=""0"" y=""0"" width=""100%"" height=""100%""></use>
				</svg>
			</span>
		</dd>
					<dt>Parkování</dt>
					<dd>parkování na ulici</dd>
					<dt>Plyn</dt>
					<dd>zaveden</dd>
					<dt>Topení</dt>
					<dd>ústřední - elektrické</dd>
					<dt>Elektřina</dt>
					<dd>230V, zavedena</dd>
					<dt>Voda</dt>
					<dd>veřejný</dd>
					<dt>Odpad</dt>
					<dd>veřejná kanalizace</dd>
					<dt>Vybavení domu</dt>
					<dd>částečně zařízený</dd>
					<dt>PENB</dt>
						<dd>G</dd>
			</dl>
</div>";
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(input);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("<span class=\"icon icon--check\">", "✓");
        var pairs = HtmlAgilityHelper.PairsDdDt(hd.DocumentNode, true, dict);
    }
    [Fact]
    public async Task NodesWithAttrTest()
    {
        var data = await File.ReadAllTextAsync(@"D:\_Test\PlatformIndependentNuGetPackages\SunamoHtml\NodesWithAttrTest.html");
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(data);
        var c_products__list = HtmlAgilityHelper.NodeWithAttr(hd.DocumentNode, true, "div", "class", "c-products__list grid");
        var grid__cells = HtmlAgilityHelper.NodesWithAttr(c_products__list, false, "div", "class", "grid__cell", true);
        Assert.Equal(23, grid__cells.Count);
    }
    [Fact]
    public void Test1()
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        //hd.Load(@"D:\_Test\PlatformIndependentNuGetPackages\SunamoHtml\a.html");
        hd.Load(@"E:\vs\Projects\_tests\CompareTwoFiles\CompareTwoFiles\html\2.html");
        var adsParent = hd.DocumentNode.FirstChild; //HtmlAgilityHelper.NodeWithAttr(hd.DocumentNode, true, "div", "class", "row inzlist");
        var ads = HtmlAgilityHelper.NodesWithAttr(adsParent, false, "div", "class", "col-xs-12", false);
        List<string> s = new();
        StringBuilder stringBuilder = new();
        var i = 0;
        foreach (var item in ads)
        {
            var wrapperInz = HtmlAgilityHelper.NodeWithAttr(item, false, "div", "class", "poh0", true);
            if (wrapperInz == null)
            {
                Console.WriteLine("wrapperInz == null");
                continue;
            }
            var button = HtmlAgilityHelper.NodeWithAttr(wrapperInz, true, "div", "class", "inz-but clearfix", false);
            var anchor = HtmlAgilityHelper.Node(button, true, "a");
            if (anchor == null)
            {
                Console.WriteLine("Ad was skipped");
                // zatím nevím proč se tak děje
                continue;
            }
            var hrefUserDetail = "https://www.seznamka.cz" + HtmlAssistant.GetValueOfAttribute("href", anchor).TrimStart('.');
            s.Add(hrefUserDetail);
            stringBuilder.AppendLine(hrefUserDetail);
            i++;
        }
        ClipboardService.SetText(stringBuilder.ToString());
    }
}