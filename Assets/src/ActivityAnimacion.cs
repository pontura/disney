using UnityEngine;
using System.Collections;

public class ActivityAnimacion : ScreenMain {

    public int Delay = 2;
    private bool notActive;
    public Animator anim;

    override public void OnFocus()
    {
        //Invoke("Next", Delay);
        notActive = false;
        anim.SetInteger("activity", ScreenManager.Instance.ActivityActiveID);
        anim.Play("tutorialStart",0,0);
    }
    override public void LoseFocus()
    {
        notActive = true;
    }
    public void Next()
    {
        if (notActive) return;
        GoTo("Playing_" + ScreenManager.Instance.ActivityActiveID);
    }
}
