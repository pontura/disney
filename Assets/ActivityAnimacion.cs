using UnityEngine;
using System.Collections;

public class ActivityAnimacion : ScreenMain {

    public int Delay;

	void OnEnable () {
        Invoke("AnimationDone", Delay);
	}
    void AnimationDone()
    {
        GoTo("Playing_" + ScreenManager.Instance.ActivityActiveID);
    }
}
