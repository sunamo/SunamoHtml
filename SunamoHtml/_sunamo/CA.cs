// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml._sunamo;

internal class CA
{

    internal static List<List<T>> DivideBy<T>(List<T> divs, int countOfColumn)
    {
        if (divs.Count % countOfColumn != 0)
        {
            throw new Exception($"Elements in {nameof(divs)} is not dividable by {nameof(countOfColumn)}");
        }

        List<List<T>> result = new List<List<T>>();

        List<T> t = new List<T>();

        foreach (var item in divs)
        {
            t.Add(item);
            if (t.Count == countOfColumn)
            {
                result.Add(t);
                t = new List<T>();
            }
        }

        return result;
    }


}