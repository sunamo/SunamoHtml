namespace SunamoHtml._public.SunamoData.Data;

/// <summary>
/// Represents the n tree html t type.
/// </summary>
public class NTreeHtml<T>
{
    /// <summary>
    /// Gets the children.
    /// </summary>
    public LinkedList<NTreeHtml<T>> Children { get; } = new();

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// N tree html.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>The n tree html result.</returns>
    public NTreeHtml(T data)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the add child class.
    /// </summary>
    /// <param name="data">The data.</param>
    public NTreeHtml<T> AddChild(T data)
    {
        var child = new NTreeHtml<T>(data);
        Children.AddFirst(child);
        return child;
    }

    /// <summary>
    /// Get child.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="data">The data.</param>
    /// <returns>The get child result.</returns>
    public NTreeHtml<T>? GetChild(int index)
    {
        foreach (var node in Children)
            if (--index == 0)
                return node;
        return null;
    }

    /// <summary>
    /// Initializes a new instance of the traverse class.
    /// </summary>
    /// <param name="Children">The children.</param>
    public void Traverse(NTreeHtml<T> node, Action<T> visitor)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (visitor == null) throw new ArgumentNullException(nameof(visitor));
        visitor(node.Data);
        foreach (var child in node.Children)
            Traverse(child, visitor);
    }
}
