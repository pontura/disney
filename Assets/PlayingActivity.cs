using UnityEngine;
using System.Collections;

public class PlayingActivity : ScreenMain {

    override public void OnFocus() 
    {
        Events.OnMusicChange("song" + Data.Instance.gameManager.gameID);
    }
    public void End()
    {
        Events.OnMusicChange("");
        Events.GotoBackTo("Activities");
    }
}
