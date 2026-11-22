namespace SunamoHtml.Args;

/// <summary>
/// Represents the boilerplate start args type.
/// </summary>
public class BoilerplateStartArgs
{
    /// <summary>
    /// Gets or sets the css.
    /// </summary>
    public string Css { get; set; } = string.Empty;

    // Only for CSS
    /// <summary>
    /// Gets or sets the is direct inject.
    /// </summary>
    public bool IsDirectInject { get; set; }

    /// <summary>
    /// Gets or sets the js.
    /// </summary>
    public string Js { get; set; } = string.Empty;
}
