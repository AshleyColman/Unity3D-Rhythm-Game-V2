using System.Collections;

public abstract class InputMenu : Menu
{
    private IEnumerator checkInputCoroutine;
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
    protected override IEnumerator TransitionOutCoroutine()
    {
        if (checkInputCoroutine != null)
        {
            StopCoroutine(checkInputCoroutine);
        }
        yield return null;
    }
}