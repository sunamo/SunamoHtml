namespace SunamoHtml._public.SunamoData.Data;

public class NTreeHtml<T>
{
    public LinkedList<NTreeHtml<T>> Children { get; } = new();

    public T Data { get; set; }

    public NTreeHtml(T data)
    {
        Data = data;
    }

    public NTreeHtml<T> AddChild(T data)
    {
        var child = new NTreeHtml<T>(data);
        Children.AddFirst(child);
        return child;
    }

    public NTreeHtml<T>? GetChild(int index)
    {
        foreach (var node in Children)
            if (--index == 0)
                return node;
        return null;
    }

    public void Traverse(NTreeHtml<T> node, Action<T> visitor)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (visitor == null) throw new ArgumentNullException(nameof(visitor));
        visitor(node.Data);
        foreach (var child in node.Children)
            Traverse(child, visitor);
    }
}
