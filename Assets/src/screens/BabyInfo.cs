using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BabyInfo : ScreenMain {

    public InputField inputField;
    private DateWheels dateWheels;

	void Awake () {
        dateWheels = GetComponent<DateWheels>();
	}

    override public void OnFocus() 
    {
        string username = "";
        int day = 0;
        int month = 0;
        int year = 2015;

        if (Data.Instance.userData.active)
        {
            username = Data.Instance.userData.username;
            day = Data.Instance.userData.day;
            month = Data.Instance.userData.month;
            year = Data.Instance.userData.year;
        }
        dateWheels.Init(day, month, year);
        inputField.text = username;
        
    }

    public void SexBoy()
    {
        Save(UserData.sexs.BOY);
    }
    public void SexGirl()
    {
        Save(UserData.sexs.GIRL);
    }
    void Save(UserData.sexs sexs)
    {
        UserData data = new UserData();
        data.username = inputField.textComponent.text;

        string year = dateWheels.scrollSnap_year.GetData();
        string month = dateWheels.scrollSnap_month.GetData();
        string day = dateWheels.scrollSnap_day.GetData();

      //  print("year: " + year + "  month: " + month + "   DAY: " + day  );

        data.year = int.Parse(year);
        data.month = int.Parse(month);
        data.day = int.Parse(day);
        data.sex = sexs;

        if (Data.Instance.userData.active)
        {
            data.id = Data.Instance.userData.id;
            Events.EditUser(data);
        }
        else
            Events.AddUser(data);

        Events.GotoTo("Users");
    }
    public void Back()
    {
        Events.Back();
    }
}
