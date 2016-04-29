using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Users : ScreenMain {

    public UserButton userButton;
    public Transform container;
    private bool open;
    public int MoveX;
        
	override public void OnFocus () {
        AddButtons();
	}
    void AddButtons()
    {
        foreach (Transform t in container) Destroy(t.gameObject); 

        foreach(UserData userData in Data.Instance.usersManager.users)
        {
            UserButton newUserButton = Instantiate(userButton);
            newUserButton.transform.SetParent(container);
            newUserButton.Init(this, userData);
            newUserButton.transform.localScale = Vector3.one;
        }
    }
    public void ResetApp()
    {
        Events.ResetApp();
    }
    public void AddNew()
    {
        Events.OnInactiveUser();
        Events.GotoTo("BabyInfo");
        if (open) Toogle();
    }
    public void Ready(UserButton userButton)
    {
        Events.OnActiveUser(userButton.userData.id);
        Events.GotoTo("ConnectDevice");
        if (open) Toogle();
    }
    public void Toogle()
    {
        int dest = -MoveX;

        if (open)
            dest = MoveX;

        open = !open;
        iTween.MoveBy(container.gameObject, iTween.Hash(
             "x", dest,
             "time", 1,
             "easeType", "easeOutCubic",
             "oncomplete", "OnAnimationComplete"
         ));
    }
    public void Edit(UserButton userButton)
    {
        Events.OnActiveUser(userButton.userData.id);
        Events.GotoTo("BabyInfo");
        if (open) Toogle();
    }
    public void Delete(UserButton userButton)
    {
        Events.RemoveUser(userButton.userData.id);
        Destroy(userButton.gameObject);
    }
}
