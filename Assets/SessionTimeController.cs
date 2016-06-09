using UnityEngine;
using System.Collections;

public class SessionTimeController : MonoBehaviour {

    public int lastTimeConnected_days;
    public int lastTimeConnected_hours;
    public int lastTimeConnected_minutes;    
    public int lastTimeConnected_seconds;

    //diferencia:
    private int difference_in_seconds;

    public static SessionTimeController mInstance;

    void Awake()
    {
        mInstance = this;
    }
    void Start()
    {
        NewSession();
    }
    void OnDestroy()
    {
        NewSession();
    }
    void NewSession()
    {
        string l_t_C = PlayerPrefs.GetString("lastTimeConnected", "0");
        difference_in_seconds = int.Parse(l_t_C);

        if (difference_in_seconds != 0)
        {
            lastTimeConnected_seconds = GetToday() - difference_in_seconds;
            lastTimeConnected_minutes = (int)(lastTimeConnected_seconds / 60);
            lastTimeConnected_hours = (int)(lastTimeConnected_minutes / 60);
            lastTimeConnected_days = (int)(lastTimeConnected_hours / 24);
        }

        difference_in_seconds = (int)(GetToday());

        PlayerPrefs.SetString("lastTimeConnected", difference_in_seconds.ToString());
    }
    private int GetToday()
    {
        var epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
        var timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;        
        return (int)(timestamp);
    }
    public static SessionTimeController Instance
    {
        get
        {
            return mInstance;
        }
    }
}
