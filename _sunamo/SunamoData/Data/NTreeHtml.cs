namespace SunamoHtml;


internal delegate void TreeVisitor<T>(T nodeData);
/// <summary>
/// Another big popular tree is on https://www.codeproject.com/Articles/12592/Generic-Tree-T-in-C
/// </summary>
/// <typeparam name="T"></typeparam>
public class NTreeHtml<T>
{
    internal T data;
    internal LinkedList<NTreeHtml<T>> children;
    internal NTreeHtml(T data)
    {
        this.data = data;
        children = new LinkedList<NTreeHtml<T>>();
    }
    internal NTreeHtml<T> AddChild(T data)
    {
        var child = new NTreeHtml<T>(data);
        children.AddFirst(child);
        return child;
    }
    internal NTreeHtml<T> GetChild(int i)
    {
        foreach (NTreeHtml<T> n in children)
            if (--i == 0)
                return n;
        return null;
    }
    internal void Traverse(NTreeHtml<T> node, TreeVisitor<T> visitor)
    {
        visitor(node.data);
        foreach (NTreeHtml<T> kid in node.children)
            Traverse(kid, visitor);
    }
}