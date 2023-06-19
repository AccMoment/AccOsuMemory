namespace AccOsuMemory.Core.Attribute;

[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
public class UrlParam:System.Attribute
{
    public readonly string Name;

    public UrlParam(string name)
    {
        Name = name;
    }
}