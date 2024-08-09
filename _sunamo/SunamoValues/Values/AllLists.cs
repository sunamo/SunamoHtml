namespace SunamoHtml._sunamo.SunamoValues.Values;

internal class AllLists
{
    /// <summary>
    ///     In key is long name, in value letter
    /// </summary>
    internal static Dictionary<string, string> htmlEntitiesDict;

    /// <summary>
    ///     When entity have more names, there is just one
    ///     In key is letter, in value long name
    /// </summary>
    internal static Dictionary<string, string> htmlEntitiesFullNames;

    internal static List<char> leftBrackets = CAGConsts.ToList(AllChars.lb, AllChars.lcub, AllChars.lsqb);
    internal static List<char> rightBrackets = CAGConsts.ToList(AllChars.rb, AllChars.rcub, AllChars.rsqb);
    internal static List<string> leftBracketsS = CAGConsts.ToList(AllStrings.lb, AllStrings.lcub, AllStrings.lsqb);
    internal static List<string> rightBracketsS = CAGConsts.ToList(AllStrings.rb, AllStrings.rcub, AllStrings.rsqb);
    internal static List<string> featUpper = new List<string>(["Feat.", "Featuring", "Ft."]);
    internal static List<string> featLower = new List<string>(["feat.", "featuring", "ft."]);
    internal static List<string> OstravaCityParts = null;

    internal static List<string> HtmlNonPairTags = new List<string>(["area", "base", "br", "col", "embed", "hr", "img",
        "input", "link", "meta", "param", "source", "track", "wbr"]);

    internal static List<string> PairingTagsDontWrapToParagraph =
        new List<string>(["h1", "h2", "h3", "h4", "h5", "h6", "ul", "ol", "li"]);

    /// <summary>
    ///     generate from https://www.w3.org/Style/CSS/all-properties.en.html at 7-6-2018
    /// </summary>
    internal static List<string> allCssKeys = new List<string>(["property", "align-content", "align-items",
        "align-self", "alignment-adjust", "alignment-baseline", "all", "animation", "animation-delay",
        "animation-direction", "animation-duration", "animation-fill-mode", "animation-iteration-count",
        "animation-name", "animation-play-state", "animation-timing-function", "appearance", "azimuth",
        "backface-visibility", "background", "background-attachment", "background-blend-mode", "background-clip",
        "background-color", "background-image", "background-origin", "background-position", "background-repeat",
        "background-size", "baseline-shift", "baseline-source", "block-ellipsis", "block-overflow", "block-size",
        "block-step", "block-step-align", "block-step-insert", "block-step-round", "block-step-size", "bookmark-label",
        "bookmark-level", "bookmark-state", "border", "border-block", "border-block-color", "border-block-end",
        "border-block-end-color", "border-block-end-style", "border-block-end-width", "border-block-start",
        "border-block-start-color", "border-block-start-style", "border-block-start-width", "border-block-style",
        "border-block-width", "border-bottom", "border-bottom-color", "border-bottom-fit-length",
        "border-bottom-fit-width", "border-bottom-image", "border-bottom-left-fit-width", "border-bottom-left-image",
        "border-bottom-left-radius", "border-bottom-right-fit-length", "border-bottom-right-fit-width",
        "border-bottom-right-image", "border-bottom-right-radius", "border-bottom-style", "border-bottom-width",
        "border-bottoml-eft-fit-length", "border-boundary", "border-break", "border-collapse", "border-color",
        "border-corner-fit", "border-corner-image", "border-corner-image-transform", "border-end-end-radius",
        "border-end-start-radius", "border-fit", "border-fit-length", "border-fit-width", "border-image",
        "border-image-outset", "border-image-repeat", "border-image-slice", "border-image-source",
        "border-image-transform", "border-image-width", "border-inline", "border-inline-color", "border-inline-end",
        "border-inline-end-color", "border-inline-end-style", "border-inline-end-width", "border-inline-start",
        "border-inline-start-color", "border-inline-start-style", "border-inline-start-width", "border-inline-style",
        "border-inline-width", "border-left", "border-left-color", "border-left-fit-length", "border-left-fit-width",
        "border-left-image", "border-left-style", "border-left-width", "border-radius", "border-right",
        "border-right-color", "border-right-fit-length", "border-right-fit-width", "border-right-image",
        "border-right-style", "border-right-width", "border-spacing", "border-start-end-radius",
        "border-start-start-radius", "border-style", "border-top", "border-top-color", "border-top-fit-length",
        "border-top-fit-width", "border-top-image", "border-top-left-fit-length", "border-top-left-fit-width",
        "border-top-left-image", "border-top-left-radius", "border-top-right-fit-length", "border-top-right-fit-width",
        "border-top-right-image", "border-top-right-radius", "border-top-style", "border-top-width", "border-width",
        "bottom", "box-decoration-break", "box-shadow", "box-sizing", "box-snap", "break-after", "break-before",
        "break-inside", "caption-side", "caret", "caret-color", "caret-shape", "chains", "clear", "clip", "clip-path",
        "clip-rule", "color", "color-adjust", "color-interpolation-filters", "color-scheme", "column-count",
        "column-fill", "column-gap", "column-rule", "column-rule-color", "column-rule-style", "column-rule-width",
        "column-span", "column-width", "columns", "contain", "content", "continue", "counter-increment",
        "counter-reset", "counter-set", "cue", "cue-after", "cue-before", "cursor", "direction", "display",
        "dominant-baseline", "drop-initial-after-adjust", "drop-initial-after-align", "drop-initial-before-adjust",
        "drop-initial-before-align", "drop-initial-size", "drop-initial-value", "elevation", "empty-cells", "fill",
        "fill-break", "fill-color", "fill-image", "fill-opacity", "fill-origin", "fill-position", "fill-repeat",
        "fill-rule", "fill-size", "filter", "flex", "flex-basis", "flex-direction", "flex-flow", "flex-grow",
        "flex-shrink", "flex-wrap", "float", "float-defer", "float-offset", "float-reference", "flood-color",
        "flood-opacity", "flow", "flow-from", "flow-into", "font", "font-family", "font-feature-settings",
        "font-kerning", "font-language-override", "font-max-size", "font-min-size", "font-optical-sizing",
        "font-palette", "font-size", "font-size-adjust", "font-stretch", "font-style", "font-synthesis",
        "font-synthesis-small-caps", "font-synthesis-style", "font-synthesis-weight", "font-variant",
        "font-variant-alternates", "font-variant-caps", "font-variant-east-asian", "font-variant-emoji",
        "font-variant-ligatures", "font-variant-numeric", "font-variant-position", "font-variation-settings",
        "font-weight", "footnote-display", "footnote-policy", "forced-color-adjust", "gap",
        "glyph-orientation-vertical", "grid", "grid-area", "grid-auto-columns", "grid-auto-flow", "grid-auto-rows",
        "grid-column", "grid-column-end", "grid-column-start", "grid-row", "grid-row-end", "grid-row-start",
        "grid-template", "grid-template-areas", "grid-template-columns", "grid-template-rows", "hanging-punctuation",
        "height", "hyphenate-character", "hyphenate-limit-chars", "hyphenate-limit-last", "hyphenate-limit-lines",
        "hyphenate-limit-zone", "hyphens", "image-orientation", "image-rendering", "image-resolution",
        "initial-letters", "initial-letters-align", "initial-letters-wrap", "inline-box-align", "inline-size",
        "inline-sizing", "inset", "inset-after", "inset-before", "inset-block", "inset-block-end", "inset-block-start",
        "inset-end", "inset-inline", "inset-inline-end", "inset-inline-start", "inset-start", "isolation",
        "justify-content", "justify-items", "justify-self", "leading-trim", "leading-trim-over", "leading-trim-under",
        "left", "letter-spacing", "lighting-color", "line-break", "line-clamp", "line-grid", "line-height",
        "line-height-step", "line-padding", "line-sizing", "line-snap", "line-stacking", "line-stacking-ruby",
        "line-stacking-shift", "line-stacking-strategy", "list-style", "list-style-image", "list-style-position",
        "list-style-type", "margin", "margin-block", "margin-block-end", "margin-block-start", "margin-bottom",
        "margin-break", "margin-inline", "margin-inline-end", "margin-inline-start", "margin-left", "margin-right",
        "margin-top", "margin-trim", "marker", "marker-end", "marker-knockout-left", "marker-knockout-right",
        "marker-mid", "marker-pattern", "marker-segment", "marker-side", "marker-start", "mask", "mask-border",
        "mask-border-mode", "mask-border-outset", "mask-border-repeat", "mask-border-slice", "mask-border-source",
        "mask-border-width", "mask-clip", "mask-composite", "mask-image", "mask-mode", "mask-origin", "mask-position",
        "mask-repeat", "mask-size", "mask-type", "max-block-size", "max-height", "max-inline-size", "max-lines",
        "max-width", "min-block-size", "min-height", "min-inline-size", "min-width", "mix-blend-mode", "nav-down",
        "nav-left", "nav-right", "nav-up", "object-fit", "object-position", "offset", "offset-after", "offset-anchor",
        "offset-before", "offset-distance", "offset-end", "offset-path", "offset-position", "offset-rotate",
        "offset-start", "opacity", "order", "orphans", "outline", "outline-color", "outline-offset", "outline-style",
        "outline-width", "overflow", "overflow-block", "overflow-inline", "overflow-wrap", "overflow-x", "overflow-y",
        "overscroll-behavior", "overscroll-behavior-block", "overscroll-behavior-inline", "overscroll-behavior-",
        "overscroll-behavior-", "padding", "padding-block", "padding-block-end", "padding-block-start",
        "padding-bottom", "padding-inline", "padding-inline-end", "padding-inline-start", "padding-left",
        "padding-right", "padding-top", "page", "page-break-after", "page-break-before", "page-break-inside", "pause",
        "pause-after", "pause-before", "perspective", "perspective-origin", "pitch", "pitch-range", "place-content",
        "place-items", "place-self", "play-during", "position", "quotes", "region-fragment", "resize", "richness",
        "right", "row-gap", "ruby-align", "ruby-merge", "ruby-position", "running", "scroll-behavior", "scroll-margin",
        "scroll-margin-block", "scroll-margin-block-end", "scroll-margin-block-start", "scroll-margin-bottom",
        "scroll-margin-inline", "scroll-margin-inline-end", "scroll-margin-inline-start", "scroll-margin-left",
        "scroll-margin-right", "scroll-margin-top", "scroll-padding", "scroll-padding-block",
        "scroll-padding-block-end", "scroll-padding-block-start", "scroll-padding-bottom", "scroll-padding-inline",
        "scroll-padding-inline-end", "scroll-padding-inline-start", "scroll-padding-left", "scroll-padding-right",
        "scroll-padding-top", "scroll-snap-align", "scroll-snap-stop", "scroll-snap-type", "scrollbar-color",
        "scrollbar-gutter", "scrollbar-width", "shape-image-threshold", "shape-inside", "shape-margin", "shape-outside",
        "spatial-navigation-action", "spatial-navigation-contain", "speak", "speak-header", "speak-numeral",
        "speak-punctuation", "speech-rate", "stress", "string-set", "stroke", "stroke-align", "stroke-alignment",
        "stroke-break", "stroke-color", "stroke-dash-corner", "stroke-dash-justify", "stroke-dashadjust",
        "stroke-dasharray", "stroke-dashcorner", "stroke-dashoffset", "stroke-image", "stroke-linecap",
        "stroke-linejoin", "stroke-miterlimit", "stroke-opacity", "stroke-origin", "stroke-position", "stroke-repeat",
        "stroke-size", "stroke-width", "tab-size", "table-layout", "text-align", "text-align-all", "text-align-last",
        "text-combine-upright", "text-decoration", "text-decoration-color", "text-decoration-line",
        "text-decoration-skip", "text-decoration-skip-ink", "text-decoration-style", "text-decoration-width",
        "text-emphasis", "text-emphasis-color", "text-emphasis-position", "text-emphasis-skip", "text-emphasis-style",
        "text-group-align", "text-height", "text-indent", "text-justify", "text-orientation", "text-overflow",
        "text-shadow", "text-space-collapse", "text-space-trim", "text-spacing", "text-transform",
        "text-underline-offset", "text-underline-position", "text-wrap", "top", "transform", "transform-box",
        "transform-origin", "transform-style", "transition", "transition-delay", "transition-duration",
        "transition-property", "transition-timing-function", "unicode-bidi", "user-select", "vertical-align",
        "visibility", "voice-family", "volume", "white-space", "widows", "width", "will-change", "word-break",
        "word-spacing", "word-wrap", "wrap-after", "wrap-before", "wrap-flow", "wrap-inside", "wrap-through",
        "writing-mode", "--index"]);

    /// <summary>
    ///     https://en.wikipedia.org/wiki/Generic_top-level_domain
    /// </summary>
    internal static List<string> genericDomains = new List<string>([".com", ".org", ".net"]);

    /// <summary>
    ///     parsed with WikipediaHelper.HtmlEntitiesList
    /// </summary>
    internal static List<string> htmlEntities;

    internal static readonly List<string> BasicImageExtensions = new List<string>([".png",
        ".bmp",
        ".jpg",
        ".jpeg" /*AllExtensions.png,
    AllExtensions.bmp,
    AllExtensions.jpg,
    AllExtensions.jpeg*/]);

    internal static readonly List<string> cssTemplatesSites = new List<string>(["justfreetemplates.com",
        "templatemo.com", "free-css.com", "templated.co", "w3layouts.com"]);

    internal static readonly List<string> numberPoints = new() { AllStrings.comma, AllStrings.dot };

