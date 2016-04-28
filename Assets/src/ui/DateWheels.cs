using UnityEngine;
using System.Collections;

public class DateWheels : MonoBehaviour {

    public ScrollSnap scrollSnap_day;
    public ScrollSnap scrollSnap_month;
    public ScrollSnap scrollSnap_year;

    public void Init(int day, int month, int year)
    {
        scrollSnap_day.Init(day);
        scrollSnap_month.Init(month);
        scrollSnap_year.Init(year - 2000);
    }

    
}
