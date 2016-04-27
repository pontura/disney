using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UsersManager : MonoBehaviour {

    public List<UserData> users;

	void Start () {
        Events.AddUser += AddUser;
        Events.ResetApp += ResetApp;
        Events.RemoveUser += RemoveUser;
        LoadData();
	}
    void ResetApp()
    {
        users.Clear();
    }
    void LoadData()
    {
        for (int a = 0; a < 100; a++)
        {
            string userDataString = PlayerPrefs.GetString("user" + a);

            if (userDataString.Length > 1)
            {
                print(userDataString);

                UserData userData = new UserData();
                userData.id = a;
                string[] userDataArr = userDataString.Split("_"[0]);
                userData.username = userDataArr[0];

                if (userDataArr[1] == "0")
                    userData.sex = UserData.sexs.BOY;
                else
                    userData.sex = UserData.sexs.GIRL;

                userData.year = int.Parse(userDataArr[2]);
                userData.month = int.Parse(userDataArr[3]);
                userData.day = int.Parse(userDataArr[4]);

                users.Add(userData);
            }
        }
    }
    void AddUser(UserData _userData)
    {
        UserData userData = new UserData();
        userData.username = _userData.username;
        userData.sex = _userData.sex;
        userData.year = _userData.year;
        userData.month = _userData.month;
        userData.day = _userData.day;
        userData.id = users.Count;
        users.Add(userData);

        SaveAll();
    }
    void RemoveUser(int id)
    {
        UserData dataToRemove = null;
        foreach (UserData userData in users)
        {
            if (userData.id == id)
                dataToRemove = userData;
        }
        if (dataToRemove != null)
            users.Remove(dataToRemove);
    }
    void SaveAll()
    {
        for (int a = 0; a < 100; a++)
            PlayerPrefs.DeleteKey("user" + a);

        int id = 0;
        foreach (UserData userData in users)
        {
            string sex = "0";
            if (userData.sex == UserData.sexs.GIRL)
                sex = "1";
            string data = userData.username + "_" + sex + "_" + userData.year + "_" + userData.month + "_" + userData.day;
            PlayerPrefs.SetString("user" + id, data);
            print("GRABA: " + data);
            id++;
        }
    }
}
