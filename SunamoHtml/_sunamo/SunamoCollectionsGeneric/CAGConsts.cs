namespace SunamoHtml._sunamo.SunamoCollectionsGeneric;

internal class CAGConsts
{
    // It must be here, SunamoValues cannot inherit from SunamoCollectionGeneric - it would cause Cycle detected
    // Those few lines will hopefully not kill me.
    internal static List<T> ToList<T>(params T[] array)
    {
        return array.ToList();
    }
}
