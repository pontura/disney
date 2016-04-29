using UnityEngine;
using System.Collections;

public class Activities : ScreenMain {

    public void Select(int id)
    {
        Events.GotoTo("Activity_" + id);
    }
}
