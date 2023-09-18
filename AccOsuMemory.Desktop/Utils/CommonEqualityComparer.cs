using System;
using System.Collections.Generic;

namespace AccOsuMemory.Desktop.Utils;

public class CommonEqualityComparer<T,V> : IEqualityComparer<T>
{
    private Func<T, V> _keySelector;

    public CommonEqualityComparer(Func<T,V> keySelector)
    {
        _keySelector = keySelector;
    }
    
    public bool Equals(T? x, T? y)
    {
        return EqualityComparer<V>.Default.Equals(_keySelector(x), _keySelector(y));
    }

    public int GetHashCode(T obj)
    {
        return EqualityComparer<V>.Default.GetHashCode(_keySelector(obj));
    }
}