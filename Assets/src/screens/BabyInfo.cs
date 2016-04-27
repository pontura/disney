using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BabyInfo : ScreenMain {

    public InputField inputField;
    private DateWheels dateWheels;

	void Start () {
        dateWheels = GetComponent<DateWheels>();
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
        data.year = int.Parse(dateWheels.scrollSnap_year.GetData());
        data.month = int.Parse(dateWheels.scrollSnap_month.GetData());
        data.day = int.Parse(dateWheels.scrollSnap_day.GetData());
        Events.AddUser(data);
        Events.GotoTo("Users");
    }
    public void Back()
    {
        Events.Back();
    }
}
