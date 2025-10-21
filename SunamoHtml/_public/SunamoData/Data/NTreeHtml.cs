// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml._public.SunamoData.Data;

public class NTreeHtml<T>
{
    public LinkedList<NTreeHtml<T>> children;
    public T data;

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
        foreach (var n in children)
            if (--i == 0)
                return n;
        return null;
    }

    public void Traverse(NTreeHtml<T> node, Action<T> visitor)
    {
        visitor(node.data);
        foreach (var kid in node.children)
            Traverse(kid, visitor);
    }
}