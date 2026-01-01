namespace SunamoHtml.Args;

/// <summary>
/// Arguments for start section of HTML boilerplate.
/// </summary>
public class BoilerplateStartArgs
{
    /// <summary>
    /// Gets or sets the CSS content or path.
    /// </summary>
    public string Css { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether to directly inject CSS content (only for CSS).
    /// </summary>
    public bool IsDirectInject { get; set; }

    /// <summary>
    /// Gets or sets the JavaScript content or path.
    /// </summary>
    public string Js { get; set; } = string.Empty;
}