    static AllLists()
    {
        htmlEntities = new List<string>(["excl", "quot", "QUOT", "num", "dollar", "percnt", "amp", "AMP", "apos",
            "lpar", "rpar", "ast", "midast", "add", "comma", "period", "sol", "colon", "semi", "lt", "LT", "equals",
            "gt", "GT", "quest", "commat", "lsqb", "lbrack", "bsol", "rsqb", "rbrack", "Hat", "lowbar", "grave",
            "DiacriticalGrave", "lcub", "lbrace", "verbar", "vert", "VerticalLine", "rcub", "rbrace", "nbsp",
            "NonBreakingSpace", "iexcl", "cent", "pound", "curren", "yen", "brvbar", "sect", "Dot", "die", "DoubleDot",
            "uml", "copy", "COPY", "ordf", "laquo", "not", "shy", "reg", "circledR", "REG", "macr", "OverBar", "strns",
            "deg", "plusmn", "pm", "PlusMinus", "sup2", "sup3", "acute", "DiacriticalAcute", "micro", "para", "middot",
            "centerdot", "CenterDot", "cedil", "Cedilla", "sup1", "ordm", "raquo", "frac14", "frac12", "half", "frac34",
            "iquest", "Agrave", "Aacute", "Acirc", "Atilde", "Auml", "Aring", "AElig", "Ccedil", "Egrave", "Eacute",
            "Ecirc", "Euml", "Igrave", "Iacute", "Icirc", "Iuml", "ETH", "Ntilde", "Ograve", "Oacute", "Ocirc",
            "Otilde", "Ouml", "times", "Oslash", "Ugrave", "Uacute", "Ucirc", "Uuml", "Yacute", "THORN", "szlig",
            "agrave", "aacute", "acirc", "atilde", "auml", "aring", "aelig", "ccedil", "egrave", "eacute", "ecirc",
            "euml", "igrave", "iacute", "icirc", "iuml", "eth", "ntilde", "ograve", "oacute", "ocirc", "otilde", "ouml",
            "divide", "oslash", "ugrave", "uacute", "ucirc", "uuml", "yacute", "thorn", "yuml", "Amacr", "amacr",
            "Abreve", "abreve", "Aogon", "aogon", "Cacute", "cacute", "Ccirc", "ccirc", "Cdot", "cdot", "Ccaron",
            "ccaron", "Dcaron", "dcaron", "Dstrok", "dstrok", "Emacr", "emacr", "Edot", "edot", "Eogon", "eogon",
            "Ecaron", "ecaron", "Gcirc", "gcirc", "Gbreve", "gbreve", "Gdot", "gdot", "Gcedil", "Hcirc", "hcirc",
            "Hstrok", "hstrok", "Itilde", "itilde", "Imacr", "imacr", "Iogon", "iogon", "Idot", "imath", "inodot",
            "IJlig", "ijlig", "Jcirc", "jcirc", "Kcedil", "kcedil", "kgreen", "Lacute", "lacute", "Lcedil", "lcedil",
            "Lcaron", "lcaron", "Lmidot", "lmidot", "Lstrok", "lstrok", "Nacute", "nacute", "Ncedil", "ncedil",
            "Ncaron", "ncaron", "napos", "ENG", "eng", "Omacr", "omacr", "Odblac", "odblac", "OElig", "oelig", "Racute",
            "racute", "Rcedil", "rcedil", "Rcaron", "rcaron", "Sacute", "sacute", "Scirc", "scirc", "Scedil", "scedil",
            "Scaron", "scaron", "Tcedil", "tcedil", "Tcaron", "tcaron", "Tstrok", "tstrok", "Utilde", "utilde", "Umacr",
            "umacr", "Ubreve", "ubreve", "Uring", "uring", "Udblac", "udblac", "Uogon", "uogon", "Wcirc", "wcirc",
            "Ycirc", "ycirc", "Yuml", "Zacute", "zacute", "Zdot", "zdot", "Zcaron", "zcaron", "fnof", "imped", "gacute",
            "jmath", "circ", "caron", "Hacek", "breve", "Breve", "dot", "DiacriticalDot", "ring", "ogon", "tilde",
            "DiacriticalTilde", "dblac", "DiacriticalDoubleAcute", "DownBreve", "UnderBar", "Alpha", "Beta", "Gamma",
            "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi",
            "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega", "alpha", "beta", "gamma", "delta",
            "epsilon", "epsiv", "varepsilon", "zeta", "eta", "theta", "iota", "kappa", "lambda", "mu", "nu", "xi",
            "omicron", "pi", "rho", "sigmaf", "sigmav", "varsigma", "sigma", "tau", "upsilon", "upsi", "phi", "chi",
            "psi", "omega", "thetasym", "thetav", "vartheta", "upsih", "Upsi", "straightphi", "piv", "varpi", "Gammad",
            "gammad", "digamma", "kappav", "varkappa", "rhov", "varrho", "epsi", "straightepsilon", "bepsi",
            "backepsilon", "IOcy", "DJcy", "GJcy", "Jukcy", "DScy", "Iukcy", "YIcy", "Jsercy", "LJcy", "NJcy", "TSHcy",
            "KJcy", "Ubrcy", "DZcy", "Acy", "Bcy", "Vcy", "Gcy", "Dcy", "IEcy", "ZHcy", "Zcy", "Icy", "Jcy", "Kcy",
            "Lcy", "Mcy", "Ncy", "Ocy", "Pcy", "Rcy", "Scy", "Tcy", "Ucy", "Fcy", "KHcy", "TScy", "CHcy", "cy", "CHcy",
            "HARDcy", "Ycy", "SOFTcy", "Ecy", "YUcy", "YAcy", "acy", "bcy", "vcy", "gcy", "dcy", "iecy", "zhcy", "zcy",
            "icy", "jcy", "kcy", "lcy", "mcy", "ncy", "ocy", "pcy", "rcy", "scy", "tcy", "ucy", "fcy", "khcy", "tscy",
            "chcy", "shcy", "shchcy", "hardcy", "ycy", "softcy", "ecy", "yucy", "yacy", "iocy", "djcy", "gjcy", "jukcy",
            "dscy", "iukcy", "yicy", "jsercy", "ljcy", "njcy", "tshcy", "kjcy", "ubrcy", "dzcy", "ensp", "emsp",
            "emsp13", "emsp14", "numsp", "puncsp", "thinsp", "ThinSpace", "hairsp", "VeryThinSpace", "ZeroWidthSpace",
            "NegativeVeryThinSpace", "NegativeThinSpace", "NegativeMediumSpace", "NegativeThickSpace", "zwnj", "zwj",
            "lrm", "rlm", "hyphen", "dash", "ndash", "mdash", "horbar", "Verbar", "Vert", "lsquo", "OpenCurlyQuote",
            "rsquo", "rsquor", "CloseCurlyQuote", "sbquo", "lsquor", "ldquo", "OpenCurlyDoubleQuote", "rdquo", "rdquor",
            "CloseCurlyDoubleQuote", "bdquo", "ldquor", "dagger", "Dagger", "ddagger", "bull", "bullet", "nldr",
            "hellip", "mldr", "permil", "pertenk", "prime", "Prime", "tprime", "bprime", "backprime", "lsaquo",
            "rsaquo", "oline", "caret", "hybull", "frasl", "bsemi", "qprime", "MediumSpace", "NoBreak", "ApplyFunction",
            "af", "InvisibleTimes", "it", "InvisibleComma", "ic", "euro", "tdot", "TripleDot", "DotDot", "Copf",
            "complexes", "incare", "gscr", "hamilt", "HilbertSpace", "Hscr", "Hfr", "PoincarePlane", "quaternions",
            "Hopf", "planckh", "planck", "hbar", "plankv", "hslash", "Iscr", "imagline", "image", "Im", "imagpart",
            "Ifr", "Lscr", "lagram", "Laplacetrf", "ell", "Nopf", "naturals", "numero", "copysr", "weierp", "wp",
            "Popf", "primes", "rationals", "Qopf", "Rscr", "realine", "real", "Re", "realpart", "Rfr", "reals", "Ropf",
            "rx", "trade", "integers", "Zopf", "ohm", "mho", "Zfr", "zeetrf", "iiota", "angst", "bernou", "Bernoullis",
            "Bscr", "Cfr", "Cayleys", "escr", "Escr", "expectation", "Fscr", "Fouriertrf", "phmmat", "Mscr",
            "Mellintrf", "order", "orderof", "oscr", "alefsym", "aleph", "beth", "gimel", "daleth",
            "CapitalDifferentialD", "DD", "DifferentialD", "dd", "ExponentialE", "exponentiale", "ee", "ImaginaryI",
            "ii", "frac13", "frac23", "frac15", "frac25", "frac35", "frac45", "frac16", "frac56", "frac18", "frac38",
            "frac58", "frac78", "larr", "leftarrow", "LeftArrow", "slarr", "ShortLeftArrow", "uarr", "uparrow",
            "UpArrow", "ShortUpArrow", "rarr", "rightarrow", "RightArrow", "srarr", "ShortRightArrow", "darr",
            "downarrow", "DownArrow", "ShortDownArrow", "harr", "leftrightarrow", "LeftRightArrow", "varr",
            "updownarrow", "UpDownArrow", "nwarr", "UpperLeftArrow", "nwarrow", "nearr", "UpperRightArrow", "nearrow",
            "searr", "searrow", "LowerRightArrow", "swarr", "swarrow", "LowerLeftArrow", "nlarr", "nleftarrow", "nrarr",
            "nrightarrow", "rarrw", "rightsquigarrow", "Larr", "twoheadleftarrow", "Uarr", "Rarr", "twoheadrightarrow",
            "Darr", "larrtl", "leftarrowtail", "rarrtl", "rightarrowtail", "LeftTeeArrow", "mapstoleft", "UpTeeArrow",
            "mapstoup", "map", "RightTeeArrow", "mapsto", "DownTeeArrow", "mapstodown", "larrhk", "hookleftarrow",
            "rarrhk", "hookrightarrow", "larrlp", "looparrowleft", "rarrlp", "looparrowright", "harrw",
            "leftrightsquigarrow", "nharrow", "nleftrightarrow", "lsh", "Lsh", "rsh", "Rsh", "ldsh", "rdsh", "crarr",
            "cularr", "curvearrowleft", "curarr", "curvearrowright", "olarr", "circlearrowleft", "orarr",
            "circlearrowright", "lharu", "LeftVector", "leftharpoonup", "lhard", "leftharpoondown", "DownLeftVector",
            "uharr", "upharpoonright", "RightUpVector", "uharl", "upharpoonleft", "LeftUpVector", "rharu",
            "RightVector", "rightharpoonup", "rhard", "rightharpoondown", "DownRightVector", "dharr", "RightDownVector",
            "downharpoonright", "dharl", "LeftDownVector", "downharpoonleft", "rlarr", "rightleftarrows",
            "RightArrowLeftArrow", "udarr", "UpArrowDownArrow", "lrarr", "leftrightarrows", "LeftArrowRightArrow",
            "llarr", "leftleftarrows", "uuarr", "upuparrows", "rrarr", "rightrightarrows", "ddarr", "downdownarrows",
            "lrhar", "ReverseEquilibrium", "leftrightharpoons", "rlhar", "Equilibrium", "rightleftharpoons", "nlArr",
            "nLeftArrow", "nhArr", "nLeftrightarrow", "nrArr", "nRightArrow", "lArr", "Leftarrow", "DoubleLeftArrow",
            "uArr", "Uparrow", "DoubleUpArrow", "rArr", "Rightarrow", "Implies", "DoubleRightArrow", "dArr",
            "Downarrow", "DoubleDownArrow", "hArr", "Leftrightarrow", "DoubleLeftRightArrow", "iff", "vArr",
            "Updownarrow", "DoubleUpDownArrow", "nwArr", "neArr", "seArr", "swArr", "lAarr", "Lleftarrow", "rAarr",
            "Rrightarrow", "zigrarr", "larrb", "LeftArrowBar", "rarrb", "RightArrowBar", "duarr", "DownArrowUpArrow",
            "loarr", "roarr", "hoarr", "forall", "ForAll", "comp", "complement", "part", "PartialD", "exist", "Exists",
            "nexist", "NotExists", "nexists", "empty", "emptyset", "emptyv", "varnothing", "nabla", "Del", "isin",
            "isinv", "Element", "in", "notin", "NotElement", "notinva", "ni", "niv", "ReverseElement", "SuchThat",
            "notni", "notniva", "NotReverseElement", "prod", "Product", "coprod", "Coproduct", "sum", "Sum", "minus",
            "mnplus", "mp", "MinusPlus", "plusdo", "dotplus", "setmn", "setminus", "Backslash", "ssetmn",
            "smallsetminus", "lowast", "compfn", "SmallCircle", "radic", "Sqrt", "prop", "propto", "Proportional",
            "vprop", "varpropto", "infin", "angrt", "ang", "angle", "angmsd", "measuredangle", "angsph", "mid",
            "VerticalBar", "smid", "shortmid", "nmid", "NotVerticalBar", "nsmid", "nshortmid", "par", "parallel",
            "DoubleVerticalBar", "spar", "shortparallel", "npar", "nparallel", "NotDoubleVerticalBar", "nspar",
            "nshortparallel", "and", "wedge", "or", "vee", "cap", "cup", "int", "Integral", "int", "Integral", "tint",
            "iiint", "conint", "oint", "ContourIntegral", "Conint", "DoubleContourIntegral", "Cconint", "cwint",
            "cwconint", "ClockwiseContourIntegral", "awconint", "CounterClockwiseContourIntegral", "there4",
            "therefore", "Therefore", "becaus", "because", "Because", "ratio", "Colon", "Proportion", "minusd",
            "dotminus", "mDDot", "homtht", "sim", "Tilde", "thksim", "thicksim", "bsim", "backsim", "ac", "mstpos",
            "acd", "wreath", "VerticalTilde", "wr", "nsim", "NotTilde", "esim", "EqualTilde", "eqsim", "sime",
            "TildeEqual", "simeq", "nsime", "nsimeq", "NotTildeEqual", "cong", "TildeFullEqual", "simne", "ncong",
            "NotTildeFullEqual", "asymp", "ap", "TildeTilde", "approx", "thkap", "thickapprox", "nap", "NotTildeTilde",
            "napprox", "ape", "approxeq", "apid", "bcong", "backcong", "asympeq", "CupCap", "bump", "HumpDownHump",
            "Bumpeq", "bumpe", "HumpEqual", "Humpeq", "esdot", "DotEqual", "doteq", "edot", "doteqdot", "efdot",
            "fallingdotseq", "erdot", "risingdotseq", "colone", "coloneq", "Assign", "ecolon", "eqcolon", "ecir",
            "eqcirc", "cire", "circeq", "wedgeq", "veeeq", "trie", "triangleq", "equest", "questeq", "ne", "NotEqual",
            "equiv", "Congruent", "nequiv", "NotCongruent", "le", "LessEqual", "leq", "ge", "GreaterEqual", "geq", "lE",
            "LessFullEqual", "leqq", "gE", "GreaterFullEqual", "geqq", "lnE", "lneqq", "gnE", "gneqq", "Lt",
            "NestedLessLess", "ll", "Gt", "NestedGreaterGreater", "gg", "twixt", "between", "NotCupCap", "nlt",
            "NotLess", "nless", "ngt", "NotGreater", "ngtr", "nle", "NotLessEqual", "nleq", "nge", "NotGreaterEqual",
            "ngeq", "lsim", "LessTilde", "lesssim", "gsim", "GreaterTilde", "gtrsim", "nlsim", "NotLessTilde", "ngsim",
            "NotGreaterTilde", "lg", "lessgtr", "LessGreater", "gl", "gtrless", "GreaterLess", "ntlg", "NotLessGreater",
            "ntgl", "GreaterLess", "pr", "Precedes", "prec", "sc", "Succeeds", "succ", "prcue", "PrecedesSlantEqual",
            "preccurlyeq", "sccue", "SucceedsSlantEqual", "succcurlyeq", "prsim", "precsim", "PrecedesTilde", "sccue",
            "SucceedsSlantEqual", "succcurlyeq", "npr", "nprec", "NotPrecedes", "nsc", "nsucc", "NotSucceeds", "sub",
            "subset", "sup", "supset", "Superset", "nsub", "nsup", "sube", "SubsetEqual", "subseteq", "supe",
            "supseteq", "SupersetEqual", "nsube", "nsubseteq", "NotSubsetEqual", "nsupe", "nsupseteq",
            "NotSupersetEqual", "subne", "subsetneq", "supne", "supsetneq", "cupdot", "uplus", "UnionPlus", "sqsub",
            "SquareSubset", "sqsubset", "sqsup", "SquareSuperset", "sqsupset", "sqsube", "SquareSubsetEqual",
            "sqsubseteq", "sqsupe", "SquareSupersetEqual", "sqsupseteq", "sqcap", "SquareIntersection", "sqcup",
            "SquareUnion", "oplus", "CirclePlus", "ominus", "CircleMinus", "otimes", "CircleTimes", "osol", "odot",
            "CircleDot", "ocr", "circledcirc", "oast", "circledast", "odash", "circleddash", "plusb", "boxplus",
            "minusb", "boxminus", "timesb", "boxtimes", "sdotb", "dotsquare", "vdash", "RightTee", "dashv", "LeftTee",
            "top", "DownTee", "perp", "bottom", "bot", "UpTee", "models", "vDash", "DoubleRightTee", "Vdash", "Vvdash",
            "VDash", "nvdash", "nvDash", "nVdash", "nVDash", "prurel", "vltri", "vartriangleleft", "LeftTriangle",
            "vrtri", "vartriangleright", "RightTriangle;", "ltrie", "trianglelefteq", "LeftTriangleEqual", "rtrie",
            "trianglerighteq", "RightTriangleEqual", "origof", "imof", "mmap", "multimap", "hercon", "intcal",
            "intercal", "veebar", "barvee", "angrtvb", "lrtri", "xwedge", "Wedge", "bigwedge", "xvee", "Vee", "bigvee",
            "xcap", "Intersection", "bigcap", "xcup", "Union", "bigcup", "diam", "diamond", "Diamond", "sdot", "sstarf",
            "Star", "divonx", "divideontimes", "bowtie", "ltimes", "rtimes", "lthree", "leftthreetimes", "rthree",
            "rightthreetimes", "bsime", "backsimeq", "cuvee", "curlyvee", "cuwed", "curlywedge", "Sub", "Subset", "Sup",
            "Supset", "Cap", "Cup", "fork", "pitchfork", "epar", "ltdot", "lessdot", "gtdot", "gtrdot", "Ll", "lll",
            "Gg", "ggg", "leg", "LessEqualGreater", "lesseqgtr", "gel", "gtreqless", "GreaterEqualLess", "cuepr",
            "curlyeqprec", "cuesc", "curlyeqsucc", "nprcue", "NotPrecedesSlantEqual", "nsccue", "NotSucceedsSlantEqual",
            "nsqsube", "NotSquareSubsetEqual", "nsqsupe", "NotSquareSupersetEqual", "lnsim", "gnsim", "prnsim",
            "precnsim", "scnsim", "succnsim", "nltri", "ntriangleleft", "NotLeftTriangle", "nrtri", "ntriangleright",
            "NotRightTriangle", "nltrie", "ntrianglelefteq", "NotLeftTriangleEqual", "nrtrie", "ntrianglerighteq",
            "NotRightTriangleEqual", "vellip", "ctdot", "utdot", "dtdot", "disin", "isinsv", "isins", "isindot",
            "notinvc", "notinvb", "isinE", "nisd", "xnis", "nis", "notnivc", "notnivb", "barwed", "barwedge", "Barwed",
            "doublebarwedge", "lceil", "LeftCeiling", "rceil", "RightCeiling", "lfloor", "LeftFloor", "rfloor",
            "RightFloor", "drcrop", "dlcrop", "urcrop", "ulcrop", "bnot", "profline", "profsurf", "telrec", "target",
            "ulcorn", "ulcorner", "urcorn", "urcorner", "dlcorn", "dlcorner", "drcorn", "drcorner", "frown", "sfrown",
            "smile", "ssmile", "cylcty", "profalar", "topbot", "ovbar", "solbar", "angzarr", "lmoust", "lmoustache",
            "rmoust", "rmoustache", "tbrk", "OverBracket", "bbrk", "UnderBracket", "bbrktbrk", "OverParenthesis",
            "UnderParenthesis", "OverBrace", "UnderBrace", "trpezium", "elinters", "blank", "oS", "circledS", "boxh",
            "HorizontalLine", "boxv", "boxdr", "boxdl", "boxur", "boxul", "boxvr", "boxvl", "boxhd", "boxhu", "boxvh",
            "boxH", "boxV", "boxdR", "boxDr", "boxDR", "boxdL", "boxDl", "boxDL", "boxuR", "boxUr", "boxUR", "boxuL",
            "boxUl", "boxUL", "boxvR", "boxVr", "boxVR", "boxvL", "boxVl", "boxVL", "boxHd", "boxhD", "boxHD", "boxHu",
            "boxhU", "boxHU", "boxvH", "boxVh", "boxVH", "uhblk", "lhblk", "block", "blk14", "blk12", "blk34", "squ",
            "square", "Square", "squf", "squaref", "blacksquare", "FilledVerySmallSquare", "EmptyVerySmallSquare",
            "rect", "marker", "fltns", "xutri", "bigtriangleup", "utrif", "blacktriangle", "utri", "triangle", "rtrif",
            "blacktriangleright", "rtri", "triangleright", "xdtri", "bigtriangledown", "dtrif", "blacktriangledown",
            "dtri", "triangledown", "ltrif", "blacktriangleleft", "ltri", "triangleleft", "loz", "lozenge", "cir",
            "tridot", "xcirc", "bigcirc", "ultri", "urtri", "lltri", "EmptySmallSquare", "FilledSmallSquare", "starf",
            "bigstar", "star", "phone", "female", "male", "spades", "spadesuit", "clubs", "clubsuit", "hearts",
            "heartsuit", "diams", "diamondsuit", "sung", "flat", "natur", "natural", "sharp", "check", "checkmark",
            "cross", "malt", "Maltese", "sext", "VerticalSeparator", "lbbrk", "rbbrk", "lobrk", "LeftDoubleBracket",
            "robrk", "RightDoubleBracket", "lang", "LeftAngleBracket", "langle", "rang", "RightAngleBracket", "rangle",
            "Lang", "Rang", "loang", "roang", "xlarr", "longleftarrow", "LongLeftArrow", "xrarr", "longrightarrow",
            "LongRightArrow", "xharr", "longleftrightarrow", "LongLeftRightArrow", "xlArr", "Longleftarrow",
            "DoubleLongLeftArrow", "xrArr", "Longrightarrow", "DoubleLongRightArrow", "xhArr", "Longleftrightarrow",
            "DoubleLongLeftRightArrow", "xmap", "longmapsto", "dzigrarr", "nvlArr", "nvrArr", "nvHArr", "Map", "lbarr",
            "rbarr", "bkarow", "lBarr", "rBarr", "dbkarow", "RBarr", "drbkarow", "DDotrahd", "UpArrowBar",
            "DownArrowBar", "Rarrtl", "latail", "ratail", "lAtail", "rAtail", "larrfs", "rarrfs", "larrbfs", "rarrbfs",
            "nwarhk", "nearhk", "searhk", "hksearow", "swarhk", "hkswarow", "nwnear", "nesear", "toea", "seswar",
            "tosa", "swnwar", "rarrc", "cudarrr", "ldca", "rdca", "cudarrl", "larrpl", "curarrm", "cularrp", "rarrpl",
            "harrcir", "Uarrocir", "lurdshar", "ldrushar", "LeftRightVector", "RightUpDownVector",
            "DownLeftRightVector", "LeftUpDownVector", "LeftVectorBar", "RightVectorBar", "RightUpVectorBar",
            "RightDownVectorBar", "DownLeftVectorBar", "DownRightVectorBar", "LeftUpVectorBar", "LeftDownVectorBar",
            "LeftTeeVector", "RightTeeVector", "RightUpTeeVector", "RightDownTeeVector", "DownLeftTeeVector",
            "DownRightTeeVector", "LeftUpTeeVector", "LeftDownTeeVector", "lHar", "uHar", "rHar", "dHar", "luruhar",
            "ldrdhar", "ruluhar", "rdldhar", "lharul", "llhard", "rharul", "lrhard", "udhar", "UpEquilibrium", "duhar",
            "ReverseUpEquilibrium", "RoundImplies", "erarr", "simrarr", "larrsim", "rarrsim", "rarrap", "ltlarr",
            "gtrarr", "subrarr", "suprarr", "lfisht", "rfisht", "ufisht", "dfisht", "lopar", "ropar", "lbrke", "rbrke",
            "lbrkslu", "rbrksld", "lbrksld", "rbrkslu", "langd", "rangd", "lparlt", "rpargt", "gtlPar", "ltrPar",
            "vzigzag", "vangrt", "angrtvbd", "ange", "range", "dwangle", "uwangle", "angmsdaa", "angmsdab", "angmsdac",
            "angmsdad", "angmsdae", "angmsdaf", "angmsdag", "angmsdah", "bemptyv", "demptyv", "cemptyv", "raemptyv",
            "laemptyv", "ohbar", "omid", "opar", "operp", "olcross", "odsold", "olcir", "ofcir", "olt", "ogt",
            "cirscir", "cirE", "solb", "bsolb", "boxbox", "trisb", "rtriltri", "LeftTriangleBar", "RightTriangleBar",
            "race", "iinfin", "infintie", "nvinfin", "eparsl", "smeparsl", "eqvparsl", "lozf", "blacklozenge",
            "RuleDelayed", "dsol", "xodot", "bigodot", "xoplus", "bigoplus", "xotime", "bigotimes", "xuplus",
            "biguplus", "xsqcup", "bigsqcup", "qint", "iiiint", "fpartint", "cirfnint", "awint", "rppolint", "scpolint",
            "npolint", "pointint", "quatint", "intlarhk", "pluscir", "plusacir", "simplus", "plusdu", "plussim",
            "plustwo", "mcomma", "minusdu", "loplus", "roplus", "Cross", "timesd", "timesbar", "smashp", "lotimes",
            "rotimes", "otimesas", "Otimes", "odiv", "triplus", "triminus", "tritime", "iprod", "intprod", "amalg",
            "capdot", "ncup", "ncap", "capand", "cupor", "cupcap", "capcup", "cupbrcap", "capbrcup", "cupcup", "capcap",
            "ccups", "ccaps", "ccups", "And", "Or", "andand", "oror", "orslope", "andslope", "andv", "orv", "andd",
            "ord", "wedbar", "sdote", "simdot", "congdot", "easter", "apacir", "apE", "eplus", "pluse", "Esim",
            "Colone", "Equal", "eDDot", "ddotseq", "equivDD", "ltcir", "gtcir", "fflig", "filig", "fllig", "ffilig",
            "ffllig", "Ascr", "Cscr", "Dscr", "Gscr", "Jscr", "Kscr", "Nscr", "Oscr", "Pscr", "Qscr", "Sscr", "Tscr",
            "Uscr", "Vscr", "Wscr", "Xscr", "Yscr", "Zscr", "ascr", "bscr", "cscr", "dscr", "fscr", "hscr", "iscr",
            "jscr", "kscr", "lscr", "mscr", "nscr", "pscr", "qscr", "rscr", "sscr", "tscr", "uscr", "vscr", "wscr",
            "xscr", "yscr", "zscr", "Afr", "Bfr", "Dfr", "Efr", "Ffr", "Gfr", "Jfr", "Kfr", "Lfr", "Mfr", "Nfr", "Ofr",
            "Pfr", "Qfr", "Sfr", "Tfr", "Ufr", "Vfr", "Wfr", "Xfr", "Yfr", "afr", "bfr", "cfr", "dfr", "efr", "ffr",
            "gfr", "hfr", "ifr", "jfr", "kfr", "lfr", "mfr", "nfr", "ofr", "pfr", "qfr", "rfr", "sfr", "tfr", "ufr",
            "vfr", "wfr", "xfr", "yfr", "zfr", "Aopf", "Bopf", "Dopf", "Eopf", "Fopf", "Gopf", "Iopf", "Jopf", "Kopf",
            "Lopf", "Mopf", "Oopf", "Sopf", "Topf", "Uopf", "Vopf", "Wopf", "Xopf", "Yopf", "aopf", "bopf", "copf",
            "dopf", "eopf", "fopf", "gopf", "hopf", "iopf", "jopf", "kopf", "lopf", "mopf", "nopf", "oopf", "popf",
            "qopf", "ropf", "sopf", "topf", "uopf", "vopf", "wopf", "xopf", "yopf", "zopf"]);
    }

