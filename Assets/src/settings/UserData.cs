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

    public sexs sex;

    public enum sexs
    {
        BOY,
        GIRL
    }
}
