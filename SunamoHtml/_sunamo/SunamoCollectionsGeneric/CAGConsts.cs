namespace SunamoHtml._sunamo.SunamoCollectionsGeneric;

internal class CAGConsts
{
    // Tady to musí být, SunamoValues nemůže dědit od SunamoCollectionGeneric - vzniklo by Cycle detected
    // Těch pár řádků mě snad nezabije.
    internal static List<T> ToList<T>(params T[] array)
    {
        return array.ToList();
    }
}
