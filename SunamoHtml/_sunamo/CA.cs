namespace SunamoHtml._sunamo;

internal class CA
{
    internal static List<List<T>> DivideBy<T>(List<T> items, int groupSize)
    {
        if (items.Count % groupSize != 0)
        {
            throw new InvalidOperationException($"Elements in {nameof(items)} is not dividable by {nameof(groupSize)}");
        }

        var result = new List<List<T>>();
        var currentGroup = new List<T>();

        foreach (var item in items)
        {
            currentGroup.Add(item);
            if (currentGroup.Count == groupSize)
            {
                result.Add(currentGroup);
                currentGroup = new List<T>();
            }
        }

        return result;
    }
}
