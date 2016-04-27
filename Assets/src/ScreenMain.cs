using UnityEngine;
using System.Collections;

public class ScreenMain : MonoBehaviour {

    //cuando llega al centro:
	virtual public void OnFocus() { }

    public void GoTo(string ScreenName)
    {
        Events.GotoTo(ScreenName);
    }
    public void GoBackTo(string ScreenName)
    {
        Events.GotoBackTo(ScreenName);
    }
}
