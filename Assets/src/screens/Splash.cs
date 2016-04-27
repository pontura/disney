using UnityEngine;
using System.Collections;

public class Splash : ScreenMain
{
	void Start () {
	
	}
    public void Ready()
    {
        if (Data.Instance.usersManager.users.Count > 0)
            Events.GotoTo("Users");
        else
            Events.GotoTo("Intro");
    }
}
