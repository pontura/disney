using UnityEngine;
using System.Collections;

public class ActivityAnimacion : ScreenMain {

    public int Delay = 2;
    private bool notActive;

    override public void OnFocus()
    {
        Invoke("Next", Delay);
        notActive = false;
    }
    override public void LoseFocus()
    {
        notActive = true;
    }
    void Next()
    {
        if (notActive) return;
        GoTo("Playing_" + ScreenManager.Instance.ActivityActiveID);
    }
}
