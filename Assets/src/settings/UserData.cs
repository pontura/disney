using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class UserData : MonoBehaviour {

    public int id;
    public string username;
    public int year;
    public int month;
    public int day;

    public bool active;

    public sexs sex;

    public enum sexs
    {
        BOY,
        GIRL
    }
    void Start()
    {
        Events.OnActiveUser += OnActiveUser;
        Events.OnInactiveUser += OnInactiveUser;
    }
    void OnActiveUser(int _id)
    {
        active = true;
        UserData userData = Data.Instance.usersManager.GetUser(_id);
        id = userData.id;
        username = userData.username;
        year = userData.year;
        day = userData.day;
        month = userData.month;
        if (userData.sex == 0)
            sex = sexs.BOY;
        else
            sex = sexs.GIRL;
    }
    void OnInactiveUser()
    {
        active = false;
    }
  
}
