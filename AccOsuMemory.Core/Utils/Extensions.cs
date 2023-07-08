namespace AccOsuMemory.Core.Utils;

public static class Extensions
{
    public static T GetAndRemove<T>(this List<T> list, int index = 0)
    {
        var item = list[index];
        list.RemoveAt(index);
        return item;
    }
}