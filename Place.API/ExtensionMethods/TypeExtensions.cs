namespace Place.API.ExtensionMethods;

public static class TypeExtensions
{
    public static T CastTo<T>(this object obj)
    {
        return (T)obj;
    }
}