using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
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
