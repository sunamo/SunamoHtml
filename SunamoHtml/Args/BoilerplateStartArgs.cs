namespace SunamoHtml.Args;

public class BoilerplateStartArgs
{
    public string Css { get; set; } = string.Empty;

    // Only for CSS
    public bool IsDirectInject { get; set; }

    public string Js { get; set; } = string.Empty;
}
