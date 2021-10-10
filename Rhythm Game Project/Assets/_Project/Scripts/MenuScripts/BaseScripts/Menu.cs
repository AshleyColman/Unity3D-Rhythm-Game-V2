using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected GameObject screen = default;
    private IEnumerator transitionInCoroutine;
    private IEnumerator transitionOutCoroutine;
    private IEnumerator checkInputCoroutine;

    public void TransitionIn()
    {
        if (transitionInCoroutine != null)
        {
            StopCoroutine(transitionInCoroutine);
        }
        transitionInCoroutine = TransitionInCoroutine();
        StartCoroutine(transitionInCoroutine);
    }
    public void TransitionOut()
    {
        if (transitionOutCoroutine != null)
        {
            StopCoroutine(transitionOutCoroutine);
        }
        transitionOutCoroutine = TransitionOutCoroutine();
        StartCoroutine(transitionOutCoroutine);
    }
    protected virtual IEnumerator TransitionInCoroutine()
    {
        yield return null;
    }
    protected virtual IEnumerator TransitionOutCoroutine()
    {
        if (checkInputCoroutine != null)
        {
            StopCoroutine(checkInputCoroutine);
        }
        yield return null;
    }
    protected virtual IEnumerator CheckInputCoroutine()
    {
        yield return null;
    }
    protected void CheckInput()
    {
        if (checkInputCoroutine != null)
        {
            StopCoroutine(checkInputCoroutine);
        }
        checkInputCoroutine = CheckInputCoroutine();
        StartCoroutine(checkInputCoroutine);
    }
}