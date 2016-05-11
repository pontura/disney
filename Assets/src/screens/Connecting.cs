using UnityEngine;
using System.Collections;

public class Connecting : ScreenMain {

    public GameObject connectingPanel;
    public GameObject readyPanel;

    public states state;
    private bool canceled;
    public enum states
    {
        IDLE,
        CONNECTING,
        READY
    }
    override public void OnFocus() {

        connectingPanel.SetActive(true);
        readyPanel.SetActive(false);

        state = states.CONNECTING;

        //HARDCODE:
        Invoke("OnConnection", 3);
        ///////////
    }
    public void Back()
    {
        canceled = true;
        Events.GotoBackTo("ConnectDevice");
    }
    public void Go()
    {
        Events.GotoTo("Activities");
    }
    void OnConnection()
    {
        if (canceled) return;

        connectingPanel.SetActive(false);
        readyPanel.SetActive(true);
	}
}
