namespace SunamoHtml;







public class NTreeHtml<T>
{
    public T data;
    public LinkedList<NTreeHtml<T>> children;
    public NTreeHtml(T data)
    {
        this.data = data;
        children = new LinkedList<NTreeHtml<T>>();
    }
    public NTreeHtml<T> AddChild(T data)
    {
        var child = new NTreeHtml<T>(data);
        children.AddFirst(child);
        return child;
    }
    public NTreeHtml<T> GetChild(int i)
    {
        foreach (NTreeHtml<T> n in children)
            if (--i == 0)
                return n;
        return null;
    }
    public void Traverse(NTreeHtml<T> node, Action<T> visitor)
    {
        visitor(node.data);
        foreach (NTreeHtml<T> kid in node.children)
            Traverse(kid, visitor);
    }
}