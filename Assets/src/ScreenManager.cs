using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour {

    const string PREFAB_PATH = "ScreenManager";
    static ScreenManager mInstance = null;
    public float transitionDuration;
    private float Actual_X;

    public string lastScene;
    public ScreenMain activeScreen;    
    public List<ScreenMain> screens;
    public GameObject screensContainer;

    private int transitionSpeed = 10;

    public Canvas canvas;
    public int ActivityActiveID;

    public float ScreenWidth;

    public ScreenMain newScreen;
    public int _X;

    public static ScreenManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<ScreenManager>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<ScreenManager>();
                    go.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            return mInstance;
        }
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void Start()
    {
        Events.GotoTo += GotoTo;
        Events.GotoBackTo += GotoBackTo;
        Events.ResetApp += ResetApp;
        Events.Back += Back;

        foreach (ScreenMain screenMain in screensContainer.GetComponentsInChildren<ScreenMain>())
            screenMain.gameObject.SetActive(false);
        ActivateScreen(screens[0]);
        screens[0].transform.localPosition = Vector3.zero;

        ScreenWidth = Screen.width / canvas.scaleFactor;
    }
    void OnDestroy()
    {
        Events.GotoTo -= GotoTo;
        Events.GotoBackTo -= GotoBackTo;
        Events.ResetApp -= ResetApp;
        Events.Back -= Back;
    }
    public void Back()
    {
        GotoBackTo(lastScene);
    }
    void ResetApp()
    {
        ActivateScreen(screens[0]);
        screens[0].transform.localPosition = Vector3.zero;
    }
    public void GotoTo(string screenName)
    {
        Move(screenName, false);
    }
    public void GotoBackTo(string screenName)
    {
        Move(screenName, true);
    }
    void ActivateScreen(ScreenMain newScreen)
    {
        activeScreen = newScreen;
        activeScreen.gameObject.SetActive(true);
    }
    void ChangeActiveScreen()
    {
        activeScreen.gameObject.SetActive(false);
        activeScreen = newScreen;
        activeScreen.gameObject.SetActive(true);
    }
    private bool moveBack;
    public void Move(string _newScreen, bool moveBack)
    {
        Events.OnMusicChange("");

        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        lastScene = activeScreen.name;
        this.moveBack = moveBack;
        newScreen = GetScreenByName(_newScreen);
        newScreen.gameObject.SetActive(true);
        if (moveBack)
            Actual_X = -ScreenWidth;
        else
            Actual_X = ScreenWidth;

        SetPosition(newScreen, Actual_X);
        MoveNow();
    }
    void MoveNow()
    {
        newScreen.OnFocus();
        //ScreenWidth = Screen.width;
        float newX = -(Screen.width);
        if (moveBack)
            newX = (Screen.width);

        iTween.MoveBy(newScreen.gameObject, iTween.Hash(
              "x", newX,
              "time", transitionDuration,
              "easeType", "easeOutCubic",
              "oncomplete", "OnAnimationComplete",
              "onCompleteTarget", this.gameObject
          ));
        iTween.MoveBy(activeScreen.gameObject, iTween.Hash(
             "x", newX,
             "time", transitionDuration,
             "easeType", "easeOutCubic"
         ));
    }
    void OnAnimationComplete()
    {
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ChangeActiveScreen();
        activeScreen = newScreen;
        SetPosition(activeScreen, 0);
    }
    private void SetPosition(ScreenMain screenMain, float _x)
    {
        Vector2 pos = screenMain.transform.localPosition;
        pos.x = _x;
        screenMain.transform.localPosition = pos;
    }
    private ScreenMain GetScreenByName(string screenName)
    {
        foreach (ScreenMain screenMain in screens)
        {
            if (screenMain.name == screenName)
                return screenMain;
        }
        Debug.Log("Falta la screen :" + screenName);
        return null;
    }
}
