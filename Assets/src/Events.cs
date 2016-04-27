using UnityEngine;
using System.Collections;

public static class Events {

    public static System.Action ResetApp = delegate { };

    public static System.Action<UserData> AddUser = delegate { };
    public static System.Action<int> RemoveUser = delegate { };
    public static System.Action<string> GotoTo = delegate { };
    public static System.Action<string> GotoBackTo = delegate { };
    public static System.Action Back = delegate { };
    
}
