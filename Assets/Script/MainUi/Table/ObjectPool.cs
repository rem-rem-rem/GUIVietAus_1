using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class
{
    private readonly Queue<T> _objects;
    private readonly System.Func<T> _objectGenerator;

    public ObjectPool(System.Func<T> objectGenerator, int initialCapacity = 10)
    {
        _objects = new Queue<T>(initialCapacity);
        _objectGenerator = objectGenerator;

        //for (int i = 0; i < initialCapacity; i++)
        //{
        //    _objects.Enqueue(_objectGenerator());
        //}
    }

    public T Get()
    {
        return _objects.Count > 0 ? _objects.Dequeue() : _objectGenerator();
    }

    public void Release(T item)
    {
        if (item is GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        _objects.Enqueue(item);
    }
}
