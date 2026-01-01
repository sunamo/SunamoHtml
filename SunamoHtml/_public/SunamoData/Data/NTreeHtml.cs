namespace SunamoHtml._public.SunamoData.Data;

/// <summary>
/// EN: Generic N-ary tree structure for HTML nodes.
/// CZ: Obecná N-ární stromová struktura pro HTML uzly.
/// </summary>
/// <typeparam name="T">The type of data stored in each node.</typeparam>
public class NTreeHtml<T>
{
    /// <summary>
    /// EN: Gets or sets the child nodes of this node.
    /// CZ: Získá nebo nastaví podřízené uzly tohoto uzlu.
    /// </summary>
    public LinkedList<NTreeHtml<T>> Children { get; set; }

    /// <summary>
    /// EN: Gets or sets the data stored in this node.
    /// CZ: Získá nebo nastaví data uložená v tomto uzlu.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Initializes a new instance of the NTreeHtml class with specified data.
    /// </summary>
    /// <param name="data">The data to store in this node.</param>
    public NTreeHtml(T data)
    {
        Data = data;
        Children = new LinkedList<NTreeHtml<T>>();
    }

    /// <summary>
    /// EN: Adds a new child node with specified data to the beginning of the children list.
    /// CZ: Přidá nový podřízený uzel se zadanými daty na začátek seznamu dětí.
    /// </summary>
    /// <param name="data">The data for the new child node.</param>
    /// <returns>The newly created child node.</returns>
    public NTreeHtml<T> AddChild(T data)
    {
        var child = new NTreeHtml<T>(data);
        Children.AddFirst(child);
        return child;
    }

    /// <summary>
    /// EN: Gets the child node at the specified index.
    /// CZ: Získá podřízený uzel na zadaném indexu.
    /// </summary>
    /// <param name="index">The zero-based index of the child to retrieve.</param>
    /// <returns>The child node at the specified index, or null if not found.</returns>
    public NTreeHtml<T> GetChild(int index)
    {
        foreach (var node in Children)
            if (--index == 0)
                return node;
        return null;
    }

    /// <summary>
    /// EN: Traverses the tree starting from the specified node, calling the visitor action for each node.
    /// CZ: Projde strom od zadaného uzlu, zavolá akci visitor pro každý uzel.
    /// </summary>
    /// <param name="node">The root node to start traversal from.</param>
    /// <param name="visitor">The action to call for each node's data.</param>
    public void Traverse(NTreeHtml<T> node, Action<T> visitor)
    {
        visitor(node.Data);
        foreach (var child in node.Children)
            Traverse(child, visitor);
    }
}
