using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController<T>
{
    public event Action<T> baseEvent;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}

public class EventController
{
    private event Action baseAction;
    public void AddListener(Action listener)
    {
        baseAction += listener;
    }

    public void RemoveListener(Action listener)
    {
        baseAction -= listener;
    }

    public void InvokeEvent()
    {
        baseAction?.Invoke();
    }
}
