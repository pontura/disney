using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivityInstructions : ScreenMain {

    public Text field;

	void Start () {
	
	}
    public void GotoNext()
    {
        GoTo("Animacion_" + ScreenManager.Instance.ActivityActiveID);
    }
}
