namespace AccOsuMemory.Core.OsuApi.Utils;

[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
public class UrlParaName:Attribute
{
    public string Name;

    public UrlParaName(string name)
    {
        this.Name = name;
    }
}