using UnityEngine;
using System.Collections;

public class SettingsButton : MonoBehaviour {

	void Start () {
	
	}

    public void OpenSettings()
    {
        Events.SettingsOpen();
    }
}
