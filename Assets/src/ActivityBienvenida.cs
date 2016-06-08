using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivityBienvenida : ScreenMain
{
    public Text field;
    public int ActivityActiveID;
    private bool notActive;

    override public void OnFocus()
    {    
        ScreenManager.Instance.ActivityActiveID = ActivityActiveID;
        Invoke("Cierra", 3);
        notActive = false;
        field.text = Data.Instance.texts.GetSaludo();
	}
    override public void LoseFocus()
    {
        notActive = true;
    }
    void Cierra()
    {
        if (notActive) return;
        if (ScreenManager.Instance.ActivityActiveID == 4)
        {
            Next();
            return;
        }
        field.text = Data.Instance.texts.GetCierra();
        Invoke("Next", 3);
    }
    void Next()
    {
        if (notActive) return;
        Events.GotoTo("Activity_" + ScreenManager.Instance.ActivityActiveID + "_instructions");
    }
}
