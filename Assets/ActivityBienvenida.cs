using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivityBienvenida : ScreenMain
{
    public Text field;
    public int ActivityActiveID;

	void Start () {
        ScreenManager.Instance.ActivityActiveID = ActivityActiveID;
        Invoke("Next", 3);
	}
    void Next()
    {
        Events.GotoTo("Activity_" + ScreenManager.Instance.ActivityActiveID +"_instructions");
    }
}
