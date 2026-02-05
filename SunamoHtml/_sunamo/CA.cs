namespace SunamoHtml._sunamo;

/// <summary>
/// EN: Collection Array helper methods for dividing collections into groups.
/// CZ: Pomocné metody pro dělení kolekcí do skupin.
/// </summary>
internal class CA
{
    /// <summary>
    /// EN: Divides a list into groups of specified size.
    /// CZ: Rozdělí seznam do skupin zadané velikosti.
    /// Throws exception if the list cannot be evenly divided by the group size.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="items">The list to divide.</param>
    /// <param name="groupSize">The number of items per group.</param>
    /// <returns>List of groups, each containing the specified number of items.</returns>
    /// <exception cref="Exception">Thrown when the list count is not evenly divisible by group size.</exception>
    internal static List<List<T>> DivideBy<T>(List<T> items, int groupSize)
    {
        if (items.Count % groupSize != 0)
        {
            throw new Exception($"Elements in {nameof(items)} is not dividable by {nameof(groupSize)}");
        }

        List<List<T>> result = new List<List<T>>();

        List<T> currentGroup = new List<T>();

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