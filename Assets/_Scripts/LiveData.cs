using System;

public class LiveData<T>
{
    private T _value;

    public Action<T> OnChange;

    public void Set(T value)
    {
        _value = value;
        OnChange?.Invoke(value);
    }

    public T Get()
    {
        return _value;
    }
}
