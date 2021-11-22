using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public sealed class ToMainThread : MonoBehaviour
{
    private readonly static Queue<Action> queue = new Queue<Action>();
    private ToMainThread _instance;

    void Awake() => _instance = this;
    public void Update()
    {
        lock (queue)
        {
            while (queue.Count > 0)
            {
                queue.Dequeue().Invoke();
            }
        }
    }
    public void ExecuteOnMainThread(IEnumerator _action)
    {
        lock (queue)
        {
            queue.Enqueue(() => StartCoroutine(_action));
        }
    }
    public void ExecuteOnMainThread(Action _action) => ExecuteOnMainThread(ExecuteAction(_action));
    private IEnumerator ExecuteAction(Action _action)
    {
        _action();
        yield return null;
    }
    public ToMainThread AssignNewAction() => _instance;
}