    internal static void InitHtmlEntitiesDict()
    {
        if (htmlEntitiesDict == null)
        {
            htmlEntitiesDict = new Dictionary<string, string>();

            #region Parsed from

            htmlEntitiesDict.Add("Tab", "  	  ");
            htmlEntitiesDict.Add("NewLine", @"
");
            htmlEntitiesDict.Add("excl", "!");
            htmlEntitiesDict.Add("quot", "\"");
            htmlEntitiesDict.Add("QUOT", "\"");
            htmlEntitiesDict.Add("num", "#");
            htmlEntitiesDict.Add("dollar", "$");
            htmlEntitiesDict.Add("percnt", "%");
            htmlEntitiesDict.Add("amp", "&");
            htmlEntitiesDict.Add("AMP", "&");
            htmlEntitiesDict.Add("apos", "'");
            htmlEntitiesDict.Add("lpar", "(");
            htmlEntitiesDict.Add("rpar", ")");
            htmlEntitiesDict.Add("ast", "*");
            htmlEntitiesDict.Add("midast", "*");
            htmlEntitiesDict.Add("plus", "+");
            htmlEntitiesDict.Add("comma", ",");
            htmlEntitiesDict.Add("period", ".");
            htmlEntitiesDict.Add("sol", "/");
            htmlEntitiesDict.Add("colon", ":");
            htmlEntitiesDict.Add("semi", ";");
            htmlEntitiesDict.Add("lt", "<");
            htmlEntitiesDict.Add("LT", "<");
            htmlEntitiesDict.Add("equals", "=");
            htmlEntitiesDict.Add("gt", ">");
            htmlEntitiesDict.Add("GT", ">");
            htmlEntitiesDict.Add("quest", "?");
            htmlEntitiesDict.Add("commat", "@");
            htmlEntitiesDict.Add("lsqb", "[");
            htmlEntitiesDict.Add("lbrack", "[");
            htmlEntitiesDict.Add("bsol", @"\");
            htmlEntitiesDict.Add("rsqb", "]");
            htmlEntitiesDict.Add("rbrack", "]");
            htmlEntitiesDict.Add("Hat", "^");
            htmlEntitiesDict.Add("lowbar", "_");
            htmlEntitiesDict.Add("UnderBar", "_");
            htmlEntitiesDict.Add("grave", "`");
            htmlEntitiesDict.Add("DiacriticalGrave", "`");
            htmlEntitiesDict.Add("lcub", "{");
            htmlEntitiesDict.Add("lbrace", "{");
            htmlEntitiesDict.Add("verbar", "|");
            htmlEntitiesDict.Add("vert", "|");
            htmlEntitiesDict.Add("VerticalLine", "|");
            htmlEntitiesDict.Add("rcub", "}");
            htmlEntitiesDict.Add("rbrace", "}");
            htmlEntitiesDict.Add("nbsp", "     ");
            htmlEntitiesDict.Add("NonBreakingSpace", "     ");
            htmlEntitiesDict.Add("iexcl", "¡");
            htmlEntitiesDict.Add("cent", "¢");
            htmlEntitiesDict.Add("pound", "£");
            htmlEntitiesDict.Add("curren", "¤");
            htmlEntitiesDict.Add("yen", "¥");
            htmlEntitiesDict.Add("brvbar", "¦");
            htmlEntitiesDict.Add("sect", "§");
            htmlEntitiesDict.Add("Dot", "¨");
            htmlEntitiesDict.Add("die", "¨");
            htmlEntitiesDict.Add("DoubleDot", "¨");
            htmlEntitiesDict.Add("uml", "¨");
            htmlEntitiesDict.Add("copy", "©");
            htmlEntitiesDict.Add("COPY", "©");
            htmlEntitiesDict.Add("ordf", "ª");
            htmlEntitiesDict.Add("laquo", "«");
            htmlEntitiesDict.Add("not", "¬");
            htmlEntitiesDict.Add("shy", "­");
            htmlEntitiesDict.Add("reg", "®");
            htmlEntitiesDict.Add("circledR", "®");
            htmlEntitiesDict.Add("REG", "®");
            htmlEntitiesDict.Add("macr", "¯");
            htmlEntitiesDict.Add("strns", "¯");
            htmlEntitiesDict.Add("deg", "°");
            htmlEntitiesDict.Add("plusmn", "±");
            htmlEntitiesDict.Add("pm", "±");
            htmlEntitiesDict.Add("PlusMinus", "±");
            htmlEntitiesDict.Add("sup2", "²");
            htmlEntitiesDict.Add("sup3", "³");
            htmlEntitiesDict.Add("acute", "´");
            htmlEntitiesDict.Add("DiacriticalAcute", "´");
            htmlEntitiesDict.Add("micro", "µ");
            htmlEntitiesDict.Add("para", "¶");
            htmlEntitiesDict.Add("middot", "·");
            htmlEntitiesDict.Add("centerdot", "·");
            htmlEntitiesDict.Add("CenterDot", "·");
            htmlEntitiesDict.Add("cedil", "¸");
            htmlEntitiesDict.Add("Cedilla", "¸");
            htmlEntitiesDict.Add("sup1", "¹");
            htmlEntitiesDict.Add("ordm", "º");
            htmlEntitiesDict.Add("raquo", "»");
            htmlEntitiesDict.Add("frac14", "¼");
            htmlEntitiesDict.Add("frac12", "½");
            htmlEntitiesDict.Add("half", "½");
            htmlEntitiesDict.Add("frac34", "¾");
            htmlEntitiesDict.Add("iquest", "¿");
            htmlEntitiesDict.Add("Agrave", "À");
            htmlEntitiesDict.Add("Aacute", "Á");
            htmlEntitiesDict.Add("Acirc", "Â");
            htmlEntitiesDict.Add("Atilde", "Ã");
            htmlEntitiesDict.Add("Auml", "Ä");
            htmlEntitiesDict.Add("Aring", "Å");
            htmlEntitiesDict.Add("angst", "Å");
            htmlEntitiesDict.Add("AElig", "Æ");
            htmlEntitiesDict.Add("Ccedil", "Ç");
            htmlEntitiesDict.Add("Egrave", "È");
            htmlEntitiesDict.Add("Eacute", "É");
            htmlEntitiesDict.Add("Ecirc", "Ê");
            htmlEntitiesDict.Add("Euml", "Ë");
            htmlEntitiesDict.Add("Igrave", "Ì");
            htmlEntitiesDict.Add("Iacute", "Í");
            htmlEntitiesDict.Add("Icirc", "Î");
            htmlEntitiesDict.Add("Iuml", "Ï");
            htmlEntitiesDict.Add("ETH", "Ð");
            htmlEntitiesDict.Add("Ntilde", "Ñ");
            htmlEntitiesDict.Add("Ograve", "Ò");
            htmlEntitiesDict.Add("Oacute", "Ó");
            htmlEntitiesDict.Add("Ocirc", "Ô");
            htmlEntitiesDict.Add("Otilde", "Õ");
            htmlEntitiesDict.Add("Ouml", "Ö");
            htmlEntitiesDict.Add("times", "×");
            htmlEntitiesDict.Add("Oslash", "Ø");
            htmlEntitiesDict.Add("Ugrave", "Ù");
            htmlEntitiesDict.Add("Uacute", "Ú");
            htmlEntitiesDict.Add("Ucirc", "Û");
            htmlEntitiesDict.Add("Uuml", "Ü");
            htmlEntitiesDict.Add("Yacute", "Ý");
            htmlEntitiesDict.Add("THORN", "Þ");
            htmlEntitiesDict.Add("szlig", "ß");
            htmlEntitiesDict.Add("agrave", "à");
            htmlEntitiesDict.Add("aacute", "á");
            htmlEntitiesDict.Add("acirc", "â");
            htmlEntitiesDict.Add("atilde", "ã");
            htmlEntitiesDict.Add("auml", "ä");
            htmlEntitiesDict.Add("aring", "å");
            htmlEntitiesDict.Add("aelig", "æ");
            htmlEntitiesDict.Add("ccedil", "ç");
            htmlEntitiesDict.Add("egrave", "è");
            htmlEntitiesDict.Add("eacute", "é");
            htmlEntitiesDict.Add("ecirc", "ê");
            htmlEntitiesDict.Add("euml", "ë");
            htmlEntitiesDict.Add("igrave", "ì");
            htmlEntitiesDict.Add("iacute", "í");
            htmlEntitiesDict.Add("icirc", "î");
            htmlEntitiesDict.Add("iuml", "ï");
            htmlEntitiesDict.Add("eth", "ð");
            htmlEntitiesDict.Add("ntilde", "ñ");
            htmlEntitiesDict.Add("ograve", "ò");
            htmlEntitiesDict.Add("oacute", "ó");
            htmlEntitiesDict.Add("ocirc", "ô");
            htmlEntitiesDict.Add("otilde", "õ");
            htmlEntitiesDict.Add("ouml", "ö");
            htmlEntitiesDict.Add("divide", "÷");
            htmlEntitiesDict.Add("div", "÷");
            htmlEntitiesDict.Add("oslash", "ø");
            htmlEntitiesDict.Add("ugrave", "ù");
            htmlEntitiesDict.Add("uacute", "ú");
            htmlEntitiesDict.Add("ucirc", "û");
            htmlEntitiesDict.Add("uuml", "ü");
            htmlEntitiesDict.Add("yacute", "ý");
            htmlEntitiesDict.Add("thorn", "þ");
            htmlEntitiesDict.Add("yuml", "ÿ");
            htmlEntitiesDict.Add("Amacr", "Ā");
            htmlEntitiesDict.Add("amacr", "ā");
            htmlEntitiesDict.Add("Abreve", "Ă");
            htmlEntitiesDict.Add("abreve", "ă");
            htmlEntitiesDict.Add("Aogon", "Ą");
            htmlEntitiesDict.Add("aogon", "ą");
            htmlEntitiesDict.Add("Cacute", "Ć");
            htmlEntitiesDict.Add("cacute", "ć");
            htmlEntitiesDict.Add("Ccirc", "Ĉ");
            htmlEntitiesDict.Add("ccirc", "ĉ");
            htmlEntitiesDict.Add("Cdot", "Ċ");
            htmlEntitiesDict.Add("cdot", "ċ");
            htmlEntitiesDict.Add("Ccaron", "Č");
            htmlEntitiesDict.Add("ccaron", "č");
            htmlEntitiesDict.Add("Dcaron", "Ď");
            htmlEntitiesDict.Add("dcaron", "ď");
            htmlEntitiesDict.Add("Dstrok", "Đ");
            htmlEntitiesDict.Add("dstrok", "đ");
            htmlEntitiesDict.Add("Emacr", "Ē");
            htmlEntitiesDict.Add("emacr", "ē");
            htmlEntitiesDict.Add("Edot", "Ė");
            htmlEntitiesDict.Add("edot", "ė");
            htmlEntitiesDict.Add("Eogon", "Ę");
            htmlEntitiesDict.Add("eogon", "ę");
            htmlEntitiesDict.Add("Ecaron", "Ě");
            htmlEntitiesDict.Add("ecaron", "ě");
            htmlEntitiesDict.Add("Gcirc", "Ĝ");
            htmlEntitiesDict.Add("gcirc", "ĝ");
            htmlEntitiesDict.Add("Gbreve", "Ğ");
            htmlEntitiesDict.Add("gbreve", "ğ");
            htmlEntitiesDict.Add("Gdot", "Ġ");
            htmlEntitiesDict.Add("gdot", "ġ");
            htmlEntitiesDict.Add("Gcedil", "Ģ");
            htmlEntitiesDict.Add("Hcirc", "Ĥ");
            htmlEntitiesDict.Add("hcirc", "ĥ");
            htmlEntitiesDict.Add("Hstrok", "Ħ");
            htmlEntitiesDict.Add("hstrok", "ħ");
            htmlEntitiesDict.Add("Itilde", "Ĩ");
            htmlEntitiesDict.Add("itilde", "ĩ");
            htmlEntitiesDict.Add("Imacr", "Ī");
            htmlEntitiesDict.Add("imacr", "ī");
            htmlEntitiesDict.Add("Iogon", "Į");
            htmlEntitiesDict.Add("iogon", "į");
            htmlEntitiesDict.Add("Idot", "İ");
            htmlEntitiesDict.Add("imath", "ı");
            htmlEntitiesDict.Add("inodot", "ı");
            htmlEntitiesDict.Add("IJlig", "Ĳ");
            htmlEntitiesDict.Add("ijlig", "ĳ");
            htmlEntitiesDict.Add("Jcirc", "Ĵ");
            htmlEntitiesDict.Add("jcirc", "ĵ");
            htmlEntitiesDict.Add("Kcedil", "Ķ");
            htmlEntitiesDict.Add("kcedil", "ķ");
            htmlEntitiesDict.Add("kgreen", "ĸ");
            htmlEntitiesDict.Add("Lacute", "Ĺ");
            htmlEntitiesDict.Add("lacute", "ĺ");
            htmlEntitiesDict.Add("Lcedil", "Ļ");
            htmlEntitiesDict.Add("lcedil", "ļ");
            htmlEntitiesDict.Add("Lcaron", "Ľ");
            htmlEntitiesDict.Add("lcaron", "ľ");
            htmlEntitiesDict.Add("Lmidot", "Ŀ");
            htmlEntitiesDict.Add("lmidot", "ŀ");
            htmlEntitiesDict.Add("Lstrok", "Ł");
            htmlEntitiesDict.Add("lstrok", "ł");
            htmlEntitiesDict.Add("Nacute", "Ń");
            htmlEntitiesDict.Add("nacute", "ń");
            htmlEntitiesDict.Add("Ncedil", "Ņ");
            htmlEntitiesDict.Add("ncedil", "ņ");
            htmlEntitiesDict.Add("Ncaron", "Ň");
            htmlEntitiesDict.Add("ncaron", "ň");
            htmlEntitiesDict.Add("napos", "ŉ");
            htmlEntitiesDict.Add("ENG", "Ŋ");
            htmlEntitiesDict.Add("eng", "ŋ");
            htmlEntitiesDict.Add("Omacr", "Ō");
            htmlEntitiesDict.Add("omacr", "ō");
            htmlEntitiesDict.Add("Odblac", "Ő");
            htmlEntitiesDict.Add("odblac", "ő");
            htmlEntitiesDict.Add("OElig", "Œ");
            htmlEntitiesDict.Add("oelig", "œ");
            htmlEntitiesDict.Add("Racute", "Ŕ");
            htmlEntitiesDict.Add("racute", "ŕ");
            htmlEntitiesDict.Add("Rcedil", "Ŗ");
            htmlEntitiesDict.Add("rcedil", "ŗ");
            htmlEntitiesDict.Add("Rcaron", "Ř");
            htmlEntitiesDict.Add("rcaron", "ř");
            htmlEntitiesDict.Add("Sacute", "Ś");
            htmlEntitiesDict.Add("sacute", "ś");
            htmlEntitiesDict.Add("Scirc", "Ŝ");
            htmlEntitiesDict.Add("scirc", "ŝ");
            htmlEntitiesDict.Add("Scedil", "Ş");
            htmlEntitiesDict.Add("scedil", "ş");
            htmlEntitiesDict.Add("Scaron", "Š");
            htmlEntitiesDict.Add("scaron", "š");
            htmlEntitiesDict.Add("Tcedil", "Ţ");
            htmlEntitiesDict.Add("tcedil", "ţ");
            htmlEntitiesDict.Add("Tcaron", "Ť");
            htmlEntitiesDict.Add("tcaron", "ť");
            htmlEntitiesDict.Add("Tstrok", "Ŧ");
            htmlEntitiesDict.Add("tstrok", "ŧ");
            htmlEntitiesDict.Add("Utilde", "Ũ");
            htmlEntitiesDict.Add("utilde", "ũ");
            htmlEntitiesDict.Add("Umacr", "Ū");
            htmlEntitiesDict.Add("umacr", "ū");
            htmlEntitiesDict.Add("Ubreve", "Ŭ");
            htmlEntitiesDict.Add("ubreve", "ŭ");
            htmlEntitiesDict.Add("Uring", "Ů");
            htmlEntitiesDict.Add("uring", "ů");
            htmlEntitiesDict.Add("Udblac", "Ű");
            htmlEntitiesDict.Add("udblac", "ű");
            htmlEntitiesDict.Add("Uogon", "Ų");
            htmlEntitiesDict.Add("uogon", "ų");
            htmlEntitiesDict.Add("Wcirc", "Ŵ");
            htmlEntitiesDict.Add("wcirc", "ŵ");
            htmlEntitiesDict.Add("Ycirc", "Ŷ");
            htmlEntitiesDict.Add("ycirc", "ŷ");
            htmlEntitiesDict.Add("Yuml", "Ÿ");
            htmlEntitiesDict.Add("Zacute", "Ź");
            htmlEntitiesDict.Add("zacute", "ź");
            htmlEntitiesDict.Add("Zdot", "Ż");
            htmlEntitiesDict.Add("zdot", "ż");
            htmlEntitiesDict.Add("Zcaron", "Ž");
            htmlEntitiesDict.Add("zcaron", "ž");
            htmlEntitiesDict.Add("fnof", "ƒ");
            htmlEntitiesDict.Add("imped", "Ƶ");
            htmlEntitiesDict.Add("gacute", "ǵ");
            htmlEntitiesDict.Add("jmath", "ȷ");
            htmlEntitiesDict.Add("circ", "ˆ");
            htmlEntitiesDict.Add("caron", "ˇ");
            htmlEntitiesDict.Add("Hacek", "ˇ");
            htmlEntitiesDict.Add("breve", "˘");
            htmlEntitiesDict.Add("Breve", "˘");
            htmlEntitiesDict.Add("dot", "˙");
            htmlEntitiesDict.Add("DiacriticalDot", "˙");
            htmlEntitiesDict.Add("ring", "˚");
            htmlEntitiesDict.Add("ogon", "˛");
            htmlEntitiesDict.Add("tilde", "˜");
            htmlEntitiesDict.Add("DiacriticalTilde", "˜");
            htmlEntitiesDict.Add("dblac", "˝");
            htmlEntitiesDict.Add("DiacriticalDoubleAcute", "˝");
            htmlEntitiesDict.Add("DownBreve", "̑");
            htmlEntitiesDict.Add("Alpha", "Α");
            htmlEntitiesDict.Add("Beta", "Β");
            htmlEntitiesDict.Add("Gamma", "Γ");
            htmlEntitiesDict.Add("Delta", "Δ");
            htmlEntitiesDict.Add("Epsilon", "Ε");
            htmlEntitiesDict.Add("Zeta", "Ζ");
            htmlEntitiesDict.Add("Eta", "Η");
            htmlEntitiesDict.Add("Theta", "Θ");
            htmlEntitiesDict.Add("Iota", "Ι");
            htmlEntitiesDict.Add("Kappa", "Κ");
            htmlEntitiesDict.Add("Lambda", "Λ");
            htmlEntitiesDict.Add("Mu", "Μ");
            htmlEntitiesDict.Add("Nu", "Ν");
            htmlEntitiesDict.Add("Xi", "Ξ");
            htmlEntitiesDict.Add("Omicron", "Ο");
            htmlEntitiesDict.Add("Pi", "Π");
            htmlEntitiesDict.Add("Rho", "Ρ");
            htmlEntitiesDict.Add("Sigma", "Σ");
            htmlEntitiesDict.Add("Tau", "Τ");
            htmlEntitiesDict.Add("Upsilon", "Υ");
            htmlEntitiesDict.Add("Phi", "Φ");
            htmlEntitiesDict.Add("Chi", "Χ");
            htmlEntitiesDict.Add("Psi", "Ψ");
            htmlEntitiesDict.Add("Omega", "Ω");
            htmlEntitiesDict.Add("ohm", "Ω");
            htmlEntitiesDict.Add("alpha", "α");
            htmlEntitiesDict.Add("beta", "β");
            htmlEntitiesDict.Add("gamma", "γ");
            htmlEntitiesDict.Add("delta", "δ");
            htmlEntitiesDict.Add("epsi", "ε");
            htmlEntitiesDict.Add("epsilon", "ε");
            htmlEntitiesDict.Add("zeta", "ζ");
            htmlEntitiesDict.Add("eta", "η");
            htmlEntitiesDict.Add("theta", "θ");
            htmlEntitiesDict.Add("iota", "ι");
            htmlEntitiesDict.Add("kappa", "κ");
            htmlEntitiesDict.Add("lambda", "λ");
            htmlEntitiesDict.Add("mu", "μ");
            htmlEntitiesDict.Add("nu", "ν");
            htmlEntitiesDict.Add("xi", "ξ");
            htmlEntitiesDict.Add("omicron", "ο");
            htmlEntitiesDict.Add("pi", "π");
            htmlEntitiesDict.Add("rho", "ρ");
            htmlEntitiesDict.Add("sigmav", "ς");
            htmlEntitiesDict.Add("varsigma", "ς");
            htmlEntitiesDict.Add("sigmaf", "ς");
            htmlEntitiesDict.Add("sigma", "σ");
            htmlEntitiesDict.Add("tau", "τ");
            htmlEntitiesDict.Add("upsi", "υ");
            htmlEntitiesDict.Add("upsilon", "υ");
            htmlEntitiesDict.Add("phi", "φ");
            htmlEntitiesDict.Add("chi", "χ");
            htmlEntitiesDict.Add("psi", "ψ");
            htmlEntitiesDict.Add("omega", "ω");
            htmlEntitiesDict.Add("thetav", "ϑ");
            htmlEntitiesDict.Add("vartheta", "ϑ");
            htmlEntitiesDict.Add("thetasym", "ϑ");
            htmlEntitiesDict.Add("Upsi", "ϒ");
            htmlEntitiesDict.Add("upsih", "ϒ");
            htmlEntitiesDict.Add("straightphi", "ϕ");
            htmlEntitiesDict.Add("phiv", "ϕ");
            htmlEntitiesDict.Add("varphi", "ϕ");
            htmlEntitiesDict.Add("piv", "ϖ");
            htmlEntitiesDict.Add("varpi", "ϖ");
            htmlEntitiesDict.Add("Gammad", "Ϝ");
            htmlEntitiesDict.Add("gammad", "ϝ");
            htmlEntitiesDict.Add("digamma", "ϝ");
            htmlEntitiesDict.Add("kappav", "ϰ");
            htmlEntitiesDict.Add("varkappa", "ϰ");
            htmlEntitiesDict.Add("rhov", "ϱ");
            htmlEntitiesDict.Add("varrho", "ϱ");
            htmlEntitiesDict.Add("epsiv", "ϵ");
            htmlEntitiesDict.Add("varepsilon", "ϵ");
            htmlEntitiesDict.Add("straightepsilon", "ϵ");
            htmlEntitiesDict.Add("bepsi", "϶");
            htmlEntitiesDict.Add("backepsilon", "϶");
            htmlEntitiesDict.Add("IOcy", "Ё");
            htmlEntitiesDict.Add("DJcy", "Ђ");
            htmlEntitiesDict.Add("GJcy", "Ѓ");
            htmlEntitiesDict.Add("Jukcy", "Є");
            htmlEntitiesDict.Add("DScy", "Ѕ");
            htmlEntitiesDict.Add("Iukcy", "І");
            htmlEntitiesDict.Add("YIcy", "Ї");
            htmlEntitiesDict.Add("Jsercy", "Ј");
            htmlEntitiesDict.Add("LJcy", "Љ");
            htmlEntitiesDict.Add("NJcy", "Њ");
            htmlEntitiesDict.Add("TSHcy", "Ћ");
            htmlEntitiesDict.Add("KJcy", "Ќ");
            htmlEntitiesDict.Add("Ubrcy", "Ў");
            htmlEntitiesDict.Add("DZcy", "Џ");
            htmlEntitiesDict.Add("Acy", "А");
            htmlEntitiesDict.Add("Bcy", "Б");
            htmlEntitiesDict.Add("Vcy", "В");
            htmlEntitiesDict.Add("Gcy", "Г");
            htmlEntitiesDict.Add("Dcy", "Д");
            htmlEntitiesDict.Add("IEcy", "Е");
            htmlEntitiesDict.Add("ZHcy", "Ж");
            htmlEntitiesDict.Add("Zcy", "З");
            htmlEntitiesDict.Add("Icy", "И");
            htmlEntitiesDict.Add("Jcy", "Й");
            htmlEntitiesDict.Add("Kcy", "К");
            htmlEntitiesDict.Add("Lcy", "Л");
            htmlEntitiesDict.Add("Mcy", "М");
            htmlEntitiesDict.Add("Ncy", "Н");
            htmlEntitiesDict.Add("Ocy", "О");
            htmlEntitiesDict.Add("Pcy", "П");
            htmlEntitiesDict.Add("Rcy", "Р");
            htmlEntitiesDict.Add("Scy", "С");
            htmlEntitiesDict.Add("Tcy", "Т");
            htmlEntitiesDict.Add("Ucy", "У");
            htmlEntitiesDict.Add("Fcy", "Ф");
            htmlEntitiesDict.Add("KHcy", "Х");
            htmlEntitiesDict.Add("TScy", "Ц");
            htmlEntitiesDict.Add("CHcy", "Ч");
            htmlEntitiesDict.Add("cy", "Ш");
            htmlEntitiesDict.Add("CHcy", "Щ");
            htmlEntitiesDict.Add("HARDcy", "Ъ");
            htmlEntitiesDict.Add("Ycy", "Ы");
            htmlEntitiesDict.Add("SOFTcy", "Ь");
            htmlEntitiesDict.Add("Ecy", "Э");
            htmlEntitiesDict.Add("YUcy", "Ю");
            htmlEntitiesDict.Add("YAcy", "Я");
            htmlEntitiesDict.Add("acy", "а");
            htmlEntitiesDict.Add("bcy", "б");
            htmlEntitiesDict.Add("vcy", "в");
            htmlEntitiesDict.Add("gcy", "г");
            htmlEntitiesDict.Add("dcy", "д");
            htmlEntitiesDict.Add("iecy", "е");
            htmlEntitiesDict.Add("zhcy", "ж");
            htmlEntitiesDict.Add("zcy", "з");
            htmlEntitiesDict.Add("icy", "и");
            htmlEntitiesDict.Add("jcy", "й");
            htmlEntitiesDict.Add("kcy", "к");
            htmlEntitiesDict.Add("lcy", "л");
            htmlEntitiesDict.Add("mcy", "м");
            htmlEntitiesDict.Add("ncy", "н");
            htmlEntitiesDict.Add("ocy", "о");
            htmlEntitiesDict.Add("pcy", "п");
            htmlEntitiesDict.Add("rcy", "р");
            htmlEntitiesDict.Add("scy", "с");
            htmlEntitiesDict.Add("tcy", "т");
            htmlEntitiesDict.Add("ucy", "у");
            htmlEntitiesDict.Add("fcy", "ф");
            htmlEntitiesDict.Add("khcy", "х");
            htmlEntitiesDict.Add("tscy", "ц");
            htmlEntitiesDict.Add("chcy", "ч");
            htmlEntitiesDict.Add("shcy", "ш");
            htmlEntitiesDict.Add("shchcy", "щ");
            htmlEntitiesDict.Add("hardcy", "ъ");
            htmlEntitiesDict.Add("ycy", "ы");
            htmlEntitiesDict.Add("softcy", "ь");
            htmlEntitiesDict.Add("ecy", "э");
            htmlEntitiesDict.Add("yucy", "ю");
            htmlEntitiesDict.Add("yacy", "я");
            htmlEntitiesDict.Add("iocy", "ё");
            htmlEntitiesDict.Add("djcy", "ђ");
            htmlEntitiesDict.Add("gjcy", "ѓ");
            htmlEntitiesDict.Add("jukcy", "є");
            htmlEntitiesDict.Add("dscy", "ѕ");
            htmlEntitiesDict.Add("iukcy", "і");
            htmlEntitiesDict.Add("yicy", "ї");
            htmlEntitiesDict.Add("jsercy", "ј");
            htmlEntitiesDict.Add("ljcy", "љ");
            htmlEntitiesDict.Add("njcy", "њ");
            htmlEntitiesDict.Add("tshcy", "ћ");
            htmlEntitiesDict.Add("kjcy", "ќ");
            htmlEntitiesDict.Add("ubrcy", "ў");
            htmlEntitiesDict.Add("dzcy", "џ");
            htmlEntitiesDict.Add("ensp", "     ");
            htmlEntitiesDict.Add("emsp", "     ");
            htmlEntitiesDict.Add("emsp13", "     ");
            htmlEntitiesDict.Add("emsp14", "     ");
            htmlEntitiesDict.Add("numsp", "     ");
            htmlEntitiesDict.Add("puncsp", "     ");
            htmlEntitiesDict.Add("thinsp", "     ");
            htmlEntitiesDict.Add("ThinSpace", "     ");
            htmlEntitiesDict.Add("hairsp", "     ");
            htmlEntitiesDict.Add("VeryThinSpace", "     ");
            htmlEntitiesDict.Add("ZeroWidthSpace", "​");
            htmlEntitiesDict.Add("NegativeVeryThinSpace", "​");
            htmlEntitiesDict.Add("NegativeThinSpace", "​");
            htmlEntitiesDict.Add("NegativeMediumSpace", "​");
            htmlEntitiesDict.Add("NegativeThickSpace", "​");
            htmlEntitiesDict.Add("zwnj", "‌");
            htmlEntitiesDict.Add("zwj", "‍");
            htmlEntitiesDict.Add("lrm", "‎");
            htmlEntitiesDict.Add("rlm", "‏");
            htmlEntitiesDict.Add("hyphen", "‐");
            htmlEntitiesDict.Add("dash", "‐");
            htmlEntitiesDict.Add("ndash", "–");
            htmlEntitiesDict.Add("mdash", "—");
            htmlEntitiesDict.Add("horbar", "―");
            htmlEntitiesDict.Add("Verbar", "‖");
            htmlEntitiesDict.Add("Vert", "‖");
            htmlEntitiesDict.Add("lsquo", "‘");
            htmlEntitiesDict.Add("OpenCurlyQuote", "‘");
            htmlEntitiesDict.Add("rsquo", "’");
            htmlEntitiesDict.Add("rsquor", "’");
            htmlEntitiesDict.Add("CloseCurlyQuote", "’");
            htmlEntitiesDict.Add("lsquor", "‚");
            htmlEntitiesDict.Add("sbquo", "‚");
            htmlEntitiesDict.Add("ldquo", "“");
            htmlEntitiesDict.Add("OpenCurlyDoubleQuote", "“");
            htmlEntitiesDict.Add("rdquo", "”");
            htmlEntitiesDict.Add("rdquor", "”");
            htmlEntitiesDict.Add("CloseCurlyDoubleQuote", "”");
            htmlEntitiesDict.Add("ldquor", "„");
            htmlEntitiesDict.Add("bdquo", "„");
            htmlEntitiesDict.Add("dagger", "†");
            htmlEntitiesDict.Add("Dagger", "‡");
            htmlEntitiesDict.Add("ddagger", "‡");
            htmlEntitiesDict.Add("bull", "•");
            htmlEntitiesDict.Add("bullet", "•");
            htmlEntitiesDict.Add("nldr", "‥");
            htmlEntitiesDict.Add("hellip", "…");
            htmlEntitiesDict.Add("mldr", "…");
            htmlEntitiesDict.Add("permil", "‰");
            htmlEntitiesDict.Add("pertenk", "‱");
            htmlEntitiesDict.Add("prime", "′");
            htmlEntitiesDict.Add("Prime", "″");
            htmlEntitiesDict.Add("tprime", "‴");
            htmlEntitiesDict.Add("bprime", "‵");
            htmlEntitiesDict.Add("backprime", "‵");
            htmlEntitiesDict.Add("lsaquo", "‹");
            htmlEntitiesDict.Add("rsaquo", "›");
            htmlEntitiesDict.Add("oline", "‾");
            htmlEntitiesDict.Add("OverBar", "‾");
            htmlEntitiesDict.Add("caret", "⁁");
            htmlEntitiesDict.Add("hybull", "⁃");
            htmlEntitiesDict.Add("frasl", "⁄");
            htmlEntitiesDict.Add("bsemi", "⁏");
            htmlEntitiesDict.Add("qprime", "⁗");
            htmlEntitiesDict.Add("MediumSpace", "     ");
            htmlEntitiesDict.Add("NoBreak", "⁠");
            htmlEntitiesDict.Add("ApplyFunction", "⁡");
            htmlEntitiesDict.Add("af", "⁡");
            htmlEntitiesDict.Add("InvisibleTimes", "⁢");
            htmlEntitiesDict.Add("it", "⁢");
            htmlEntitiesDict.Add("InvisibleComma", "⁣");
            htmlEntitiesDict.Add("ic", "⁣");
            htmlEntitiesDict.Add("euro", "€");
            htmlEntitiesDict.Add("tdot", "⃛");
            htmlEntitiesDict.Add("TripleDot", "⃛");
            htmlEntitiesDict.Add("DotDot", "⃜");
            htmlEntitiesDict.Add("Copf", "ℂ");
            htmlEntitiesDict.Add("complexes", "ℂ");
            htmlEntitiesDict.Add("incare", "℅");
            htmlEntitiesDict.Add("gscr", "ℊ");
            htmlEntitiesDict.Add("hamilt", "ℋ");
            htmlEntitiesDict.Add("HilbertSpace", "ℋ");
            htmlEntitiesDict.Add("Hscr", "ℋ");
            htmlEntitiesDict.Add("Hfr", "ℌ");
            htmlEntitiesDict.Add("Poincareplane", "ℌ");
            htmlEntitiesDict.Add("quaternions", "ℍ");
            htmlEntitiesDict.Add("Hopf", "ℍ");
            htmlEntitiesDict.Add("planckh", "ℎ");
            htmlEntitiesDict.Add("planck", "ℏ");
            htmlEntitiesDict.Add("hbar", "ℏ");
            htmlEntitiesDict.Add("plankv", "ℏ");
            htmlEntitiesDict.Add("hslash", "ℏ");
            htmlEntitiesDict.Add("Iscr", "ℐ");
            htmlEntitiesDict.Add("imagline", "ℐ");
            htmlEntitiesDict.Add("image", "ℑ");
            htmlEntitiesDict.Add("Im", "ℑ");
            htmlEntitiesDict.Add("imagpart", "ℑ");
            htmlEntitiesDict.Add("Ifr", "ℑ");
            htmlEntitiesDict.Add("Lscr", "ℒ");
            htmlEntitiesDict.Add("lagran", "ℒ");
            htmlEntitiesDict.Add("Laplacetrf", "ℒ");
            htmlEntitiesDict.Add("ell", "ℓ");
            htmlEntitiesDict.Add("Nopf", "ℕ");
            htmlEntitiesDict.Add("naturals", "ℕ");
            htmlEntitiesDict.Add("numero", "№");
            htmlEntitiesDict.Add("copysr", "℗");
            htmlEntitiesDict.Add("weierp", "℘");
            htmlEntitiesDict.Add("wp", "℘");
            htmlEntitiesDict.Add("Popf", "ℙ");
            htmlEntitiesDict.Add("primes", "ℙ");
            htmlEntitiesDict.Add("rationals", "ℚ");
            htmlEntitiesDict.Add("Qopf", "ℚ");
            htmlEntitiesDict.Add("Rscr", "ℛ");
            htmlEntitiesDict.Add("realine", "ℛ");
            htmlEntitiesDict.Add("real", "ℜ");
            htmlEntitiesDict.Add("Re", "ℜ");
            htmlEntitiesDict.Add("realpart", "ℜ");
            htmlEntitiesDict.Add("Rfr", "ℜ");
            htmlEntitiesDict.Add("reals", "ℝ");
            htmlEntitiesDict.Add("Ropf", "ℝ");
            htmlEntitiesDict.Add("rx", "℞");
            htmlEntitiesDict.Add("trade", "™");
            htmlEntitiesDict.Add("TRADE", "™");
            htmlEntitiesDict.Add("integers", "ℤ");
            htmlEntitiesDict.Add("Zopf", "ℤ");
            htmlEntitiesDict.Add("mho", "℧");
            htmlEntitiesDict.Add("Zfr", "ℨ");
            htmlEntitiesDict.Add("zeetrf", "ℨ");
            htmlEntitiesDict.Add("iiota", "℩");
            htmlEntitiesDict.Add("bernou", "ℬ");
            htmlEntitiesDict.Add("Bernoullis", "ℬ");
            htmlEntitiesDict.Add("Bscr", "ℬ");
            htmlEntitiesDict.Add("Cfr", "ℭ");
            htmlEntitiesDict.Add("Cayleys", "ℭ");
            htmlEntitiesDict.Add("escr", "ℯ");
            htmlEntitiesDict.Add("Escr", "ℰ");
            htmlEntitiesDict.Add("expectation", "ℰ");
            htmlEntitiesDict.Add("Fscr", "ℱ");
            htmlEntitiesDict.Add("Fouriertrf", "ℱ");
            htmlEntitiesDict.Add("phmmat", "ℳ");
            htmlEntitiesDict.Add("Mellintrf", "ℳ");
            htmlEntitiesDict.Add("Mscr", "ℳ");
            htmlEntitiesDict.Add("order", "ℴ");
            htmlEntitiesDict.Add("orderof", "ℴ");
            htmlEntitiesDict.Add("oscr", "ℴ");
            htmlEntitiesDict.Add("alefsym", "ℵ");
            htmlEntitiesDict.Add("aleph", "ℵ");
            htmlEntitiesDict.Add("beth", "ℶ");
            htmlEntitiesDict.Add("gimel", "ℷ");
            htmlEntitiesDict.Add("daleth", "ℸ");
            htmlEntitiesDict.Add("CapitalDifferentialD", "ⅅ");
            htmlEntitiesDict.Add("DD", "ⅅ");
            htmlEntitiesDict.Add("DifferentialD", "ⅆ");
            htmlEntitiesDict.Add("dd", "ⅆ");
            htmlEntitiesDict.Add("ExponentialE", "ⅇ");
            htmlEntitiesDict.Add("exponentiale", "ⅇ");
            htmlEntitiesDict.Add("ee", "ⅇ");
            htmlEntitiesDict.Add("ImaginaryI", "ⅈ");
            htmlEntitiesDict.Add("ii", "ⅈ");
            htmlEntitiesDict.Add("frac13", "⅓");
            htmlEntitiesDict.Add("frac23", "⅔");
            htmlEntitiesDict.Add("frac15", "⅕");
            htmlEntitiesDict.Add("frac25", "⅖");
            htmlEntitiesDict.Add("frac35", "⅗");
            htmlEntitiesDict.Add("frac45", "⅘");
            htmlEntitiesDict.Add("frac16", "⅙");
            htmlEntitiesDict.Add("frac56", "⅚");
            htmlEntitiesDict.Add("frac18", "⅛");
            htmlEntitiesDict.Add("frac38", "⅜");
            htmlEntitiesDict.Add("frac58", "⅝");
            htmlEntitiesDict.Add("frac78", "⅞");
            htmlEntitiesDict.Add("larr", "←");
            htmlEntitiesDict.Add("leftarrow", "←");
            htmlEntitiesDict.Add("LeftArrow", "←");
            htmlEntitiesDict.Add("slarr", "←");
            htmlEntitiesDict.Add("ShortLeftArrow", "←");
            htmlEntitiesDict.Add("uarr", "↑");
            htmlEntitiesDict.Add("uparrow", "↑");
            htmlEntitiesDict.Add("UpArrow", "↑");
            htmlEntitiesDict.Add("ShortUpArrow", "↑");
            htmlEntitiesDict.Add("rarr", "→");
            htmlEntitiesDict.Add("rightarrow", "→");
            htmlEntitiesDict.Add("RightArrow", "→");
            htmlEntitiesDict.Add("srarr", "→");
            htmlEntitiesDict.Add("ShortRightArrow", "→");
            htmlEntitiesDict.Add("darr", "↓");
            htmlEntitiesDict.Add("downarrow", "↓");
            htmlEntitiesDict.Add("DownArrow", "↓");
            htmlEntitiesDict.Add("ShortDownArrow", "↓");
            htmlEntitiesDict.Add("harr", "↔");
            htmlEntitiesDict.Add("leftrightarrow", "↔");
            htmlEntitiesDict.Add("LeftRightArrow", "↔");
            htmlEntitiesDict.Add("varr", "↕");
            htmlEntitiesDict.Add("updownarrow", "↕");
            htmlEntitiesDict.Add("UpDownArrow", "↕");
            htmlEntitiesDict.Add("nwarr", "↖");
            htmlEntitiesDict.Add("UpperLeftArrow", "↖");
            htmlEntitiesDict.Add("nwarrow", "↖");
            htmlEntitiesDict.Add("nearr", "↗");
            htmlEntitiesDict.Add("UpperRightArrow", "↗");
            htmlEntitiesDict.Add("nearrow", "↗");
            htmlEntitiesDict.Add("searr", "↘");
            htmlEntitiesDict.Add("searrow", "↘");
            htmlEntitiesDict.Add("LowerRightArrow", "↘");
            htmlEntitiesDict.Add("swarr", "↙");
            htmlEntitiesDict.Add("swarrow", "↙");
            htmlEntitiesDict.Add("LowerLeftArrow", "↙");
            htmlEntitiesDict.Add("nlarr", "↚");
            htmlEntitiesDict.Add("nleftarrow", "↚");
            htmlEntitiesDict.Add("nrarr", "↛");
            htmlEntitiesDict.Add("nrightarrow", "↛");
            htmlEntitiesDict.Add("rarrw", "↝");
            htmlEntitiesDict.Add("rightsquigarrow", "↝");
            htmlEntitiesDict.Add("Larr", "↞");
            htmlEntitiesDict.Add("twoheadleftarrow", "↞");
            htmlEntitiesDict.Add("Uarr", "↟");
            htmlEntitiesDict.Add("Rarr", "↠");
            htmlEntitiesDict.Add("twoheadrightarrow", "↠");
            htmlEntitiesDict.Add("Darr", "↡");
            htmlEntitiesDict.Add("larrtl", "↢");
            htmlEntitiesDict.Add("leftarrowtail", "↢");
            htmlEntitiesDict.Add("rarrtl", "↣");
            htmlEntitiesDict.Add("rightarrowtail", "↣");
            htmlEntitiesDict.Add("LeftTeeArrow", "↤");
            htmlEntitiesDict.Add("mapstoleft", "↤");
            htmlEntitiesDict.Add("UpTeeArrow", "↥");
            htmlEntitiesDict.Add("mapstoup", "↥");
            htmlEntitiesDict.Add("map", "↦");
            htmlEntitiesDict.Add("RightTeeArrow", "↦");
            htmlEntitiesDict.Add("mapsto", "↦");
            htmlEntitiesDict.Add("DownTeeArrow", "↧");
            htmlEntitiesDict.Add("mapstodown", "↧");
            htmlEntitiesDict.Add("larrhk", "↩");
            htmlEntitiesDict.Add("hookleftarrow", "↩");
            htmlEntitiesDict.Add("rarrhk", "↪");
            htmlEntitiesDict.Add("hookrightarrow", "↪");
            htmlEntitiesDict.Add("larrlp", "↫");
            htmlEntitiesDict.Add("looparrowleft", "↫");
            htmlEntitiesDict.Add("rarrlp", "↬");
            htmlEntitiesDict.Add("looparrowright", "↬");
            htmlEntitiesDict.Add("harrw", "↭");
            htmlEntitiesDict.Add("leftrightsquigarrow", "↭");
            htmlEntitiesDict.Add("nharr", "↮");
            htmlEntitiesDict.Add("nleftrightarrow", "↮");
            htmlEntitiesDict.Add("lsh", "↰");
            htmlEntitiesDict.Add("Lsh", "↰");
            htmlEntitiesDict.Add("rsh", "↱");
            htmlEntitiesDict.Add("Rsh", "↱");
            htmlEntitiesDict.Add("ldsh", "↲");
            htmlEntitiesDict.Add("rdsh", "↳");
            htmlEntitiesDict.Add("crarr", "↵");
            htmlEntitiesDict.Add("cularr", "↶");
            htmlEntitiesDict.Add("curvearrowleft", "↶");
            htmlEntitiesDict.Add("curarr", "↷");
            htmlEntitiesDict.Add("curvearrowright", "↷");
            htmlEntitiesDict.Add("olarr", "↺");
            htmlEntitiesDict.Add("circlearrowleft", "↺");
            htmlEntitiesDict.Add("orarr", "↻");
            htmlEntitiesDict.Add("circlearrowright", "↻");
            htmlEntitiesDict.Add("lharu", "↼");
            htmlEntitiesDict.Add("LeftVector", "↼");
            htmlEntitiesDict.Add("leftharpoonup", "↼");
            htmlEntitiesDict.Add("lhard", "↽");
            htmlEntitiesDict.Add("leftharpoondown", "↽");
            htmlEntitiesDict.Add("DownLeftVector", "↽");
            htmlEntitiesDict.Add("uharr", "↾");
            htmlEntitiesDict.Add("upharpoonright", "↾");
            htmlEntitiesDict.Add("RightUpVector", "↾");
            htmlEntitiesDict.Add("uharl", "↿");
            htmlEntitiesDict.Add("upharpoonleft", "↿");
            htmlEntitiesDict.Add("LeftUpVector", "↿");
            htmlEntitiesDict.Add("rharu", "⇀");
            htmlEntitiesDict.Add("RightVector", "⇀");
            htmlEntitiesDict.Add("rightharpoonup", "⇀");
            htmlEntitiesDict.Add("rhard", "⇁");
            htmlEntitiesDict.Add("rightharpoondown", "⇁");
            htmlEntitiesDict.Add("DownRightVector", "⇁");
            htmlEntitiesDict.Add("dharr", "⇂");
            htmlEntitiesDict.Add("RightDownVector", "⇂");
            htmlEntitiesDict.Add("downharpoonright", "⇂");
            htmlEntitiesDict.Add("dharl", "⇃");
            htmlEntitiesDict.Add("LeftDownVector", "⇃");
            htmlEntitiesDict.Add("downharpoonleft", "⇃");
            htmlEntitiesDict.Add("rlarr", "⇄");
            htmlEntitiesDict.Add("rightleftarrows", "⇄");
            htmlEntitiesDict.Add("RightArrowLeftArrow", "⇄");
            htmlEntitiesDict.Add("udarr", "⇅");
            htmlEntitiesDict.Add("UpArrowDownArrow", "⇅");
            htmlEntitiesDict.Add("lrarr", "⇆");
            htmlEntitiesDict.Add("leftrightarrows", "⇆");
            htmlEntitiesDict.Add("LeftArrowRightArrow", "⇆");
            htmlEntitiesDict.Add("llarr", "⇇");
            htmlEntitiesDict.Add("leftleftarrows", "⇇");
            htmlEntitiesDict.Add("uuarr", "⇈");
            htmlEntitiesDict.Add("upuparrows", "⇈");
            htmlEntitiesDict.Add("rrarr", "⇉");
            htmlEntitiesDict.Add("rightrightarrows", "⇉");
            htmlEntitiesDict.Add("ddarr", "⇊");
            htmlEntitiesDict.Add("downdownarrows", "⇊");
            htmlEntitiesDict.Add("lrhar", "⇋");
            htmlEntitiesDict.Add("ReverseEquilibrium", "⇋");
            htmlEntitiesDict.Add("leftrightharpoons", "⇋");
            htmlEntitiesDict.Add("rlhar", "⇌");
            htmlEntitiesDict.Add("rightleftharpoons", "⇌");
            htmlEntitiesDict.Add("Equilibrium", "⇌");
            htmlEntitiesDict.Add("nlArr", "⇍");
            htmlEntitiesDict.Add("nLeftarrow", "⇍");
            htmlEntitiesDict.Add("nhArr", "⇎");
            htmlEntitiesDict.Add("nLeftrightarrow", "⇎");
            htmlEntitiesDict.Add("nrArr", "⇏");
            htmlEntitiesDict.Add("nRightarrow", "⇏");
            htmlEntitiesDict.Add("lArr", "⇐");
            htmlEntitiesDict.Add("Leftarrow", "⇐");
            htmlEntitiesDict.Add("DoubleLeftArrow", "⇐");
            htmlEntitiesDict.Add("uArr", "⇑");
            htmlEntitiesDict.Add("Uparrow", "⇑");
            htmlEntitiesDict.Add("DoubleUpArrow", "⇑");
            htmlEntitiesDict.Add("rArr", "⇒");
            htmlEntitiesDict.Add("Rightarrow", "⇒");
            htmlEntitiesDict.Add("Implies", "⇒");
            htmlEntitiesDict.Add("DoubleRightArrow", "⇒");
            htmlEntitiesDict.Add("dArr", "⇓");
            htmlEntitiesDict.Add("Downarrow", "⇓");
            htmlEntitiesDict.Add("DoubleDownArrow", "⇓");
            htmlEntitiesDict.Add("hArr", "⇔");
            htmlEntitiesDict.Add("Leftrightarrow", "⇔");
            htmlEntitiesDict.Add("DoubleLeftRightArrow", "⇔");
            htmlEntitiesDict.Add("iff", "⇔");
            htmlEntitiesDict.Add("vArr", "⇕");
            htmlEntitiesDict.Add("Updownarrow", "⇕");
            htmlEntitiesDict.Add("DoubleUpDownArrow", "⇕");
            htmlEntitiesDict.Add("nwArr", "⇖");
            htmlEntitiesDict.Add("neArr", "⇗");
            htmlEntitiesDict.Add("seArr", "⇘");
            htmlEntitiesDict.Add("swArr", "⇙");
            htmlEntitiesDict.Add("lAarr", "⇚");
            htmlEntitiesDict.Add("Lleftarrow", "⇚");
            htmlEntitiesDict.Add("rAarr", "⇛");
            htmlEntitiesDict.Add("Rrightarrow", "⇛");
            htmlEntitiesDict.Add("zigrarr", "⇝");
            htmlEntitiesDict.Add("larrb", "⇤");
            htmlEntitiesDict.Add("LeftArrowBar", "⇤");
            htmlEntitiesDict.Add("rarrb", "⇥");
            htmlEntitiesDict.Add("RightArrowBar", "⇥");
            htmlEntitiesDict.Add("duarr", "⇵");
            htmlEntitiesDict.Add("DownArrowUpArrow", "⇵");
            htmlEntitiesDict.Add("loarr", "⇽");
            htmlEntitiesDict.Add("roarr", "⇾");
            htmlEntitiesDict.Add("hoarr", "⇿");
            htmlEntitiesDict.Add("forall", "∀");
            htmlEntitiesDict.Add("ForAll", "∀");
            htmlEntitiesDict.Add("comp", "∁");
            htmlEntitiesDict.Add("complement", "∁");
            htmlEntitiesDict.Add("part", "∂");
            htmlEntitiesDict.Add("PartialD", "∂");
            htmlEntitiesDict.Add("exist", "∃");
            htmlEntitiesDict.Add("Exists", "∃");
            htmlEntitiesDict.Add("nexist", "∄");
            htmlEntitiesDict.Add("NotExists", "∄");
            htmlEntitiesDict.Add("nexists", "∄");
            htmlEntitiesDict.Add("empty", "∅");
            htmlEntitiesDict.Add("emptyset", "∅");
            htmlEntitiesDict.Add("emptyv", "∅");
            htmlEntitiesDict.Add("varnothing", "∅");
            htmlEntitiesDict.Add("nabla", "∇");
            htmlEntitiesDict.Add("Del", "∇");
            htmlEntitiesDict.Add("isin", "∈");
            htmlEntitiesDict.Add("isinv", "∈");
            htmlEntitiesDict.Add("Element", "∈");
            htmlEntitiesDict.Add("in", "∈");
            htmlEntitiesDict.Add("notin", "∉");
            htmlEntitiesDict.Add("NotElement", "∉");
            htmlEntitiesDict.Add("notinva", "∉");
            htmlEntitiesDict.Add("niv", "∋");
            htmlEntitiesDict.Add("ReverseElement", "∋");
            htmlEntitiesDict.Add("ni", "∋");
            htmlEntitiesDict.Add("SuchThat", "∋");
            htmlEntitiesDict.Add("notni", "∌");
            htmlEntitiesDict.Add("notniva", "∌");
            htmlEntitiesDict.Add("NotReverseElement", "∌");
            htmlEntitiesDict.Add("prod", "∏");
            htmlEntitiesDict.Add("Product", "∏");
            htmlEntitiesDict.Add("coprod", "∐");
            htmlEntitiesDict.Add("Coproduct", "∐");
            htmlEntitiesDict.Add("sum", "∑");
            htmlEntitiesDict.Add("Sum", "∑");
            htmlEntitiesDict.Add("minus", "−");
            htmlEntitiesDict.Add("mnplus", "∓");
            htmlEntitiesDict.Add("mp", "∓");
            htmlEntitiesDict.Add("MinusPlus", "∓");
            htmlEntitiesDict.Add("plusdo", "∔");
            htmlEntitiesDict.Add("dotplus", "∔");
            htmlEntitiesDict.Add("setmn", "∖");
            htmlEntitiesDict.Add("setminus", "∖");
            htmlEntitiesDict.Add("Backslash", "∖");
            htmlEntitiesDict.Add("ssetmn", "∖");
            htmlEntitiesDict.Add("smallsetminus", "∖");
            htmlEntitiesDict.Add("lowast", "∗");
            htmlEntitiesDict.Add("compfn", "∘");
            htmlEntitiesDict.Add("SmallCircle", "∘");
            htmlEntitiesDict.Add("radic", "√");
            htmlEntitiesDict.Add("Sqrt", "√");
            htmlEntitiesDict.Add("prop", "∝");
            htmlEntitiesDict.Add("propto", "∝");
            htmlEntitiesDict.Add("Proportional", "∝");
            htmlEntitiesDict.Add("vprop", "∝");
            htmlEntitiesDict.Add("varpropto", "∝");
            htmlEntitiesDict.Add("infin", "∞");
            htmlEntitiesDict.Add("angrt", "∟");
            htmlEntitiesDict.Add("ang", "∠");
            htmlEntitiesDict.Add("angle", "∠");
            htmlEntitiesDict.Add("angmsd", "∡");
            htmlEntitiesDict.Add("measuredangle", "∡");
            htmlEntitiesDict.Add("angsph", "∢");
            htmlEntitiesDict.Add("mid", "∣");
            htmlEntitiesDict.Add("VerticalBar", "∣");
            htmlEntitiesDict.Add("smid", "∣");
            htmlEntitiesDict.Add("shortmid", "∣");
            htmlEntitiesDict.Add("nmid", "∤");
            htmlEntitiesDict.Add("NotVerticalBar", "∤");
            htmlEntitiesDict.Add("nsmid", "∤");
            htmlEntitiesDict.Add("nshortmid", "∤");
            htmlEntitiesDict.Add("par", "∥");
            htmlEntitiesDict.Add("parallel", "∥");
            htmlEntitiesDict.Add("DoubleVerticalBar", "∥");
            htmlEntitiesDict.Add("spar", "∥");
            htmlEntitiesDict.Add("shortparallel", "∥");
            htmlEntitiesDict.Add("npar", "∦");
            htmlEntitiesDict.Add("nparallel", "∦");
            htmlEntitiesDict.Add("NotDoubleVerticalBar", "∦");
            htmlEntitiesDict.Add("nspar", "∦");
            htmlEntitiesDict.Add("nshortparallel", "∦");
            htmlEntitiesDict.Add("and", "∧");
            htmlEntitiesDict.Add("wedge", "∧");
            htmlEntitiesDict.Add("or", "∨");
            htmlEntitiesDict.Add("vee", "∨");
            htmlEntitiesDict.Add("cap", "∩");
            htmlEntitiesDict.Add("cup", "∪");
            htmlEntitiesDict.Add("int", "∫");
            htmlEntitiesDict.Add("Integral", "∫");
            htmlEntitiesDict.Add("Int", "∬");
            htmlEntitiesDict.Add("tint", "∭");
            htmlEntitiesDict.Add("iiint", "∭");
            htmlEntitiesDict.Add("conint", "∮");
            htmlEntitiesDict.Add("oint", "∮");
            htmlEntitiesDict.Add("ContourIntegral", "∮");
            htmlEntitiesDict.Add("Conint", "∯");
            htmlEntitiesDict.Add("DoubleContourIntegral", "∯");
            htmlEntitiesDict.Add("Cconint", "∰");
            htmlEntitiesDict.Add("cwint", "∱");
            htmlEntitiesDict.Add("cwconint", "∲");
            htmlEntitiesDict.Add("ClockwiseContourIntegral", "∲");
            htmlEntitiesDict.Add("awconint", "∳");
            htmlEntitiesDict.Add("CounterClockwiseContourIntegral", "∳");
            htmlEntitiesDict.Add("there4", "∴");
            htmlEntitiesDict.Add("therefore", "∴");
            htmlEntitiesDict.Add("Therefore", "∴");
            htmlEntitiesDict.Add("becaus", "∵");
            htmlEntitiesDict.Add("because", "∵");
            htmlEntitiesDict.Add("Because", "∵");
            htmlEntitiesDict.Add("ratio", "∶");
            htmlEntitiesDict.Add("Colon", "∷");
            htmlEntitiesDict.Add("Proportion", "∷");
            htmlEntitiesDict.Add("minusd", "∸");
            htmlEntitiesDict.Add("dotminus", "∸");
            htmlEntitiesDict.Add("mDDot", "∺");
            htmlEntitiesDict.Add("homtht", "∻");
            htmlEntitiesDict.Add("sim", "∼");
            htmlEntitiesDict.Add("Tilde", "∼");
            htmlEntitiesDict.Add("thksim", "∼");
            htmlEntitiesDict.Add("thicksim", "∼");
            htmlEntitiesDict.Add("bsim", "∽");
            htmlEntitiesDict.Add("backsim", "∽");
            htmlEntitiesDict.Add("ac", "∾");
            htmlEntitiesDict.Add("mstpos", "∾");
            htmlEntitiesDict.Add("acd", "∿");
            htmlEntitiesDict.Add("wreath", "≀");
            htmlEntitiesDict.Add("VerticalTilde", "≀");
            htmlEntitiesDict.Add("wr", "≀");
            htmlEntitiesDict.Add("nsim", "≁");
            htmlEntitiesDict.Add("NotTilde", "≁");
            htmlEntitiesDict.Add("esim", "≂");
            htmlEntitiesDict.Add("EqualTilde", "≂");
            htmlEntitiesDict.Add("eqsim", "≂");
            htmlEntitiesDict.Add("sime", "≃");
            htmlEntitiesDict.Add("TildeEqual", "≃");
            htmlEntitiesDict.Add("simeq", "≃");
            htmlEntitiesDict.Add("nsime", "≄");
            htmlEntitiesDict.Add("nsimeq", "≄");
            htmlEntitiesDict.Add("NotTildeEqual", "≄");
            htmlEntitiesDict.Add("cong", "≅");
            htmlEntitiesDict.Add("TildeFullEqual", "≅");
            htmlEntitiesDict.Add("simne", "≆");
            htmlEntitiesDict.Add("ncong", "≇");
            htmlEntitiesDict.Add("NotTildeFullEqual", "≇");
            htmlEntitiesDict.Add("asymp", "≈");
            htmlEntitiesDict.Add("ap", "≈");
            htmlEntitiesDict.Add("TildeTilde", "≈");
            htmlEntitiesDict.Add("approx", "≈");
            htmlEntitiesDict.Add("thkap", "≈");
            htmlEntitiesDict.Add("thickapprox", "≈");
            htmlEntitiesDict.Add("nap", "≉");
            htmlEntitiesDict.Add("NotTildeTilde", "≉");
            htmlEntitiesDict.Add("napprox", "≉");
            htmlEntitiesDict.Add("ape", "≊");
            htmlEntitiesDict.Add("approxeq", "≊");
            htmlEntitiesDict.Add("apid", "≋");
            htmlEntitiesDict.Add("bcong", "≌");
            htmlEntitiesDict.Add("backcong", "≌");
            htmlEntitiesDict.Add("asympeq", "≍");
            htmlEntitiesDict.Add("CupCap", "≍");
            htmlEntitiesDict.Add("bump", "≎");
            htmlEntitiesDict.Add("HumpDownHump", "≎");
            htmlEntitiesDict.Add("Bumpeq", "≎");
            htmlEntitiesDict.Add("bumpe", "≏");
            htmlEntitiesDict.Add("HumpEqual", "≏");
            htmlEntitiesDict.Add("bumpeq", "≏");
            htmlEntitiesDict.Add("esdot", "≐");
            htmlEntitiesDict.Add("DotEqual", "≐");
            htmlEntitiesDict.Add("doteq", "≐");
            htmlEntitiesDict.Add("eDot", "≑");
            htmlEntitiesDict.Add("doteqdot", "≑");
            htmlEntitiesDict.Add("efDot", "≒");
            htmlEntitiesDict.Add("fallingdotseq", "≒");
            htmlEntitiesDict.Add("erDot", "≓");
            htmlEntitiesDict.Add("risingdotseq", "≓");
            htmlEntitiesDict.Add("colone", "≔");
            htmlEntitiesDict.Add("coloneq", "≔");
            htmlEntitiesDict.Add("Assign", "≔");
            htmlEntitiesDict.Add("ecolon", "≕");
            htmlEntitiesDict.Add("eqcolon", "≕");
            htmlEntitiesDict.Add("ecir", "≖");
            htmlEntitiesDict.Add("eqcirc", "≖");
            htmlEntitiesDict.Add("cire", "≗");
            htmlEntitiesDict.Add("circeq", "≗");
            htmlEntitiesDict.Add("wedgeq", "≙");
            htmlEntitiesDict.Add("veeeq", "≚");
            htmlEntitiesDict.Add("trie", "≜");
            htmlEntitiesDict.Add("triangleq", "≜");
            htmlEntitiesDict.Add("equest", "≟");
            htmlEntitiesDict.Add("questeq", "≟");
            htmlEntitiesDict.Add("ne", "≠");
            htmlEntitiesDict.Add("NotEqual", "≠");
            htmlEntitiesDict.Add("equiv", "≡");
            htmlEntitiesDict.Add("Congruent", "≡");
            htmlEntitiesDict.Add("nequiv", "≢");
            htmlEntitiesDict.Add("NotCongruent", "≢");
            htmlEntitiesDict.Add("le", "≤");
            htmlEntitiesDict.Add("leq", "≤");
            htmlEntitiesDict.Add("ge", "≥");
            htmlEntitiesDict.Add("GreaterEqual", "≥");
            htmlEntitiesDict.Add("geq", "≥");
            htmlEntitiesDict.Add("lE", "≦");
            htmlEntitiesDict.Add("LessFullEqual", "≦");
            htmlEntitiesDict.Add("leqq", "≦");
            htmlEntitiesDict.Add("gE", "≧");
            htmlEntitiesDict.Add("GreaterFullEqual", "≧");
            htmlEntitiesDict.Add("geqq", "≧");
            htmlEntitiesDict.Add("lnE", "≨");
            htmlEntitiesDict.Add("lneqq", "≨");
            htmlEntitiesDict.Add("gnE", "≩");
            htmlEntitiesDict.Add("gneqq", "≩");
            htmlEntitiesDict.Add("Lt", "≪");
            htmlEntitiesDict.Add("NestedLessLess", "≪");
            htmlEntitiesDict.Add("ll", "≪");
            htmlEntitiesDict.Add("Gt", "≫");
            htmlEntitiesDict.Add("NestedGreaterGreater", "≫");
            htmlEntitiesDict.Add("gg", "≫");
            htmlEntitiesDict.Add("twixt", "≬");
            htmlEntitiesDict.Add("between", "≬");
            htmlEntitiesDict.Add("NotCupCap", "≭");
            htmlEntitiesDict.Add("nlt", "≮");
            htmlEntitiesDict.Add("NotLess", "≮");
            htmlEntitiesDict.Add("nless", "≮");
            htmlEntitiesDict.Add("ngt", "≯");
            htmlEntitiesDict.Add("NotGreater", "≯");
            htmlEntitiesDict.Add("ngtr", "≯");
            htmlEntitiesDict.Add("nle", "≰");
            htmlEntitiesDict.Add("NotLessEqual", "≰");
            htmlEntitiesDict.Add("nleq", "≰");
            htmlEntitiesDict.Add("nge", "≱");
            htmlEntitiesDict.Add("NotGreaterEqual", "≱");
            htmlEntitiesDict.Add("ngeq", "≱");
            htmlEntitiesDict.Add("lsim", "≲");
            htmlEntitiesDict.Add("LessTilde", "≲");
            htmlEntitiesDict.Add("lesssim", "≲");
            htmlEntitiesDict.Add("gsim", "≳");
            htmlEntitiesDict.Add("gtrsim", "≳");
            htmlEntitiesDict.Add("GreaterTilde", "≳");
            htmlEntitiesDict.Add("nlsim", "≴");
            htmlEntitiesDict.Add("NotLessTilde", "≴");
            htmlEntitiesDict.Add("ngsim", "≵");
            htmlEntitiesDict.Add("NotGreaterTilde", "≵");
            htmlEntitiesDict.Add("lg", "≶");
            htmlEntitiesDict.Add("lessgtr", "≶");
            htmlEntitiesDict.Add("LessGreater", "≶");
            htmlEntitiesDict.Add("gl", "≷");
            htmlEntitiesDict.Add("gtrless", "≷");
            htmlEntitiesDict.Add("GreaterLess", "≷");
            htmlEntitiesDict.Add("ntlg", "≸");
            htmlEntitiesDict.Add("NotLessGreater", "≸");
            htmlEntitiesDict.Add("ntgl", "≹");
            htmlEntitiesDict.Add("NotGreaterLess", "≹");
            htmlEntitiesDict.Add("pr", "≺");
            htmlEntitiesDict.Add("Precedes", "≺");
            htmlEntitiesDict.Add("prec", "≺");
            htmlEntitiesDict.Add("sc", "≻");
            htmlEntitiesDict.Add("Succeeds", "≻");
            htmlEntitiesDict.Add("succ", "≻");
            htmlEntitiesDict.Add("prcue", "≼");
            htmlEntitiesDict.Add("PrecedesSlantEqual", "≼");
            htmlEntitiesDict.Add("preccurlyeq", "≼");
            htmlEntitiesDict.Add("sccue", "≽");
            htmlEntitiesDict.Add("SucceedsSlantEqual", "≽");
            htmlEntitiesDict.Add("succcurlyeq", "≽");
            htmlEntitiesDict.Add("prsim", "≾");
            htmlEntitiesDict.Add("precsim", "≾");
            htmlEntitiesDict.Add("PrecedesTilde", "≾");
            htmlEntitiesDict.Add("scsim", "≿");
            htmlEntitiesDict.Add("succsim", "≿");
            htmlEntitiesDict.Add("SucceedsTilde", "≿");
            htmlEntitiesDict.Add("npr", "⊀");
            htmlEntitiesDict.Add("nprec", "⊀");
            htmlEntitiesDict.Add("NotPrecedes", "⊀");
            htmlEntitiesDict.Add("nsc", "⊁");
            htmlEntitiesDict.Add("nsucc", "⊁");
            htmlEntitiesDict.Add("NotSucceeds", "⊁");
            htmlEntitiesDict.Add("sub", "⊂");
            htmlEntitiesDict.Add("subset", "⊂");
            htmlEntitiesDict.Add("sup", "⊃");
            htmlEntitiesDict.Add("supset", "⊃");
            htmlEntitiesDict.Add("Superset", "⊃");
            htmlEntitiesDict.Add("nsub", "⊄");
            htmlEntitiesDict.Add("nsup", "⊅");
            htmlEntitiesDict.Add("sube", "⊆");
            htmlEntitiesDict.Add("SubsetEqual", "⊆");
            htmlEntitiesDict.Add("subseteq", "⊆");
            htmlEntitiesDict.Add("supe", "⊇");
            htmlEntitiesDict.Add("supseteq", "⊇");
            htmlEntitiesDict.Add("SupersetEqual", "⊇");
            htmlEntitiesDict.Add("nsube", "⊈");
            htmlEntitiesDict.Add("nsubseteq", "⊈");
            htmlEntitiesDict.Add("NotSubsetEqual", "⊈");
            htmlEntitiesDict.Add("nsupe", "⊉");
            htmlEntitiesDict.Add("nsupseteq", "⊉");
            htmlEntitiesDict.Add("NotSupersetEqual", "⊉");
            htmlEntitiesDict.Add("subne", "⊊");
            htmlEntitiesDict.Add("subsetneq", "⊊");
            htmlEntitiesDict.Add("supne", "⊋");
            htmlEntitiesDict.Add("supsetneq", "⊋");
            htmlEntitiesDict.Add("cupdot", "⊍");
            htmlEntitiesDict.Add("uplus", "⊎");
            htmlEntitiesDict.Add("UnionPlus", "⊎");
            htmlEntitiesDict.Add("sqsub", "⊏");
            htmlEntitiesDict.Add("SquareSubset", "⊏");
            htmlEntitiesDict.Add("sqsubset", "⊏");
            htmlEntitiesDict.Add("sqsup", "⊐");
            htmlEntitiesDict.Add("SquareSuperset", "⊐");
            htmlEntitiesDict.Add("sqsupset", "⊐");
            htmlEntitiesDict.Add("sqsube", "⊑");
            htmlEntitiesDict.Add("SquareSubsetEqual", "⊑");
            htmlEntitiesDict.Add("sqsubseteq", "⊑");
            htmlEntitiesDict.Add("sqsupe", "⊒");
            htmlEntitiesDict.Add("SquareSupersetEqual", "⊒");
            htmlEntitiesDict.Add("sqsupseteq", "⊒");
            htmlEntitiesDict.Add("sqcap", "⊓");
            htmlEntitiesDict.Add("SquareIntersection", "⊓");
            htmlEntitiesDict.Add("sqcup", "⊔");
            htmlEntitiesDict.Add("SquareUnion", "⊔");
            htmlEntitiesDict.Add("oplus", "⊕");
            htmlEntitiesDict.Add("CirclePlus", "⊕");
            htmlEntitiesDict.Add("ominus", "⊖");
            htmlEntitiesDict.Add("CircleMinus", "⊖");
            htmlEntitiesDict.Add("otimes", "⊗");
            htmlEntitiesDict.Add("CircleTimes", "⊗");
            htmlEntitiesDict.Add("osol", "⊘");
            htmlEntitiesDict.Add("odot", "⊙");
            htmlEntitiesDict.Add("CircleDot", "⊙");
            htmlEntitiesDict.Add("ocir", "⊚");
            htmlEntitiesDict.Add("circledcirc", "⊚");
            htmlEntitiesDict.Add("oast", "⊛");
            htmlEntitiesDict.Add("circledast", "⊛");
            htmlEntitiesDict.Add("odash", "⊝");
            htmlEntitiesDict.Add("circleddash", "⊝");
            htmlEntitiesDict.Add("plusb", "⊞");
            htmlEntitiesDict.Add("boxplus", "⊞");
            htmlEntitiesDict.Add("minusb", "⊟");
            htmlEntitiesDict.Add("boxminus", "⊟");
            htmlEntitiesDict.Add("timesb", "⊠");
            htmlEntitiesDict.Add("boxtimes", "⊠");
            htmlEntitiesDict.Add("sdotb", "⊡");
            htmlEntitiesDict.Add("dotsquare", "⊡");
            htmlEntitiesDict.Add("vdash", "⊢");
            htmlEntitiesDict.Add("RightTee", "⊢");
            htmlEntitiesDict.Add("dashv", "⊣");
            htmlEntitiesDict.Add("LeftTee", "⊣");
            htmlEntitiesDict.Add("top", "⊤");
            htmlEntitiesDict.Add("DownTee", "⊤");
            htmlEntitiesDict.Add("bottom", "⊥");
            htmlEntitiesDict.Add("bot", "⊥");
            htmlEntitiesDict.Add("perp", "⊥");
            htmlEntitiesDict.Add("UpTee", "⊥");
            htmlEntitiesDict.Add("models", "⊧");
            htmlEntitiesDict.Add("vDash", "⊨");
            htmlEntitiesDict.Add("DoubleRightTee", "⊨");
            htmlEntitiesDict.Add("Vdash", "⊩");
            htmlEntitiesDict.Add("Vvdash", "⊪");
            htmlEntitiesDict.Add("VDash", "⊫");
            htmlEntitiesDict.Add("nvdash", "⊬");
            htmlEntitiesDict.Add("nvDash", "⊭");
            htmlEntitiesDict.Add("nVdash", "⊮");
            htmlEntitiesDict.Add("nVDash", "⊯");
            htmlEntitiesDict.Add("prurel", "⊰");
            htmlEntitiesDict.Add("vltri", "⊲");
            htmlEntitiesDict.Add("vartriangleleft", "⊲");
            htmlEntitiesDict.Add("LeftTriangle", "⊲");
            htmlEntitiesDict.Add("vrtri", "⊳");
            htmlEntitiesDict.Add("vartriangleright", "⊳");
            htmlEntitiesDict.Add("RightTriangle", "⊳");
            htmlEntitiesDict.Add("ltrie", "⊴");
            htmlEntitiesDict.Add("trianglelefteq", "⊴");
            htmlEntitiesDict.Add("LeftTriangleEqual", "⊴");
            htmlEntitiesDict.Add("rtrie", "⊵");
            htmlEntitiesDict.Add("trianglerighteq", "⊵");
            htmlEntitiesDict.Add("RightTriangleEqual", "⊵");
            htmlEntitiesDict.Add("origof", "⊶");
            htmlEntitiesDict.Add("imof", "⊷");
            htmlEntitiesDict.Add("mumap", "⊸");
            htmlEntitiesDict.Add("multimap", "⊸");
            htmlEntitiesDict.Add("hercon", "⊹");
            htmlEntitiesDict.Add("intcal", "⊺");
            htmlEntitiesDict.Add("intercal", "⊺");
            htmlEntitiesDict.Add("veebar", "⊻");
            htmlEntitiesDict.Add("barvee", "⊽");
            htmlEntitiesDict.Add("angrtvb", "⊾");
            htmlEntitiesDict.Add("lrtri", "⊿");
            htmlEntitiesDict.Add("xwedge", "⋀");
            htmlEntitiesDict.Add("Wedge", "⋀");
            htmlEntitiesDict.Add("bigwedge", "⋀");
            htmlEntitiesDict.Add("xvee", "⋁");
            htmlEntitiesDict.Add("Vee", "⋁");
            htmlEntitiesDict.Add("bigvee", "⋁");
            htmlEntitiesDict.Add("xcap", "⋂");
            htmlEntitiesDict.Add("Intersection", "⋂");
            htmlEntitiesDict.Add("bigcap", "⋂");
            htmlEntitiesDict.Add("xcup", "⋃");
            htmlEntitiesDict.Add("Union", "⋃");
            htmlEntitiesDict.Add("bigcup", "⋃");
            htmlEntitiesDict.Add("diam", "⋄");
            htmlEntitiesDict.Add("diamond", "⋄");
            htmlEntitiesDict.Add("Diamond", "⋄");
            htmlEntitiesDict.Add("sdot", "⋅");
            htmlEntitiesDict.Add("sstarf", "⋆");
            htmlEntitiesDict.Add("Star", "⋆");
            htmlEntitiesDict.Add("divonx", "⋇");
            htmlEntitiesDict.Add("divideontimes", "⋇");
            htmlEntitiesDict.Add("bowtie", "⋈");
            htmlEntitiesDict.Add("ltimes", "⋉");
            htmlEntitiesDict.Add("rtimes", "⋊");
            htmlEntitiesDict.Add("lthree", "⋋");
            htmlEntitiesDict.Add("leftthreetimes", "⋋");
            htmlEntitiesDict.Add("rthree", "⋌");
            htmlEntitiesDict.Add("rightthreetimes", "⋌");
            htmlEntitiesDict.Add("bsime", "⋍");
            htmlEntitiesDict.Add("backsimeq", "⋍");
            htmlEntitiesDict.Add("cuvee", "⋎");
            htmlEntitiesDict.Add("curlyvee", "⋎");
            htmlEntitiesDict.Add("cuwed", "⋏");
            htmlEntitiesDict.Add("curlywedge", "⋏");
            htmlEntitiesDict.Add("Sub", "⋐");
            htmlEntitiesDict.Add("Subset", "⋐");
            htmlEntitiesDict.Add("Sup", "⋑");
            htmlEntitiesDict.Add("Supset", "⋑");
            htmlEntitiesDict.Add("Cap", "⋒");
            htmlEntitiesDict.Add("Cup", "⋓");
            htmlEntitiesDict.Add("fork", "⋔");
            htmlEntitiesDict.Add("pitchfork", "⋔");
            htmlEntitiesDict.Add("epar", "⋕");
            htmlEntitiesDict.Add("ltdot", "⋖");
            htmlEntitiesDict.Add("lessdot", "⋖");
            htmlEntitiesDict.Add("gtdot", "⋗");
            htmlEntitiesDict.Add("gtrdot", "⋗");
            htmlEntitiesDict.Add("Ll", "⋘");
            htmlEntitiesDict.Add("Gg", "⋙");
            htmlEntitiesDict.Add("ggg", "⋙");
            htmlEntitiesDict.Add("leg", "⋚");
            htmlEntitiesDict.Add("LessEqualGreater", "⋚");
            htmlEntitiesDict.Add("lesseqgtr", "⋚");
            htmlEntitiesDict.Add("gel", "⋛");
            htmlEntitiesDict.Add("gtreqless", "⋛");
            htmlEntitiesDict.Add("GreaterEqualLess", "⋛");
            htmlEntitiesDict.Add("cuepr", "⋞");
            htmlEntitiesDict.Add("curlyeqprec", "⋞");
            htmlEntitiesDict.Add("cuesc", "⋟");
            htmlEntitiesDict.Add("curlyeqsucc", "⋟");
            htmlEntitiesDict.Add("nprcue", "⋠");
            htmlEntitiesDict.Add("NotPrecedesSlantEqual", "⋠");
            htmlEntitiesDict.Add("nsccue", "⋡");
            htmlEntitiesDict.Add("NotSucceedsSlantEqual", "⋡");
            htmlEntitiesDict.Add("nsqsube", "⋢");
            htmlEntitiesDict.Add("NotSquareSubsetEqual", "⋢");
            htmlEntitiesDict.Add("nsqsupe", "⋣");
            htmlEntitiesDict.Add("NotSquareSupersetEqual", "⋣");
            htmlEntitiesDict.Add("lnsim", "⋦");
            htmlEntitiesDict.Add("gnsim", "⋧");
            htmlEntitiesDict.Add("prnsim", "⋨");
            htmlEntitiesDict.Add("precnsim", "⋨");
            htmlEntitiesDict.Add("scnsim", "⋩");
            htmlEntitiesDict.Add("succnsim", "⋩");
            htmlEntitiesDict.Add("nltri", "⋪");
            htmlEntitiesDict.Add("ntriangleleft", "⋪");
            htmlEntitiesDict.Add("NotLeftTriangle", "⋪");
            htmlEntitiesDict.Add("nrtri", "⋫");
            htmlEntitiesDict.Add("ntriangleright", "⋫");
            htmlEntitiesDict.Add("NotRightTriangle", "⋫");
            htmlEntitiesDict.Add("nltrie", "⋬");
            htmlEntitiesDict.Add("ntrianglelefteq", "⋬");
            htmlEntitiesDict.Add("NotLeftTriangleEqual", "⋬");
            htmlEntitiesDict.Add("nrtrie", "⋭");
            htmlEntitiesDict.Add("ntrianglerighteq", "⋭");
            htmlEntitiesDict.Add("NotRightTriangleEqual", "⋭");
            htmlEntitiesDict.Add("vellip", "⋮");
            htmlEntitiesDict.Add("ctdot", "⋯");
            htmlEntitiesDict.Add("utdot", "⋰");
            htmlEntitiesDict.Add("dtdot", "⋱");
            htmlEntitiesDict.Add("disin", "⋲");
            htmlEntitiesDict.Add("isinsv", "⋳");
            htmlEntitiesDict.Add("isins", "⋴");
            htmlEntitiesDict.Add("isindot", "⋵");
            htmlEntitiesDict.Add("notinvc", "⋶");
            htmlEntitiesDict.Add("notinvb", "⋷");
            htmlEntitiesDict.Add("isinE", "⋹");
            htmlEntitiesDict.Add("nisd", "⋺");
            htmlEntitiesDict.Add("xnis", "⋻");
            htmlEntitiesDict.Add("nis", "⋼");
            htmlEntitiesDict.Add("notnivc", "⋽");
            htmlEntitiesDict.Add("notnivb", "⋾");
            htmlEntitiesDict.Add("barwed", "⌅");
            htmlEntitiesDict.Add("barwedge", "⌅");
            htmlEntitiesDict.Add("Barwed", "⌆");
            htmlEntitiesDict.Add("doublebarwedge", "⌆");
            htmlEntitiesDict.Add("lceil", "⌈");
            htmlEntitiesDict.Add("LeftCeiling", "⌈");
            htmlEntitiesDict.Add("rceil", "⌉");
            htmlEntitiesDict.Add("RightCeiling", "⌉");
            htmlEntitiesDict.Add("lfloor", "⌊");
            htmlEntitiesDict.Add("LeftFloor", "⌊");
            htmlEntitiesDict.Add("rfloor", "⌋");
            htmlEntitiesDict.Add("RightFloor", "⌋");
            htmlEntitiesDict.Add("drcrop", "⌌");
            htmlEntitiesDict.Add("dlcrop", "⌍");
            htmlEntitiesDict.Add("urcrop", "⌎");
            htmlEntitiesDict.Add("ulcrop", "⌏");
            htmlEntitiesDict.Add("bnot", "⌐");
            htmlEntitiesDict.Add("profline", "⌒");
            htmlEntitiesDict.Add("profsurf", "⌓");
            htmlEntitiesDict.Add("telrec", "⌕");
            htmlEntitiesDict.Add("target", "⌖");
            htmlEntitiesDict.Add("ulcorn", "⌜");
            htmlEntitiesDict.Add("ulcorner", "⌜");
            htmlEntitiesDict.Add("urcorn", "⌝");
            htmlEntitiesDict.Add("urcorner", "⌝");
            htmlEntitiesDict.Add("dlcorn", "⌞");
            htmlEntitiesDict.Add("llcorner", "⌞");
            htmlEntitiesDict.Add("drcorn", "⌟");
            htmlEntitiesDict.Add("lrcorner", "⌟");
            htmlEntitiesDict.Add("frown", "⌢");
            htmlEntitiesDict.Add("sfrown", "⌢");
            htmlEntitiesDict.Add("smile", "⌣");
            htmlEntitiesDict.Add("ssmile", "⌣");
            htmlEntitiesDict.Add("cylcty", "⌭");
            htmlEntitiesDict.Add("profalar", "⌮");
            htmlEntitiesDict.Add("topbot", "⌶");
            htmlEntitiesDict.Add("ovbar", "⌽");
            htmlEntitiesDict.Add("solbar", "⌿");
            htmlEntitiesDict.Add("angzarr", "⍼");
            htmlEntitiesDict.Add("lmoust", "⎰");
            htmlEntitiesDict.Add("lmoustache", "⎰");
            htmlEntitiesDict.Add("rmoust", "⎱");
            htmlEntitiesDict.Add("rmoustache", "⎱");
            htmlEntitiesDict.Add("tbrk", "⎴");
            htmlEntitiesDict.Add("OverBracket", "⎴");
            htmlEntitiesDict.Add("bbrk", "⎵");
            htmlEntitiesDict.Add("UnderBracket", "⎵");
            htmlEntitiesDict.Add("bbrktbrk", "⎶");
            htmlEntitiesDict.Add("OverParenthesis", "⏜");
            htmlEntitiesDict.Add("UnderParenthesis", "⏝");
            htmlEntitiesDict.Add("OverBrace", "⏞");
            htmlEntitiesDict.Add("UnderBrace", "⏟");
            htmlEntitiesDict.Add("trpezium", "⏢");
            htmlEntitiesDict.Add("elinters", "⏧");
            htmlEntitiesDict.Add("blank", "␣");
            htmlEntitiesDict.Add("oS", "Ⓢ");
            htmlEntitiesDict.Add("circledS", "Ⓢ");
            htmlEntitiesDict.Add("boxh", "─");
            htmlEntitiesDict.Add("HorizontalLine", "─");
            htmlEntitiesDict.Add("boxv", "│");
            htmlEntitiesDict.Add("boxdr", "┌");
            htmlEntitiesDict.Add("boxdl", "┐");
            htmlEntitiesDict.Add("boxur", "└");
            htmlEntitiesDict.Add("boxul", "┘");
            htmlEntitiesDict.Add("boxvr", "├");
            htmlEntitiesDict.Add("boxvl", "┤");
            htmlEntitiesDict.Add("boxhd", "┬");
            htmlEntitiesDict.Add("boxhu", "┴");
            htmlEntitiesDict.Add("boxvh", "┼");
            htmlEntitiesDict.Add("boxH", "═");
            htmlEntitiesDict.Add("boxV", "║");
            htmlEntitiesDict.Add("boxdR", "╒");
            htmlEntitiesDict.Add("boxDr", "╓");
            htmlEntitiesDict.Add("boxDR", "╔");
            htmlEntitiesDict.Add("boxdL", "╕");
            htmlEntitiesDict.Add("boxDl", "╖");
            htmlEntitiesDict.Add("boxDL", "╗");
            htmlEntitiesDict.Add("boxuR", "╘");
            htmlEntitiesDict.Add("boxUr", "╙");
            htmlEntitiesDict.Add("boxUR", "╚");
            htmlEntitiesDict.Add("boxuL", "╛");
            htmlEntitiesDict.Add("boxUl", "╜");
            htmlEntitiesDict.Add("boxUL", "╝");
            htmlEntitiesDict.Add("boxvR", "╞");
            htmlEntitiesDict.Add("boxVr", "╟");
            htmlEntitiesDict.Add("boxVR", "╠");
            htmlEntitiesDict.Add("boxvL", "╡");
            htmlEntitiesDict.Add("boxVl", "╢");
            htmlEntitiesDict.Add("boxVL", "╣");
            htmlEntitiesDict.Add("boxHd", "╤");
            htmlEntitiesDict.Add("boxhD", "╥");
            htmlEntitiesDict.Add("boxHD", "╦");
            htmlEntitiesDict.Add("boxHu", "╧");
            htmlEntitiesDict.Add("boxhU", "╨");
            htmlEntitiesDict.Add("boxHU", "╩");
            htmlEntitiesDict.Add("boxvH", "╪");
            htmlEntitiesDict.Add("boxVh", "╫");
            htmlEntitiesDict.Add("boxVH", "╬");
            htmlEntitiesDict.Add("uhblk", "▀");
            htmlEntitiesDict.Add("lhblk", "▄");
            htmlEntitiesDict.Add("block", "█");
            htmlEntitiesDict.Add("blk14", "░");
            htmlEntitiesDict.Add("blk12", "▒");
            htmlEntitiesDict.Add("blk34", "▓");
            htmlEntitiesDict.Add("squ", "□");
            htmlEntitiesDict.Add("square", "□");
            htmlEntitiesDict.Add("Square", "□");
            htmlEntitiesDict.Add("squf", "▪");
            htmlEntitiesDict.Add("squarf", "▪");
            htmlEntitiesDict.Add("blacksquare", "▪");
            htmlEntitiesDict.Add("FilledVerySmallSquare", "▪");
            htmlEntitiesDict.Add("EmptyVerySmallSquare", "▫");
            htmlEntitiesDict.Add("rect", "▭");
            htmlEntitiesDict.Add("marker", "▮");
            htmlEntitiesDict.Add("fltns", "▱");
            htmlEntitiesDict.Add("xutri", "△");
            htmlEntitiesDict.Add("bigtriangleup", "△");
            htmlEntitiesDict.Add("utrif", "▴");
            htmlEntitiesDict.Add("blacktriangle", "▴");
            htmlEntitiesDict.Add("utri", "▵");
            htmlEntitiesDict.Add("triangle", "▵");
            htmlEntitiesDict.Add("rtrif", "▸");
            htmlEntitiesDict.Add("blacktriangleright", "▸");
            htmlEntitiesDict.Add("rtri", "▹");
            htmlEntitiesDict.Add("triangleright", "▹");
            htmlEntitiesDict.Add("xdtri", "▽");
            htmlEntitiesDict.Add("bigtriangledown", "▽");
            htmlEntitiesDict.Add("dtrif", "▾");
            htmlEntitiesDict.Add("blacktriangledown", "▾");
            htmlEntitiesDict.Add("dtri", "▿");
            htmlEntitiesDict.Add("triangledown", "▿");
            htmlEntitiesDict.Add("ltrif", "◂");
            htmlEntitiesDict.Add("blacktriangleleft", "◂");
            htmlEntitiesDict.Add("ltri", "◃");
            htmlEntitiesDict.Add("triangleleft", "◃");
            htmlEntitiesDict.Add("loz", "◊");
            htmlEntitiesDict.Add("lozenge", "◊");
            htmlEntitiesDict.Add("cir", "○");
            htmlEntitiesDict.Add("tridot", "◬");
            htmlEntitiesDict.Add("xcirc", "◯");
            htmlEntitiesDict.Add("bigcirc", "◯");
            htmlEntitiesDict.Add("ultri", "◸");
            htmlEntitiesDict.Add("urtri", "◹");
            htmlEntitiesDict.Add("lltri", "◺");
            htmlEntitiesDict.Add("EmptySmallSquare", "◻");
            htmlEntitiesDict.Add("FilledSmallSquare", "◼");
            htmlEntitiesDict.Add("starf", "★");
            htmlEntitiesDict.Add("bigstar", "★");
            htmlEntitiesDict.Add("star", "☆");
            htmlEntitiesDict.Add("phone", "☎");
            htmlEntitiesDict.Add("female", "♀");
            htmlEntitiesDict.Add("male", "♂");
            htmlEntitiesDict.Add("spades", "♠");
            htmlEntitiesDict.Add("spadesuit", "♠");
            htmlEntitiesDict.Add("clubs", "♣");
            htmlEntitiesDict.Add("clubsuit", "♣");
            htmlEntitiesDict.Add("hearts", "♥");
            htmlEntitiesDict.Add("heartsuit", "♥");
            htmlEntitiesDict.Add("diams", "♦");
            htmlEntitiesDict.Add("diamondsuit", "♦");
            htmlEntitiesDict.Add("sung", "♪");
            htmlEntitiesDict.Add("flat", "♭");
            htmlEntitiesDict.Add("natur", "♮");
            htmlEntitiesDict.Add("natural", "♮");
            htmlEntitiesDict.Add("sharp", "♯");
            htmlEntitiesDict.Add("check", "✓");
            htmlEntitiesDict.Add("checkmark", "✓");
            htmlEntitiesDict.Add("cross", "✗");
            htmlEntitiesDict.Add("malt", "✠");
            htmlEntitiesDict.Add("maltese", "✠");
            htmlEntitiesDict.Add("sext", "✶");
            htmlEntitiesDict.Add("VerticalSeparator", "❘");
            htmlEntitiesDict.Add("lbbrk", "❲");
            htmlEntitiesDict.Add("rbbrk", "❳");
            htmlEntitiesDict.Add("bsolhsub", "⟈");
            htmlEntitiesDict.Add("suphsol", "⟉");
            htmlEntitiesDict.Add("lobrk", "⟦");
            htmlEntitiesDict.Add("LeftDoubleBracket", "⟦");
            htmlEntitiesDict.Add("robrk", "⟧");
            htmlEntitiesDict.Add("RightDoubleBracket", "⟧");
            htmlEntitiesDict.Add("lang", "⟨");
            htmlEntitiesDict.Add("LeftAngleBracket", "⟨");
            htmlEntitiesDict.Add("langle", "⟨");
            htmlEntitiesDict.Add("rang", "⟩");
            htmlEntitiesDict.Add("RightAngleBracket", "⟩");
            htmlEntitiesDict.Add("rangle", "⟩");
            htmlEntitiesDict.Add("Lang", "⟪");
            htmlEntitiesDict.Add("Rang", "⟫");
            htmlEntitiesDict.Add("loang", "⟬");
            htmlEntitiesDict.Add("roang", "⟭");
            htmlEntitiesDict.Add("xlarr", "⟵");
            htmlEntitiesDict.Add("longleftarrow", "⟵");
            htmlEntitiesDict.Add("LongLeftArrow", "⟵");
            htmlEntitiesDict.Add("xrarr", "⟶");
            htmlEntitiesDict.Add("longrightarrow", "⟶");
            htmlEntitiesDict.Add("LongRightArrow", "⟶");
            htmlEntitiesDict.Add("xharr", "⟷");
            htmlEntitiesDict.Add("longleftrightarrow", "⟷");
            htmlEntitiesDict.Add("LongLeftRightArrow", "⟷");
            htmlEntitiesDict.Add("xlArr", "⟸");
            htmlEntitiesDict.Add("Longleftarrow", "⟸");
            htmlEntitiesDict.Add("DoubleLongLeftArrow", "⟸");
            htmlEntitiesDict.Add("xrArr", "⟹");
            htmlEntitiesDict.Add("Longrightarrow", "⟹");
            htmlEntitiesDict.Add("DoubleLongRightArrow", "⟹");
            htmlEntitiesDict.Add("xhArr", "⟺");
            htmlEntitiesDict.Add("Longleftrightarrow", "⟺");
            htmlEntitiesDict.Add("DoubleLongLeftRightArrow", "⟺");
            htmlEntitiesDict.Add("xmap", "⟼");
            htmlEntitiesDict.Add("longmapsto", "⟼");
            htmlEntitiesDict.Add("dzigrarr", "⟿");
            htmlEntitiesDict.Add("nvlArr", "⤂");
            htmlEntitiesDict.Add("nvrArr", "⤃");
            htmlEntitiesDict.Add("nvHarr", "⤄");
            htmlEntitiesDict.Add("Map", "⤅");
            htmlEntitiesDict.Add("lbarr", "⤌");
            htmlEntitiesDict.Add("rbarr", "⤍");
            htmlEntitiesDict.Add("bkarow", "⤍");
            htmlEntitiesDict.Add("lBarr", "⤎");
            htmlEntitiesDict.Add("rBarr", "⤏");
            htmlEntitiesDict.Add("dbkarow", "⤏");
            htmlEntitiesDict.Add("RBarr", "⤐");
            htmlEntitiesDict.Add("drbkarow", "⤐");
            htmlEntitiesDict.Add("DDotrahd", "⤑");
            htmlEntitiesDict.Add("UpArrowBar", "⤒");
            htmlEntitiesDict.Add("DownArrowBar", "⤓");
            htmlEntitiesDict.Add("Rarrtl", "⤖");
            htmlEntitiesDict.Add("latail", "⤙");
            htmlEntitiesDict.Add("ratail", "⤚");
            htmlEntitiesDict.Add("lAtail", "⤛");
            htmlEntitiesDict.Add("rAtail", "⤜");
            htmlEntitiesDict.Add("larrfs", "⤝");
            htmlEntitiesDict.Add("rarrfs", "⤞");
            htmlEntitiesDict.Add("larrbfs", "⤟");
            htmlEntitiesDict.Add("rarrbfs", "⤠");
            htmlEntitiesDict.Add("nwarhk", "⤣");
            htmlEntitiesDict.Add("nearhk", "⤤");
            htmlEntitiesDict.Add("searhk", "⤥");
            htmlEntitiesDict.Add("hksearow", "⤥");
            htmlEntitiesDict.Add("swarhk", "⤦");
            htmlEntitiesDict.Add("hkswarow", "⤦");
            htmlEntitiesDict.Add("nwnear", "⤧");
            htmlEntitiesDict.Add("nesear", "⤨");
            htmlEntitiesDict.Add("toea", "⤨");
            htmlEntitiesDict.Add("seswar", "⤩");
            htmlEntitiesDict.Add("tosa", "⤩");
            htmlEntitiesDict.Add("swnwar", "⤪");
            htmlEntitiesDict.Add("rarrc", "⤳");
            htmlEntitiesDict.Add("cudarrr", "⤵");
            htmlEntitiesDict.Add("ldca", "⤶");
            htmlEntitiesDict.Add("rdca", "⤷");
            htmlEntitiesDict.Add("cudarrl", "⤸");
            htmlEntitiesDict.Add("larrpl", "⤹");
            htmlEntitiesDict.Add("curarrm", "⤼");
            htmlEntitiesDict.Add("cularrp", "⤽");
            htmlEntitiesDict.Add("rarrpl", "⥅");
            htmlEntitiesDict.Add("harrcir", "⥈");
            htmlEntitiesDict.Add("Uarrocir", "⥉");
            htmlEntitiesDict.Add("lurdshar", "⥊");
            htmlEntitiesDict.Add("ldrushar", "⥋");
            htmlEntitiesDict.Add("LeftRightVector", "⥎");
            htmlEntitiesDict.Add("RightUpDownVector", "⥏");
            htmlEntitiesDict.Add("DownLeftRightVector", "⥐");
            htmlEntitiesDict.Add("LeftUpDownVector", "⥑");
            htmlEntitiesDict.Add("LeftVectorBar", "⥒");
            htmlEntitiesDict.Add("RightVectorBar", "⥓");
            htmlEntitiesDict.Add("RightUpVectorBar", "⥔");
            htmlEntitiesDict.Add("RightDownVectorBar", "⥕");
            htmlEntitiesDict.Add("DownLeftVectorBar", "⥖");
            htmlEntitiesDict.Add("DownRightVectorBar", "⥗");
            htmlEntitiesDict.Add("LeftUpVectorBar", "⥘");
            htmlEntitiesDict.Add("LeftDownVectorBar", "⥙");
            htmlEntitiesDict.Add("LeftTeeVector", "⥚");
            htmlEntitiesDict.Add("RightTeeVector", "⥛");
            htmlEntitiesDict.Add("RightUpTeeVector", "⥜");
            htmlEntitiesDict.Add("RightDownTeeVector", "⥝");
            htmlEntitiesDict.Add("DownLeftTeeVector", "⥞");
            htmlEntitiesDict.Add("DownRightTeeVector", "⥟");
            htmlEntitiesDict.Add("LeftUpTeeVector", "⥠");
            htmlEntitiesDict.Add("LeftDownTeeVector", "⥡");
            htmlEntitiesDict.Add("lHar", "⥢");
            htmlEntitiesDict.Add("uHar", "⥣");
            htmlEntitiesDict.Add("rHar", "⥤");
            htmlEntitiesDict.Add("dHar", "⥥");
            htmlEntitiesDict.Add("luruhar", "⥦");
            htmlEntitiesDict.Add("ldrdhar", "⥧");
            htmlEntitiesDict.Add("ruluhar", "⥨");
            htmlEntitiesDict.Add("rdldhar", "⥩");
            htmlEntitiesDict.Add("lharul", "⥪");
            htmlEntitiesDict.Add("llhard", "⥫");
            htmlEntitiesDict.Add("rharul", "⥬");
            htmlEntitiesDict.Add("lrhard", "⥭");
            htmlEntitiesDict.Add("udhar", "⥮");
            htmlEntitiesDict.Add("UpEquilibrium", "⥮");
            htmlEntitiesDict.Add("duhar", "⥯");
            htmlEntitiesDict.Add("ReverseUpEquilibrium", "⥯");
            htmlEntitiesDict.Add("RoundImplies", "⥰");
            htmlEntitiesDict.Add("erarr", "⥱");
            htmlEntitiesDict.Add("simrarr", "⥲");
            htmlEntitiesDict.Add("larrsim", "⥳");
            htmlEntitiesDict.Add("rarrsim", "⥴");
            htmlEntitiesDict.Add("rarrap", "⥵");
            htmlEntitiesDict.Add("ltlarr", "⥶");
            htmlEntitiesDict.Add("gtrarr", "⥸");
            htmlEntitiesDict.Add("subrarr", "⥹");
            htmlEntitiesDict.Add("suplarr", "⥻");
            htmlEntitiesDict.Add("lfisht", "⥼");
            htmlEntitiesDict.Add("rfisht", "⥽");
            htmlEntitiesDict.Add("ufisht", "⥾");
            htmlEntitiesDict.Add("dfisht", "⥿");
            htmlEntitiesDict.Add("lopar", "⦅");
            htmlEntitiesDict.Add("ropar", "⦆");
            htmlEntitiesDict.Add("lbrke", "⦋");
            htmlEntitiesDict.Add("rbrke", "⦌");
            htmlEntitiesDict.Add("lbrkslu", "⦍");
            htmlEntitiesDict.Add("rbrksld", "⦎");
            htmlEntitiesDict.Add("lbrksld", "⦏");
            htmlEntitiesDict.Add("rbrkslu", "⦐");
            htmlEntitiesDict.Add("langd", "⦑");
            htmlEntitiesDict.Add("rangd", "⦒");
            htmlEntitiesDict.Add("lparlt", "⦓");
            htmlEntitiesDict.Add("rpargt", "⦔");
            htmlEntitiesDict.Add("gtlPar", "⦕");
            htmlEntitiesDict.Add("ltrPar", "⦖");
            htmlEntitiesDict.Add("vzigzag", "⦚");
            htmlEntitiesDict.Add("vangrt", "⦜");
            htmlEntitiesDict.Add("angrtvbd", "⦝");
            htmlEntitiesDict.Add("ange", "⦤");
            htmlEntitiesDict.Add("range", "⦥");
            htmlEntitiesDict.Add("dwangle", "⦦");
            htmlEntitiesDict.Add("uwangle", "⦧");
            htmlEntitiesDict.Add("angmsdaa", "⦨");
            htmlEntitiesDict.Add("angmsdab", "⦩");
            htmlEntitiesDict.Add("angmsdac", "⦪");
            htmlEntitiesDict.Add("angmsdad", "⦫");
            htmlEntitiesDict.Add("angmsdae", "⦬");
            htmlEntitiesDict.Add("angmsdaf", "⦭");
            htmlEntitiesDict.Add("angmsdag", "⦮");
            htmlEntitiesDict.Add("angmsdah", "⦯");
            htmlEntitiesDict.Add("bemptyv", "⦰");
            htmlEntitiesDict.Add("demptyv", "⦱");
            htmlEntitiesDict.Add("cemptyv", "⦲");
            htmlEntitiesDict.Add("raemptyv", "⦳");
            htmlEntitiesDict.Add("laemptyv", "⦴");
            htmlEntitiesDict.Add("ohbar", "⦵");
            htmlEntitiesDict.Add("omid", "⦶");
            htmlEntitiesDict.Add("opar", "⦷");
            htmlEntitiesDict.Add("operp", "⦹");
            htmlEntitiesDict.Add("olcross", "⦻");
            htmlEntitiesDict.Add("odsold", "⦼");
            htmlEntitiesDict.Add("olcir", "⦾");
            htmlEntitiesDict.Add("ofcir", "⦿");
            htmlEntitiesDict.Add("olt", "⧀");
            htmlEntitiesDict.Add("ogt", "⧁");
            htmlEntitiesDict.Add("cirscir", "⧂");
            htmlEntitiesDict.Add("cirE", "⧃");
            htmlEntitiesDict.Add("solb", "⧄");
            htmlEntitiesDict.Add("bsolb", "⧅");
            htmlEntitiesDict.Add("boxbox", "⧉");
            htmlEntitiesDict.Add("trisb", "⧍");
            htmlEntitiesDict.Add("rtriltri", "⧎");
            htmlEntitiesDict.Add("LeftTriangleBar", "⧏");
            htmlEntitiesDict.Add("RightTriangleBar", "⧐");
            htmlEntitiesDict.Add("iinfin", "⧜");
            htmlEntitiesDict.Add("infintie", "⧝");
            htmlEntitiesDict.Add("nvinfin", "⧞");
            htmlEntitiesDict.Add("eparsl", "⧣");
            htmlEntitiesDict.Add("smeparsl", "⧤");
            htmlEntitiesDict.Add("eqvparsl", "⧥");
            htmlEntitiesDict.Add("lozf", "⧫");
            htmlEntitiesDict.Add("blacklozenge", "⧫");
            htmlEntitiesDict.Add("RuleDelayed", "⧴");
            htmlEntitiesDict.Add("dsol", "⧶");
            htmlEntitiesDict.Add("xodot", "⨀");
            htmlEntitiesDict.Add("bigodot", "⨀");
            htmlEntitiesDict.Add("xoplus", "⨁");
            htmlEntitiesDict.Add("bigoplus", "⨁");
            htmlEntitiesDict.Add("xotime", "⨂");
            htmlEntitiesDict.Add("bigotimes", "⨂");
            htmlEntitiesDict.Add("xuplus", "⨄");
            htmlEntitiesDict.Add("biguplus", "⨄");
            htmlEntitiesDict.Add("xsqcup", "⨆");
            htmlEntitiesDict.Add("bigsqcup", "⨆");
            htmlEntitiesDict.Add("qint", "⨌");
            htmlEntitiesDict.Add("iiiint", "⨌");
            htmlEntitiesDict.Add("fpartint", "⨍");
            htmlEntitiesDict.Add("cirfnint", "⨐");
            htmlEntitiesDict.Add("awint", "⨑");
            htmlEntitiesDict.Add("rppolint", "⨒");
            htmlEntitiesDict.Add("scpolint", "⨓");
            htmlEntitiesDict.Add("npolint", "⨔");
            htmlEntitiesDict.Add("pointint", "⨕");
            htmlEntitiesDict.Add("quatint", "⨖");
            htmlEntitiesDict.Add("intlarhk", "⨗");
            htmlEntitiesDict.Add("pluscir", "⨢");
            htmlEntitiesDict.Add("plusacir", "⨣");
            htmlEntitiesDict.Add("simplus", "⨤");
            htmlEntitiesDict.Add("plusdu", "⨥");
            htmlEntitiesDict.Add("plussim", "⨦");
            htmlEntitiesDict.Add("plustwo", "⨧");
            htmlEntitiesDict.Add("mcomma", "⨩");
            htmlEntitiesDict.Add("minusdu", "⨪");
            htmlEntitiesDict.Add("loplus", "⨭");
            htmlEntitiesDict.Add("roplus", "⨮");
            htmlEntitiesDict.Add("Cross", "⨯");
            htmlEntitiesDict.Add("timesd", "⨰");
            htmlEntitiesDict.Add("timesbar", "⨱");
            htmlEntitiesDict.Add("smashp", "⨳");
            htmlEntitiesDict.Add("lotimes", "⨴");
            htmlEntitiesDict.Add("rotimes", "⨵");
            htmlEntitiesDict.Add("otimesas", "⨶");
            htmlEntitiesDict.Add("Otimes", "⨷");
            htmlEntitiesDict.Add("odiv", "⨸");
            htmlEntitiesDict.Add("triplus", "⨹");
            htmlEntitiesDict.Add("triminus", "⨺");
            htmlEntitiesDict.Add("tritime", "⨻");
            htmlEntitiesDict.Add("iprod", "⨼");
            htmlEntitiesDict.Add("intprod", "⨼");
            htmlEntitiesDict.Add("amalg", "⨿");
            htmlEntitiesDict.Add("capdot", "⩀");
            htmlEntitiesDict.Add("ncup", "⩂");
            htmlEntitiesDict.Add("ncap", "⩃");
            htmlEntitiesDict.Add("capand", "⩄");
            htmlEntitiesDict.Add("cupor", "⩅");
            htmlEntitiesDict.Add("cupcap", "⩆");
            htmlEntitiesDict.Add("capcup", "⩇");
            htmlEntitiesDict.Add("cupbrcap", "⩈");
            htmlEntitiesDict.Add("capbrcup", "⩉");
            htmlEntitiesDict.Add("cupcup", "⩊");
            htmlEntitiesDict.Add("capcap", "⩋");
            htmlEntitiesDict.Add("ccups", "⩌");
            htmlEntitiesDict.Add("ccaps", "⩍");
            htmlEntitiesDict.Add("ccupssm", "⩐");
            htmlEntitiesDict.Add("And", "⩓");
            htmlEntitiesDict.Add("Or", "⩔");
            htmlEntitiesDict.Add("andand", "⩕");
            htmlEntitiesDict.Add("oror", "⩖");
            htmlEntitiesDict.Add("orslope", "⩗");
            htmlEntitiesDict.Add("andslope", "⩘");
            htmlEntitiesDict.Add("andv", "⩚");
            htmlEntitiesDict.Add("orv", "⩛");
            htmlEntitiesDict.Add("andd", "⩜");
            htmlEntitiesDict.Add("ord", "⩝");
            htmlEntitiesDict.Add("wedbar", "⩟");
            htmlEntitiesDict.Add("sdote", "⩦");
            htmlEntitiesDict.Add("simdot", "⩪");
            htmlEntitiesDict.Add("congdot", "⩭");
            htmlEntitiesDict.Add("easter", "⩮");
            htmlEntitiesDict.Add("apacir", "⩯");
            htmlEntitiesDict.Add("apE", "⩰");
            htmlEntitiesDict.Add("eplus", "⩱");
            htmlEntitiesDict.Add("pluse", "⩲");
            htmlEntitiesDict.Add("Esim", "⩳");
            htmlEntitiesDict.Add("Colone", "⩴");
            htmlEntitiesDict.Add("Equal", "⩵");
            htmlEntitiesDict.Add("eDDot", "⩷");
            htmlEntitiesDict.Add("ddotseq", "⩷");
            htmlEntitiesDict.Add("equivDD", "⩸");
            htmlEntitiesDict.Add("ltcir", "⩹");
            htmlEntitiesDict.Add("gtcir", "⩺");
            htmlEntitiesDict.Add("ltquest", "⩻");
            htmlEntitiesDict.Add("gtquest", "⩼");
            htmlEntitiesDict.Add("les", "⩽");
            htmlEntitiesDict.Add("LessSlantEqual", "⩽");
            htmlEntitiesDict.Add("leqslant", "⩽");
            htmlEntitiesDict.Add("ges", "⩾");
            htmlEntitiesDict.Add("GreaterSlantEqual", "⩾");
            htmlEntitiesDict.Add("geqslant", "⩾");
            htmlEntitiesDict.Add("lesdot", "⩿");
            htmlEntitiesDict.Add("gesdot", "⪀");
            htmlEntitiesDict.Add("lesdoto", "⪁");
            htmlEntitiesDict.Add("gesdoto", "⪂");
            htmlEntitiesDict.Add("lesdotor", "⪃");
            htmlEntitiesDict.Add("gesdotol", "⪄");
            htmlEntitiesDict.Add("lap", "⪅");
            htmlEntitiesDict.Add("lessapprox", "⪅");
            htmlEntitiesDict.Add("gap", "⪆");
            htmlEntitiesDict.Add("gtrapprox", "⪆");
            htmlEntitiesDict.Add("lne", "⪇");
            htmlEntitiesDict.Add("lneq", "⪇");
            htmlEntitiesDict.Add("gne", "⪈");
            htmlEntitiesDict.Add("gneq", "⪈");
            htmlEntitiesDict.Add("lnap", "⪉");
            htmlEntitiesDict.Add("lnapprox", "⪉");
            htmlEntitiesDict.Add("gnap", "⪊");
            htmlEntitiesDict.Add("gnapprox", "⪊");
            htmlEntitiesDict.Add("lEg", "⪋");
            htmlEntitiesDict.Add("lesseqqgtr", "⪋");
            htmlEntitiesDict.Add("gEl", "⪌");
            htmlEntitiesDict.Add("gtreqqless", "⪌");
            htmlEntitiesDict.Add("lsime", "⪍");
            htmlEntitiesDict.Add("gsime", "⪎");
            htmlEntitiesDict.Add("lsimg", "⪏");
            htmlEntitiesDict.Add("gsiml", "⪐");
            htmlEntitiesDict.Add("lgE", "⪑");
            htmlEntitiesDict.Add("glE", "⪒");
            htmlEntitiesDict.Add("lesges", "⪓");
            htmlEntitiesDict.Add("gesles", "⪔");
            htmlEntitiesDict.Add("els", "⪕");
            htmlEntitiesDict.Add("eqslantless", "⪕");
            htmlEntitiesDict.Add("egs", "⪖");
            htmlEntitiesDict.Add("eqslantgtr", "⪖");
            htmlEntitiesDict.Add("elsdot", "⪗");
            htmlEntitiesDict.Add("egsdot", "⪘");
            htmlEntitiesDict.Add("el", "⪙");
            htmlEntitiesDict.Add("eg", "⪚");
            htmlEntitiesDict.Add("siml", "⪝");
            htmlEntitiesDict.Add("simg", "⪞");
            htmlEntitiesDict.Add("simlE", "⪟");
            htmlEntitiesDict.Add("simgE", "⪠");
            htmlEntitiesDict.Add("LessLess", "⪡");
            htmlEntitiesDict.Add("GreaterGreater", "⪢");
            htmlEntitiesDict.Add("glj", "⪤");
            htmlEntitiesDict.Add("gla", "⪥");
            htmlEntitiesDict.Add("ltcc", "⪦");
            htmlEntitiesDict.Add("gtcc", "⪧");
            htmlEntitiesDict.Add("lescc", "⪨");
            htmlEntitiesDict.Add("gescc", "⪩");
            htmlEntitiesDict.Add("smt", "⪪");
            htmlEntitiesDict.Add("lat", "⪫");
            htmlEntitiesDict.Add("smte", "⪬");
            htmlEntitiesDict.Add("late", "⪭");
            htmlEntitiesDict.Add("bumpE", "⪮");
            htmlEntitiesDict.Add("pre", "⪯");
            htmlEntitiesDict.Add("preceq", "⪯");
            htmlEntitiesDict.Add("PrecedesEqual", "⪯");
            htmlEntitiesDict.Add("sce", "⪰");
            htmlEntitiesDict.Add("succeq", "⪰");
            htmlEntitiesDict.Add("SucceedsEqual", "⪰");
            htmlEntitiesDict.Add("prE", "⪳");
            htmlEntitiesDict.Add("scE", "⪴");
            htmlEntitiesDict.Add("prnE", "⪵");
            htmlEntitiesDict.Add("precneqq", "⪵");
            htmlEntitiesDict.Add("scnE", "⪶");
            htmlEntitiesDict.Add("succneqq", "⪶");
            htmlEntitiesDict.Add("prap", "⪷");
            htmlEntitiesDict.Add("precapprox", "⪷");
            htmlEntitiesDict.Add("scap", "⪸");
            htmlEntitiesDict.Add("succapprox", "⪸");
            htmlEntitiesDict.Add("prnap", "⪹");
            htmlEntitiesDict.Add("precnapprox", "⪹");
            htmlEntitiesDict.Add("scnap", "⪺");
            htmlEntitiesDict.Add("succnapprox", "⪺");
            htmlEntitiesDict.Add("Pr", "⪻");
            htmlEntitiesDict.Add("Sc", "⪼");
            htmlEntitiesDict.Add("subdot", "⪽");
            htmlEntitiesDict.Add("supdot", "⪾");
            htmlEntitiesDict.Add("subplus", "⪿");
            htmlEntitiesDict.Add("supplus", "⫀");
            htmlEntitiesDict.Add("submult", "⫁");
            htmlEntitiesDict.Add("supmult", "⫂");
            htmlEntitiesDict.Add("subedot", "⫃");
            htmlEntitiesDict.Add("supedot", "⫄");
            htmlEntitiesDict.Add("subE", "⫅");
            htmlEntitiesDict.Add("subseteqq", "⫅");
            htmlEntitiesDict.Add("supE", "⫆");
            htmlEntitiesDict.Add("supseteqq", "⫆");
            htmlEntitiesDict.Add("subsim", "⫇");
            htmlEntitiesDict.Add("supsim", "⫈");
            htmlEntitiesDict.Add("subnE", "⫋");
            htmlEntitiesDict.Add("subsetneqq", "⫋");
            htmlEntitiesDict.Add("supnE", "⫌");
            htmlEntitiesDict.Add("supsetneqq", "⫌");
            htmlEntitiesDict.Add("csub", "⫏");
            htmlEntitiesDict.Add("csup", "⫐");
            htmlEntitiesDict.Add("csube", "⫑");
            htmlEntitiesDict.Add("csupe", "⫒");
            htmlEntitiesDict.Add("subsup", "⫓");
            htmlEntitiesDict.Add("supsub", "⫔");
            htmlEntitiesDict.Add("subsub", "⫕");
            htmlEntitiesDict.Add("supsup", "⫖");
            htmlEntitiesDict.Add("suphsub", "⫗");
            htmlEntitiesDict.Add("supdsub", "⫘");
            htmlEntitiesDict.Add("forkv", "⫙");
            htmlEntitiesDict.Add("topfork", "⫚");
            htmlEntitiesDict.Add("mlcp", "⫛");
            htmlEntitiesDict.Add("dashv", "⫤");
            htmlEntitiesDict.Add("DoubleLeftTee", "⫤");
            htmlEntitiesDict.Add("vdashl", "⫦");
            htmlEntitiesDict.Add("color", "⫧");
            htmlEntitiesDict.Add("vBar", "⫨");
            htmlEntitiesDict.Add("vBarv", "⫩");
            htmlEntitiesDict.Add("vbar", "⫫");
            htmlEntitiesDict.Add("Not", "⫬");
            htmlEntitiesDict.Add("bNot", "⫭");
            htmlEntitiesDict.Add("rnmid", "⫮");
            htmlEntitiesDict.Add("cirmid", "⫯");
            htmlEntitiesDict.Add("midcir", "⫰");
            htmlEntitiesDict.Add("topcir", "⫱");
            htmlEntitiesDict.Add("nhpar", "⫲");
            htmlEntitiesDict.Add("parsim", "⫳");
            htmlEntitiesDict.Add("parsl", "⫽");
            htmlEntitiesDict.Add("fflig", "ﬀ");
            htmlEntitiesDict.Add("filig", "ﬁ");
            htmlEntitiesDict.Add("fllig", "ﬂ");
            htmlEntitiesDict.Add("ffilig", "ﬃ");
            htmlEntitiesDict.Add("ffllig", "ﬄ");
            htmlEntitiesDict.Add("Ascr", "𝒜");
            htmlEntitiesDict.Add("Cscr", "𝒞");
            htmlEntitiesDict.Add("Dscr", "𝒟");
            htmlEntitiesDict.Add("Gscr", "𝒢");
            htmlEntitiesDict.Add("Jscr", "𝒥");
            htmlEntitiesDict.Add("Kscr", "𝒦");
            htmlEntitiesDict.Add("Nscr", "𝒩");
            htmlEntitiesDict.Add("Oscr", "𝒪");
            htmlEntitiesDict.Add("Pscr", "𝒫");
            htmlEntitiesDict.Add("Qscr", "𝒬");
            htmlEntitiesDict.Add("Sscr", "𝒮");
            htmlEntitiesDict.Add("Tscr", "𝒯");
            htmlEntitiesDict.Add("Uscr", "𝒰");
            htmlEntitiesDict.Add("Vscr", "𝒱");
            htmlEntitiesDict.Add("Wscr", "𝒲");
            htmlEntitiesDict.Add("Xscr", "𝒳");
            htmlEntitiesDict.Add("Yscr", "𝒴");
            htmlEntitiesDict.Add("Zscr", "𝒵");
            htmlEntitiesDict.Add("ascr", "𝒶");
            htmlEntitiesDict.Add("bscr", "𝒷");
            htmlEntitiesDict.Add("cscr", "𝒸");
            htmlEntitiesDict.Add("dscr", "𝒹");
            htmlEntitiesDict.Add("fscr", "𝒻");
            htmlEntitiesDict.Add("hscr", "𝒽");
            htmlEntitiesDict.Add("iscr", "𝒾");
            htmlEntitiesDict.Add("jscr", "𝒿");
            htmlEntitiesDict.Add("kscr", "𝓀");
            htmlEntitiesDict.Add("lscr", "𝓁");
            htmlEntitiesDict.Add("mscr", "𝓂");
            htmlEntitiesDict.Add("nscr", "𝓃");
            htmlEntitiesDict.Add("pscr", "𝓅");
            htmlEntitiesDict.Add("qscr", "𝓆");
            htmlEntitiesDict.Add("rscr", "𝓇");
            htmlEntitiesDict.Add("sscr", "𝓈");
            htmlEntitiesDict.Add("tscr", "𝓉");
            htmlEntitiesDict.Add("uscr", "𝓊");
            htmlEntitiesDict.Add("vscr", "𝓋");
            htmlEntitiesDict.Add("wscr", "𝓌");
            htmlEntitiesDict.Add("xscr", "𝓍");
            htmlEntitiesDict.Add("yscr", "𝓎");
            htmlEntitiesDict.Add("zscr", "𝓏");
            htmlEntitiesDict.Add("Afr", "𝔄");
            htmlEntitiesDict.Add("Bfr", "𝔅");
            htmlEntitiesDict.Add("Dfr", "𝔇");
            htmlEntitiesDict.Add("Efr", "𝔈");
            htmlEntitiesDict.Add("Ffr", "𝔉");
            htmlEntitiesDict.Add("Gfr", "𝔊");
            htmlEntitiesDict.Add("Jfr", "𝔍");
            htmlEntitiesDict.Add("Kfr", "𝔎");
            htmlEntitiesDict.Add("Lfr", "𝔏");
            htmlEntitiesDict.Add("Mfr", "𝔐");
            htmlEntitiesDict.Add("Nfr", "𝔑");
            htmlEntitiesDict.Add("Ofr", "𝔒");
            htmlEntitiesDict.Add("Pfr", "𝔓");
            htmlEntitiesDict.Add("Qfr", "𝔔");
            htmlEntitiesDict.Add("Sfr", "𝔖");
            htmlEntitiesDict.Add("Tfr", "𝔗");
            htmlEntitiesDict.Add("Ufr", "𝔘");
            htmlEntitiesDict.Add("Vfr", "𝔙");
            htmlEntitiesDict.Add("Wfr", "𝔚");
            htmlEntitiesDict.Add("Xfr", "𝔛");
            htmlEntitiesDict.Add("Yfr", "𝔜");
            htmlEntitiesDict.Add("afr", "𝔞");
            htmlEntitiesDict.Add("bfr", "𝔟");
            htmlEntitiesDict.Add("cfr", "𝔠");
            htmlEntitiesDict.Add("dfr", "𝔡");
            htmlEntitiesDict.Add("efr", "𝔢");
            htmlEntitiesDict.Add("ffr", "𝔣");
            htmlEntitiesDict.Add("gfr", "𝔤");
            htmlEntitiesDict.Add("hfr", "𝔥");
            htmlEntitiesDict.Add("ifr", "𝔦");
            htmlEntitiesDict.Add("jfr", "𝔧");
            htmlEntitiesDict.Add("kfr", "𝔨");
            htmlEntitiesDict.Add("lfr", "𝔩");
            htmlEntitiesDict.Add("mfr", "𝔪");
            htmlEntitiesDict.Add("nfr", "𝔫");
            htmlEntitiesDict.Add("ofr", "𝔬");
            htmlEntitiesDict.Add("pfr", "𝔭");
            htmlEntitiesDict.Add("qfr", "𝔮");
            htmlEntitiesDict.Add("rfr", "𝔯");
            htmlEntitiesDict.Add("sfr", "𝔰");
            htmlEntitiesDict.Add("tfr", "𝔱");
            htmlEntitiesDict.Add("ufr", "𝔲");
            htmlEntitiesDict.Add("vfr", "𝔳");
            htmlEntitiesDict.Add("wfr", "𝔴");
            htmlEntitiesDict.Add("xfr", "𝔵");
            htmlEntitiesDict.Add("yfr", "𝔶");
            htmlEntitiesDict.Add("zfr", "𝔷");
            htmlEntitiesDict.Add("Aopf", "𝔸");
            htmlEntitiesDict.Add("Bopf", "𝔹");
            htmlEntitiesDict.Add("Dopf", "𝔻");
            htmlEntitiesDict.Add("Eopf", "𝔼");
            htmlEntitiesDict.Add("Fopf", "𝔽");
            htmlEntitiesDict.Add("Gopf", "𝔾");
            htmlEntitiesDict.Add("Iopf", "𝕀");
            htmlEntitiesDict.Add("Jopf", "𝕁");
            htmlEntitiesDict.Add("Kopf", "𝕂");
            htmlEntitiesDict.Add("Lopf", "𝕃");
            htmlEntitiesDict.Add("Mopf", "𝕄");
            htmlEntitiesDict.Add("Oopf", "𝕆");
            htmlEntitiesDict.Add("Sopf", "𝕊");
            htmlEntitiesDict.Add("Topf", "𝕋");
            htmlEntitiesDict.Add("Uopf", "𝕌");
            htmlEntitiesDict.Add("Vopf", "𝕍");
            htmlEntitiesDict.Add("Wopf", "𝕎");
            htmlEntitiesDict.Add("Xopf", "𝕏");
            htmlEntitiesDict.Add("Yopf", "𝕐");
            htmlEntitiesDict.Add("aopf", "𝕒");
            htmlEntitiesDict.Add("bopf", "𝕓");
            htmlEntitiesDict.Add("copf", "𝕔");
            htmlEntitiesDict.Add("dopf", "𝕕");
            htmlEntitiesDict.Add("eopf", "𝕖");
            htmlEntitiesDict.Add("fopf", "𝕗");
            htmlEntitiesDict.Add("gopf", "𝕘");
            htmlEntitiesDict.Add("hopf", "𝕙");
            htmlEntitiesDict.Add("iopf", "𝕚");
            htmlEntitiesDict.Add("jopf", "𝕛");
            htmlEntitiesDict.Add("kopf", "𝕜");
            htmlEntitiesDict.Add("lopf", "𝕝");
            htmlEntitiesDict.Add("mopf", "𝕞");
            htmlEntitiesDict.Add("nopf", "𝕟");
            htmlEntitiesDict.Add("oopf", "𝕠");
            htmlEntitiesDict.Add("popf", "𝕡");
            htmlEntitiesDict.Add("qopf", "𝕢");
            htmlEntitiesDict.Add("ropf", "𝕣");
            htmlEntitiesDict.Add("sopf", "𝕤");
            htmlEntitiesDict.Add("topf", "𝕥");
            htmlEntitiesDict.Add("uopf", "𝕦");
            htmlEntitiesDict.Add("vopf", "𝕧");
            htmlEntitiesDict.Add("wopf", "𝕨");
            htmlEntitiesDict.Add("xopf", "𝕩");
            htmlEntitiesDict.Add("yopf", "𝕪");
            htmlEntitiesDict.Add("zopf", "𝕫");
            htmlEntitiesDict.Add("nvlt", "<⃒");
            htmlEntitiesDict.Add("bne", "=⃥");
            htmlEntitiesDict.Add("nvgt", ">⃒");
            htmlEntitiesDict.Add("fjlig", "fj");
            htmlEntitiesDict.Add("ThickSpace", "");
            htmlEntitiesDict.Add("nrarrw", "↝̸");
            htmlEntitiesDict.Add("npart", "∂̸");
            htmlEntitiesDict.Add("nang", "∠⃒");
            htmlEntitiesDict.Add("caps", "∩︀");
            htmlEntitiesDict.Add("cups", "∪︀");
            htmlEntitiesDict.Add("nvsim", "∼⃒");
            htmlEntitiesDict.Add("race", "∽̱");
            htmlEntitiesDict.Add("acE", "∾̳");
            htmlEntitiesDict.Add("nesim", "≂̸");
            htmlEntitiesDict.Add("NotEqualTilde", "≂̸");
            htmlEntitiesDict.Add("napid", "≋̸");
            htmlEntitiesDict.Add("nvap", "≍⃒");
            htmlEntitiesDict.Add("nbump", "≎̸");
            htmlEntitiesDict.Add("NotHumpDownHump", "≎̸");
            htmlEntitiesDict.Add("nbumpe", "≏̸");
            htmlEntitiesDict.Add("NotHumpEqual", "≏̸");
            htmlEntitiesDict.Add("nedot", "≐̸");
            htmlEntitiesDict.Add("bnequiv", "≡⃥");
            htmlEntitiesDict.Add("nvle", "≤⃒");
            htmlEntitiesDict.Add("nvge", "≥⃒");
            htmlEntitiesDict.Add("nlE", "≦̸");
            htmlEntitiesDict.Add("nleqq", "≦̸");
            htmlEntitiesDict.Add("ngE", "≧̸");
            htmlEntitiesDict.Add("ngeqq", "≧̸");
            htmlEntitiesDict.Add("NotGreaterFullEqual", "≧̸");
            htmlEntitiesDict.Add("lvertneqq", "≨︀");
            htmlEntitiesDict.Add("lvnE", "≨︀");
            htmlEntitiesDict.Add("gvertneqq", "≩︀");
            htmlEntitiesDict.Add("gvnE", "≩︀");
            htmlEntitiesDict.Add("nLtv", "≪̸");
            htmlEntitiesDict.Add("NotLessLess", "≪̸");
            htmlEntitiesDict.Add("nLt", "≪⃒");
            htmlEntitiesDict.Add("nGtv", "≫̸");
            htmlEntitiesDict.Add("NotGreaterGreater", "≫̸");
            htmlEntitiesDict.Add("nGt", "≫⃒");
            htmlEntitiesDict.Add("NotSucceedsTilde", "≿̸");
            htmlEntitiesDict.Add("NotSubset", "⊂⃒");
            htmlEntitiesDict.Add("nsubset", "⊂⃒");
            htmlEntitiesDict.Add("vnsub", "⊂⃒");
            htmlEntitiesDict.Add("NotSuperset", "⊃⃒");
            htmlEntitiesDict.Add("nsupset", "⊃⃒");
            htmlEntitiesDict.Add("vnsup", "⊃⃒");
            htmlEntitiesDict.Add("varsubsetneq", "⊊︀");
            htmlEntitiesDict.Add("vsubne", "⊊︀");
            htmlEntitiesDict.Add("varsupsetneq", "⊋︀");
            htmlEntitiesDict.Add("vsupne", "⊋︀");
            htmlEntitiesDict.Add("NotSquareSubset", "⊏̸");
            htmlEntitiesDict.Add("NotSquareSuperset", "⊐̸");
            htmlEntitiesDict.Add("sqcaps", "⊓︀");
            htmlEntitiesDict.Add("sqcups", "⊔︀");
            htmlEntitiesDict.Add("nvltrie", "⊴⃒");
            htmlEntitiesDict.Add("nvrtrie", "⊵⃒");
            htmlEntitiesDict.Add("nLl", "⋘̸");
            htmlEntitiesDict.Add("nGg", "⋙̸");
            htmlEntitiesDict.Add("lesg", "⋚︀");
            htmlEntitiesDict.Add("gesl", "⋛︀");
            htmlEntitiesDict.Add("notindot", "⋵̸");
            htmlEntitiesDict.Add("notinE", "⋹̸");
            htmlEntitiesDict.Add("nrarrc", "⤳̸");
            htmlEntitiesDict.Add("NotLeftTriangleBar", "⧏̸");
            htmlEntitiesDict.Add("NotRightTriangleBar", "⧐̸");
            htmlEntitiesDict.Add("ncongdot", "⩭̸");
            htmlEntitiesDict.Add("napE", "⩰̸");
            htmlEntitiesDict.Add("nleqslant", "⩽̸");
            htmlEntitiesDict.Add("nles", "⩽̸");
            htmlEntitiesDict.Add("NotLessSlantEqual", "⩽̸");
            htmlEntitiesDict.Add("ngeqslant", "⩾̸");
            htmlEntitiesDict.Add("nges", "⩾̸");
            htmlEntitiesDict.Add("NotGreaterSlantEqual", "⩾̸");
            htmlEntitiesDict.Add("NotNestedLessLess", "⪡̸");
            htmlEntitiesDict.Add("NotNestedGreaterGreater", "⪢̸");
            htmlEntitiesDict.Add("smtes", "⪬︀");
            htmlEntitiesDict.Add("lates", "⪭︀");
            htmlEntitiesDict.Add("NotPrecedesEqual", "⪯̸");
            htmlEntitiesDict.Add("npre", "⪯̸");
            htmlEntitiesDict.Add("npreceq", "⪯̸");
            htmlEntitiesDict.Add("NotSucceedsEqual", "⪰̸");
            htmlEntitiesDict.Add("nsce", "⪰̸");
            htmlEntitiesDict.Add("nsucceq", "⪰̸");
            htmlEntitiesDict.Add("nsubE", "⫅̸");
            htmlEntitiesDict.Add("nsubseteqq", "⫅̸");
            htmlEntitiesDict.Add("nsupE", "⫆̸");
            htmlEntitiesDict.Add("nsupseteqq", "⫆̸");
            htmlEntitiesDict.Add("varsubsetneqq", "⫋︀");
            htmlEntitiesDict.Add("vsubnE", "⫋︀");
            htmlEntitiesDict.Add("varsupsetneqq", "⫌︀");
            htmlEntitiesDict.Add("vsupnE", "⫌︀");
            htmlEntitiesDict.Add("nparsl", "⫽⃥");

            #endregion
        }
    }

    internal static void InitHtmlEntitiesFullNames( /*ITwoWayDictionary<string, string> htmlEntitiesFullNames2*/)
    {
        if (htmlEntitiesFullNames == null)
        {
            InitHtmlEntitiesDict();
            htmlEntitiesFullNames = /*htmlEntitiesFullNames2;*/ new Dictionary<string, string>();
            foreach (var item in htmlEntitiesDict)
                if (!htmlEntitiesFullNames.ContainsKey(item.Value))
                    htmlEntitiesFullNames.Add(item.Value, item.Key);
        }
    }

    /// <summary>
    ///     Before calling is needed to call InitHtmlEntitiesFullNames
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string HtmlEncode(string v)
    {
        if (v.Length == 1)
        {
            return HtmlEncodeOne(v);
        }

        var sb = new StringBuilder();
        foreach (var item in v) sb.Append(HtmlEncodeOne(item.ToString()));
        return sb.ToString();
    }

    private static string HtmlEncodeOne(string v)
    {
        if (htmlEntitiesFullNames.ContainsKey(v))
            return htmlEntitiesFullNames[v];
        return v;
    }
}