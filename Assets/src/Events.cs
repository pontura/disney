using UnityEngine;
using System.Collections;

public static class Events
{

    public static System.Action ResetApp = delegate { };

    public static System.Action<UserData> AddUser = delegate { };
    public static System.Action<UserData> EditUser = delegate { };
    public static System.Action<int> RemoveUser = delegate { };
    public static System.Action<string> GotoTo = delegate { };
    public static System.Action<string> GotoBackTo = delegate { };
    public static System.Action Back = delegate { };
    public static System.Action<int> SetGameId = delegate { };

    public static System.Action<int> OnActiveUser = delegate { };
    public static System.Action OnInactiveUser = delegate { };


    public static System.Action<string> OnMusicChange = delegate { };
    public static System.Action<float> OnMusicVolumeChanged = delegate { };
}
   
